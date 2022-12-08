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
}