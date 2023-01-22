using System.Runtime.InteropServices;
using SharpGP_Structures;
using SharpGP_Structures.Evolution;
using SharpGP_Structures.TestSuite;
using SharpGP_Structures.Tree;
using SharpGP.Utils;

namespace SharpGP;

public static partial class SharpGP
{
    private static Random _rand = new Random();

    public static void PerformEvolution(TestSet ts)
    {
        Console.WriteLine("Starting evolution");
        //initialize all Graders and Agregraders - connect strings to functions
        ts.stages.ForEach(stage => stage.ag.Initialize());
        ts.stages.ForEach(stage => stage.grader.Initialize());

        //since this is static, make sure no variables are shared between runs (so they are declared in the method)
        EvolutionHistory eh = new EvolutionHistory();
        int currentGeneration = 0;
        int popSize = 100; //move this to TestSet
        TestStage currentStage = ts.stages[0];
        bool isLastStage = ts.stages.Count == 1;
        Grader g = currentStage.grader;
        Agregrader ag = currentStage.ag;

        //create initial population
        List<PRogram> population = new List<PRogram>();
        for (int i = 0; i < popSize; i++) population.Add(TreeGenerator.GenerateProgram_FromConfig(ts.config));

        //evaluate initial population
        Dictionary<PRogram, double> programsToMarks = evaluatedPopulation(population, ts.testCases, g, ag);
        //while (termination condition not met)
        //termination condition set to satisfy threshold in 90 percentiles
        List<double> marks = programsToMarks.Values.ToList();
        marks.Sort();
        //take a note about the generated population
        var eg = new EvolutionGeneration();
        eg.generationIndex = -1;
        eg.SetFittness(marks);
        eg.setDepths(population.Select(x => x.GetDepth()).OrderBy(x => x).ToList());
        eh.generations.Add(eg);

        while (currentStage != null) //has job to do
        {
            while ((marks[(int)(marks.Count * 0.9)] > currentStage.threshold && !isLastStage) || (marks[0] > currentStage.threshold && isLastStage))
            {
                EvolutionGeneration gen = new EvolutionGeneration();
                gen.generationIndex = currentGeneration;

                List<PRogram> newPopulation = new List<PRogram>();
                //select zero-mark programs
                List<PRogram> zeroMarkPrograms = programsToMarks.Where(x => x.Value == 0).Select(x => x.Key).ToList();
                //add them to the new population
                newPopulation.AddRange(zeroMarkPrograms);

                gen.actions.Add(new CopyZeroAction() { count = zeroMarkPrograms.Count });

                while (newPopulation.Count < popSize)
                {
                    //select whether to crossover or mutate
                    //mutate
                    if (_rand.NextDouble() < ts.config.MutationChance)
                    {
                        PRogram p = population[_rand.Next(population.Count)];
                        while (!p.canBeMutated()) // pray that any of the programs can be mutated
                            p = population[_rand.Next(population.Count)];
                        PRogram newP = (PRogram)p.Clone();
                        Type typeToMutate = getTypeToMutate(newP.config);
                        while (!newP.hasNodeOfType(typeToMutate)) typeToMutate = getTypeToMutate(newP.config);
                        newP.Mutate(typeToMutate);
                        newPopulation.Add(newP);
                        gen.actions.Add(new MutationAction() { mutatedGene = typeToMutate.Name });
                    }
                    //crossover
                    else
                    {
                        bool crossoverSuccess = false;
                        while (!crossoverSuccess)
                        {
                            PRogram p1 = Tournament(programsToMarks, 4);
                            PRogram p2 = Tournament(programsToMarks, 4);
                            if (p1 != p2)
                            {
                                CrossoverAction ca = new CrossoverAction();
                                var x = CrossProgramsV2(p1, p2, ts.config, ca);
                                if (x != null)
                                {
                                    newPopulation.Add(x.Value.Item1);
                                    newPopulation.Add(x.Value.Item2);
                                    crossoverSuccess = true;
                                    gen.actions.Add(ca);
                                }
                            }
                        }
                    }
                }
                population = newPopulation;
                programsToMarks = evaluatedPopulation(population, ts.testCases, g, ag);
                marks = programsToMarks.Values.ToList();
                marks.Sort();
                currentGeneration++;
                gen.SetFittness(marks);
                gen.setDepths(population.Select(x => x.GetDepth()).OrderBy(x => x).ToList());
                eh.generations.Add(gen);
            }
            //go to next stage
            int indexOfCurrentStage = ts.stages.IndexOf(currentStage);
            if (indexOfCurrentStage == ts.stages.Count - 1)
                currentStage = null;
            else
            {
                currentStage = ts.stages[++indexOfCurrentStage];
                g = currentStage.grader;
                ag = currentStage.ag;
                isLastStage = indexOfCurrentStage == ts.stages.Count - 1;
            }
        }
        //print summary
        Console.WriteLine("Finished evolution");
        //Console.WriteLine("Best program:");
        var bestProgram = programsToMarks.OrderBy(x => x.Value).First().Key;
        // Console.WriteLine(bestProgram.ToString());
        Console.WriteLine("Best mark: " + programsToMarks.OrderBy(x => x.Value).First().Value);
        Console.WriteLine("Generation: " + currentGeneration);
        //ProgramRunContext sampleContext = new ProgramRunContext() { input = ts.testCases[0].input };
        //bestProgram.Invoke(sampleContext);
        //Console.WriteLine("Sample output:");
        //Console.WriteLine(sampleContext.ToStringTabbed());
    }
    private static PRogram Tournament(Dictionary<PRogram, double> programsToMarks, int tournamentSize)
    {
        PRogram target = programsToMarks.Keys.ElementAt(_rand.Next(programsToMarks.Count));
        for (int i = 0; i < tournamentSize - 1; i++)
        {
            PRogram p = programsToMarks.Keys.ElementAt(_rand.Next(programsToMarks.Count));
            if (target == null || programsToMarks[p] < programsToMarks[target]) target = p;
        }
        return target;
    }
    private static Dictionary<PRogram, double> evaluatedPopulation(List<PRogram> population, List<TestCase> testCases, Grader g, Agregrader ag)
    {
        var result = new Dictionary<PRogram, double>();
        foreach (PRogram p in population)
        {
            List<double> grades = new List<double>();
            foreach (TestCase tc in testCases)
            {
                ProgramRunContext prc = new ProgramRunContext(); // make a constructor that takes a test case
                prc.input = tc.input;
                p.Invoke(prc);
                grades.Add(g.Grade(tc, prc));
            }
            result.Add(p, ag.Agregrade(grades));
        }

        return result;
    }
    public static Type getTypeToMutate(TreeConfig tc)
    {
        double chance = _rand.NextDouble();
        if (chance < tc.MutateProgramChance) return typeof(PRogram);
        if (chance < tc.MutateVariableChance) return typeof(Variable);
        if (chance < tc.MutateConstantChance) return typeof(Constant);
        if (chance < tc.MutateComparatorChance) return typeof(Comparator);
        if (chance < tc.MutateOperatorChance) return typeof(Operator);
        //if (chance < tc.MutateScopeChance) //MutationScopeChance is 1 
        return typeof(Scope);
    }
}