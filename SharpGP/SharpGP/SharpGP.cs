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
        Dictionary<Node, int> nodesToDepth  =  p2n.ToDictionary( n => n, n => n.GetDepth() );
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
            {
                return null;
            }
            else
            {
                var equalNodes = nodesToDepth.Where(p => p.Value == p1Depth).ToList();
                return equalNodes[_rand.Next(0, equalNodes.Count)].Key;
            }
        }
        else
        {
            //Ruletka
            var nodesToFactor = new Dictionary< Node, double>();
            foreach (var v in nodesToDepth)
            {
                Node node = v.Key;
                int depth = v.Value;
                double factor = 0;
                if( depth == p1Depth)
                {
                    factor = 1.0/ p1Depth;
                }
                else
                {
                    double a = 1.0 / (1.0 / p1Depth);
                    if (depth < p1Depth)
                    {
                        factor = a / (subtreesCount_Lower * (1 + avgSize_Lower / avgSize_Greater));
                    }
                    else
                    { 
                        factor  = a / (subtreesCount_Greater * (1 + avgSize_Greater / avgSize_Lower));
                    }
                }
                
                nodesToFactor.Add(node, factor);
            }
            
            Dictionary<int, double> depthFactors = new Dictionary<int, double>();
            foreach (var kvp in nodesToDepth)
            {
                Node node = kvp.Key;
                int depth = kvp.Value;
                if (!depthFactors.ContainsKey(depth))
                {
                    depthFactors.Add(depth, 0);
                }
                depthFactors[depth] += nodesToFactor[node];
            }
            
            double sum = depthFactors.Sum(kvp => kvp.Value);
            int maxDepth = depthFactors.Max(kvp => kvp.Key);
            double[] depthFactorsArray = new double[maxDepth + 1];
            for (int i = 0; i < depthFactorsArray.Length; i++)
            {
                depthFactorsArray[i] = depthFactors.ContainsKey(i) ? depthFactors[i] / sum : 0;
            }

            var depthFactorsCumulated = new double[depthFactorsArray.Length];
            for (int i = 0; i < depthFactorsCumulated.Length; i++)
            {
                depthFactorsCumulated[i] = depthFactorsArray[i] + (i > 0 ? depthFactorsCumulated[i - 1] : 0);
            }
            
            double random = _rand.NextDouble();
            int depthIndex = 0;
            for (int i = 0; i < depthFactorsCumulated.Length; i++)
            {
                if (random < depthFactorsCumulated[i])
                {
                    depthIndex = i;
                    break;
                }
            }
            
            var nodesAtDepth = nodesToDepth.Where(kvp => kvp.Value == depthIndex).ToList();
            return nodesAtDepth[_rand.Next(0, nodesAtDepth.Count)].Key;
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