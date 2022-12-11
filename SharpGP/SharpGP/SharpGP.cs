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
        int currentStage = 0;

        //create initial population
        List<PRogram> population = new List<PRogram>();
        for (int i = 0; i < 100; i++)
            population.Add(TreeGenerator.GenerateProgram_FromConfig(ts.config));

        //evaluate initial population
        //while (termination condition not met)
        //create whole new population by crossover and mutation
        //decide whether to use crossover or mutation
        //select parent or parents
        //perform crossover or mutation
        //add new individual(s) to new population
        //remove old individual(s) from old population
        //evaluate new population
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

    public static Node? Getp2Node( List<Node> p2n, int p1Depth )
    {
        var dict = p2n.Where(t => t.GetDepth() <= 1 + (2 * p1Depth)).ToDictionary(t => t.GetDepth());
        
        var n = dict.Count(p => p.Key < p1Depth);
        var n0 = dict.Count(p => p.Key == p1Depth);
        var np = dict.Count(p => p.Key > p1Depth);
        var mean = dict.Where(p => p.Key < p1Depth).Average(p => p.Key);
        var meanp = dict.Where(p => p.Key > p1Depth).Average(p => p.Key);
        
        if (n == 0 || np == 0)
        {
            if (n0 == 0)
            {
                return null;
            }
            else
            {
                int p2Index = _rand.Next(0, n0);
                Node p2Node = dict.Where(p => p.Key == p1Depth).ElementAt(p2Index).Value;
                return p2Node;
            }
        }
        else
        {
            //Ruletka
            var variables = new Dictionary<double, Node>();
            foreach (var v in dict)
            {
                if( v.Key == p1Depth)
                {
                    variables.Add(1.0 / p1Depth, v.Value);
                }
                else if (v.Key < p1Depth)
                {
                    variables.Add((1.0 / (1.0 / p1Depth)) / (n * (1 + mean / meanp)), v.Value);
                }
                else
                {   variables.Add((1.0 / (1.0 / p1Depth)) / (np * (1 + meanp / mean)), v.Value);
                }
            }
            
            var random = new Random();
            var rand = random.NextDouble();
            
            var cummulativeProbability = 0.0;
            foreach (var variable in variables.Keys)
            {
                cummulativeProbability += variable;
                if (rand <= cummulativeProbability)
                {
                    return  variables[variable];
                }
            }
        }
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