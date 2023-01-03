using SharpGP_Structures;
using SharpGP_Structures.TestSuite;
using SharpGP_Structures.Tree;
using SharpGP.Utils;

namespace SharpGP;

public static class SharpGP
{
    private static Random _rand = new Random();

    //TODO: Make actual evolution process
    public static void PerformEvolution(TestSet ts)
    {
        //since this is static, make sure no variables are shared between runs (so they are declared in the method)
        int currentGeneration = 0;
        int popSize = 100; //move this to TestSet
        TestStage currentStage = ts.stages[0];
        Grader g= currentStage.grader;
        Agregrader ag = currentStage.ag;
        
        //create initial population
        List<PRogram> population = new List<PRogram>();
        for (int i = 0; i < popSize; i++)
            population.Add(TreeGenerator.GenerateProgram_FromConfig(ts.config));

        //evaluate initial population
        Dictionary<PRogram, double> programsToMarks = evaluatedPopulation(population, ts.testCases, g,ag);
        //while (termination condition not met)
        //termination condition set to satisfy threshold in 90 percentiles
        List<double> marks = programsToMarks.Values.ToList();
        marks.Sort();
        while (currentStage != null) //has job to do
        {
            while (marks[(int)(marks.Count * 0.9)] > currentStage.threshold)
            {
                //train
                List<PRogram> newPopulation= new List<PRogram>();
                //select zero-mark programs
                List<PRogram> zeroMarkPrograms = programsToMarks.Where(x => x.Value == 0).Select(x => x.Key).ToList();
                //add them to the new population
                newPopulation.AddRange(zeroMarkPrograms);
                
                while (newPopulation.Count < popSize)
                {
                    //select whether to crossover or mutate
                    if (_rand.NextDouble() < ts.config.MutationChance)
                    {
                        //mutate
                        PRogram p = population[_rand.Next(population.Count)];
                        PRogram newP = (PRogram) p.Clone();
                        newP.Mutate();
                        newPopulation.Add(newP);
                    }
                    else
                    {
                        //crossover
                        PRogram p1 = Tournament(programsToMarks, 4);
                        PRogram p2 = Tournament(programsToMarks, 4);
                        var x= CrossPrograms(p1, p2);
                        if (x != null)
                        {
                            newPopulation.Add(x.Value.Item1);
                            newPopulation.Add(x.Value.Item2);
                        }
                    }
                }
                programsToMarks = evaluatedPopulation(population, ts.testCases, g,ag);
                currentGeneration++;
            }
            //go to next stage
            int indexOfCurrentStage = ts.stages.IndexOf(currentStage);
            if (indexOfCurrentStage == ts.stages.Count - 1)
                currentStage = null;
            else
            {
                currentStage = ts.stages[indexOfCurrentStage + 1];
                g = currentStage.grader;
                ag = currentStage.ag;
            }
        }
        //create whole new population by crossover and mutation
        //decide whether to use crossover or mutation
    }
    private static PRogram Tournament(Dictionary<PRogram,double> programsToMarks,int tournamentSize)
    {
        PRogram target = programsToMarks.Keys.ElementAt(_rand.Next(programsToMarks.Count));
        for(int i=0;i<tournamentSize-1;i++)
        {
            PRogram p = programsToMarks.Keys.ElementAt(_rand.Next(programsToMarks.Count));
            if (target == null || programsToMarks[p] < programsToMarks[target])
                target = p;
        }
        return target;
    }

    private static Dictionary<PRogram, double> evaluatedPopulation(List<PRogram> population,List<TestCase> testCases, Grader g, Agregrader ag)
    {
        var result = new Dictionary<PRogram, double>();
        foreach (PRogram p in population)
        {
            List<double> grades = new List<double>();
            foreach (TestCase tc in testCases)
            {
                ProgramRunContext prc = new ProgramRunContext(); // make a constructor that takes a test case
                prc.input=tc.input;
                p.Invoke(prc);
                grades.Add(g.Grade(tc,prc));
            }
            result.Add(p,ag.Agregrade(grades));
        }

        return result;
    }

    //Helper functions:
    public static (PRogram, PRogram)? CrossPrograms(PRogram p1, PRogram p2)
    {
        PRogram p1c = (PRogram)p1.Clone();
        PRogram p2c = (PRogram)p2.Clone();

        List<Node> p1n = p1c.GetNodes(); // get the nodes of the first program
        List<Node> p2n = p2c.GetNodes(); // get the nodes of the second program

        p1n.RemoveAt(0); //drop the root node
        p2n.RemoveAt(0); //drop the root node

        while (p1n.Count > 0)
        {
            int p1Index = _rand.Next(0, p1n.Count);
            Node p1Node = p1n[p1Index];
            p1n.RemoveAt(p1Index);
            var matchingNodes = p2n.Where(n => n.GetType() == p1Node.GetType()).ToList();
            if (matchingNodes.Count == 0)
            {
                p1n = p1n.Where(n => n.GetType() != p1Node.GetType()).ToList();
                continue;
            }

            int p2Index = _rand.Next(0, matchingNodes.Count);
            Node p2Node = matchingNodes[p2Index];

            Node.CrossNodes(p1Node, p2Node); //this is in node, since children are protected
            return (p1c, p2c);
        }

        return null;
    }

    public static Node? Getp2Node(List<Node> p2n, int p1Depth)
    {
        Dictionary<Node, int> nodesToDepth = p2n.ToDictionary(n => n, n => n.GetDepth());
        nodesToDepth = nodesToDepth.Where(t => t.Value <= 1 + (2 * p1Depth)).ToDictionary(t => t.Key, t => t.Value);
        //obliczanie factorów zgodnie ze slajdem nr.4 z wykladu
        var subtreesCount_Lower = nodesToDepth.Count(p => p.Value < p1Depth);
        var subtreesCount_Equal = nodesToDepth.Count(p => p.Value == p1Depth);
        var subtreesCount_Greater = nodesToDepth.Count(p => p.Value > p1Depth);
        var avgSize_Lower = nodesToDepth.Where(p => p.Value < p1Depth).Average(p => p.Value);
        var avgSize_Greater = nodesToDepth.Where(p => p.Value > p1Depth).Average(p => p.Value);

        if (subtreesCount_Lower == 0 || subtreesCount_Greater == 0)
        {
            //zastosowanie wyjątku zgodnie ze slajdem nr.5 z wykladu
            if (subtreesCount_Equal == 0)
                return null;
            var equalNodes = nodesToDepth.Where(p => p.Value == p1Depth).ToList();
            return equalNodes[_rand.Next(0, equalNodes.Count)].Key;
        }

        //Ruletka
        
        //"Wszystkie poddrzewa o mniejszym rozmiarze mają takie samo prawdopodobieństwo wyboru podobnie jak o większym"
        //To po co je liczyć dla kazdego node'a?
        double factor_eq = 1.0 / p1Depth;
        double factor_lt = (1 - p1Depth) /  (1 + avgSize_Lower / avgSize_Greater);
        //double factor_gt = (1 - p1Depth) / (1 + avgSize_Greater / avgSize_Lower); //wartość nieużywana

        double random = _rand.NextDouble();
        if (random < factor_eq)
            //wybierz losowe poddrzewo o takim samym rozmiarze
            return nodesToDepth.Where(p => p.Value == p1Depth).ToArray()[_rand.Next(0, subtreesCount_Equal)].Key;
        if (random < factor_eq + factor_lt)
            //wybierz losowe poddrzewo o mniejszym rozmiarze
            return nodesToDepth.Where(p => p.Value < p1Depth).ToArray()[_rand.Next(0, subtreesCount_Equal)].Key;
        else
            //wybierz losowe poddrzewo o większym rozmiarze
            return nodesToDepth.Where(p => p.Value > p1Depth).ToArray()[_rand.Next(0, subtreesCount_Equal)].Key;

        return null;
    }

    public static (PRogram, PRogram)? CrossProgramsV2(PRogram p1, PRogram p2)
    {
        PRogram p1c = (PRogram)p1.Clone();
        PRogram p2c = (PRogram)p2.Clone();

        List<Node> p1n = p1c.GetNodes(); // get the nodes of the first program
        List<Node> p2n = p2c.GetNodes(); // get the nodes of the second program

        p1n.RemoveAt(0); //drop the root node
        p2n.RemoveAt(0); //drop the root node
        while (p1n.Count > 0)
        {
            int p1Index = _rand.Next(0, p1n.Count);
            Node p1Node = p1n[p1Index];
            p1n.RemoveAt(p1Index);
            var matchingNodes = p2n.Where(n => n.GetType() == p1Node.GetType()).ToList();
            if (matchingNodes.Count == 0)
            {
                p1n = p1n.Where(n => n.GetType() != p1Node.GetType()).ToList();
                continue;
            }

            var p1Depth = p1n.Max(n => n.GetDepth());
            Node? p2Node = Getp2Node(matchingNodes, p1Depth);
            if (p2Node == null)
            {
                p1n = p1n.Where(n => n.GetType() != p1Node.GetType()).ToList();
                continue;
            }

            Node.CrossNodes(p1Node, p2Node);
            return (p1c, p2c);
        }


        return null;
    }
}