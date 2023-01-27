using System.Diagnostics;
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

    public static EvolutionHistory PerformEvolution(TestSet ts)
    {
        int currentMaxExecutionTime = ts.config.maxExectionTime;
        DateTime startTime = DateTime.Now;
        Console.WriteLine("Starting evolution for testset: " + ts.name + " current time: " + startTime.ToString("dd/MM/yy HH:mm:ss.fff"));
        //initialize all Graders and Agregraders - connect strings to functions
        ts.stages.ForEach(stage => stage.ag.Initialize());
        ts.stages.ForEach(stage => stage.grader.Initialize());
        Stopwatch sw = new Stopwatch();
        Stopwatch totalTime = new Stopwatch();
        totalTime.Start();

        //since this is static, make sure no variables are shared between runs (so they are declared in the method)
        EvolutionHistory eh = new EvolutionHistory();
        int currentGeneration = 0;
        int popSize = 100; //move this to TestSet
        TestStage currentStage = ts.stages[0];
        bool isLastStage = ts.stages.Count == 1;
        Grader g = currentStage.grader;
        Agregrader ag = currentStage.ag;

        var gen = new EvolutionGeneration();
        
        
        //create initial population
        sw.Start();
        List<PRogram> population = new List<PRogram>();
        for (int i = 0; i < popSize; i++) population.Add(TreeGenerator.GenerateProgram_FromConfig(ts.config));
        gen.generationCreationTime = sw.ElapsedMilliseconds;
        
        
        Console.Write("Generation: ");
        while (currentStage != null) //has job to do
        {
            gen = new EvolutionGeneration();
            gen.generationIndex = -1;
            gen.gradingFunction = g.gradingFunctionName;
            //Evaluate population before determining whether it need to be evolved
            Dictionary<PRogram, double> programsToMarks = evaluatedPopulation(population, ts.testCases, g, ag, currentMaxExecutionTime);
            gen.generationEvaluationTime = sw.ElapsedMilliseconds;
            sw.Restart();
            List<double> marks = programsToMarks.Values.ToList();
            marks.Sort();

            //take a note about the generated population
            gen.SetFittness(marks);
            gen.setDepths(population.Select(x => x.GetDepth()).OrderBy(x => x).ToList());
            eh.generations.Add(gen);

            //warunek podtrzymania trenowania tego samego stage'a
            //1) nie jest ostatni stage i 90% osobników nie spełnia thresholdu
            //  lub
            //2) to ostatni stage i żaden osobnik nie spełnia thresholdu
            while ((marks[(int)(marks.Count * 0.9)] > currentStage.threshold + Double.Epsilon && !isLastStage) || (marks[0] > currentStage.threshold + Double.Epsilon && isLastStage))
            {
                Console.Write(".");
                gen = new EvolutionGeneration();
                gen.generationIndex = currentGeneration;
                sw.Restart();
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
                
                gen.generationCreationTime = sw.ElapsedMilliseconds;
                population = newPopulation;
                sw.Restart();
                programsToMarks = evaluatedPopulation(population, ts.testCases, g, ag, currentMaxExecutionTime);
                gen.gradingFunction = g.gradingFunctionName;
                gen.generationEvaluationTime = sw.ElapsedMilliseconds;
                marks = programsToMarks.Values.ToList();
                marks.Sort();
                currentGeneration++;
                gen.SetFittness(marks);
                gen.setDepths(population.Select(x => x.GetDepth()).OrderBy(x => x).ToList());
                eh.generations.Add(gen);
                currentMaxExecutionTime = Math.Clamp((int)(currentMaxExecutionTime * 1.02), 0, 1_000_000);
                File.WriteAllText("C:/Users/krzys/Documents/GitHub/ProgramowanieGenetyczne/SharpGP/SharpGP/" + ts.name + ".json",
                    Newtonsoft.Json.JsonConvert.SerializeObject(eh, Newtonsoft.Json.Formatting.Indented));
            }
            //go to next stage
            gen.bestProgram = programsToMarks.MinBy(x => x.Value).Key.ToString();
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
            Console.WriteLine("Going to next stage");
        }
        //print summary
        DateTime stopTime = DateTime.Now;
        TimeSpan ts2 = stopTime - startTime;
        eh.totalEvolutionTime = ts2.ToString("g");
        Console.WriteLine(" " + currentGeneration + " generations in total, finished at " + stopTime.ToString("dd/MM/yy HH:mm:ss.fff") + " time elapsed: "
                          + eh.totalEvolutionTime);
        //Console.WriteLine("Best mark: " + programsToMarks.MinBy(x => x.Value).Value);
        eh.generations.ForEach(x => x.actionsToCount.ToList().ForEach(y => eh.AddActionToCount(y.Key, y.Value)));

        return eh;
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
    private static Dictionary<PRogram, double> evaluatedPopulation(List<PRogram> population, List<TestCase> testCases, Grader g, Agregrader ag, int treeConfig)
    {
        var result = new Dictionary<PRogram, double>();
        foreach (PRogram p in population)
        {
            List<double> grades = new List<double>();
            foreach (TestCase tc in testCases)
            {
                ProgramRunContext prc = new ProgramRunContext(); // make a constructor that takes a test case
                prc.input = new List<double>(tc.input);
                prc.maxExecutedActions = treeConfig;
                p.Invoke(prc);
                if(! prc.hasTimeouted())
                    grades.Add(g.Grade(tc, prc));
                else
                {
                    grades.Add(double.PositiveInfinity); 
                    break;
                }
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