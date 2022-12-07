using SharpGP_Structures.TestSuite;
using SharpGP_Structures.Tree;
using SharpGP.Utils;

namespace SharpGP;

public static class SharpGP
{
    private static readonly Random _rand = new();

    //TODO: Make actual evolution process
    public static void PerformEvolution(TestSet ts)
    {
        //since this is static, make sure no variables are shared between runs (so they are declared in the method)
        var currentGeneration = 0;
        var currentStage = 0;

        //create initial population
        var population = new List<PRogram>();
        for (var i = 0; i < 100; i++) population.Add(TreeGenerator.GenerateProgram_FromConfig(ts.config));

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
        var p1c = (PRogram)p1.Clone();
        var p2c = (PRogram)p2.Clone();

        var p1n = p1c.Nodes; // get the nodes of the first program
        var p2n = p2c.Nodes; // get the nodes of the second program

        p1n.RemoveAt(0); //drop the root node
        p2n.RemoveAt(0); //drop the root node

        while (p1n.Count > 0)
        {
            var p1Index = _rand.Next(0, p1n.Count);
            var p1Node = p1n[p1Index];
            p1n.RemoveAt(p1Index);
            var matchingNodes = p2n.Where(n => n.GetType() == p1Node.GetType()).ToList();
            if (matchingNodes.Count == 0)
                continue;
            var p2Index = _rand.Next(0, matchingNodes.Count);
            var p2Node = matchingNodes[p2Index];

            Node.CrossNodes(p1Node, p2Node); //this is in node, since children are protected
            return (p1c, p2c);
        }

        return null;
    }
}