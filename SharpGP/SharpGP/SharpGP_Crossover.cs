using SharpGP_Structures;
using SharpGP_Structures.Evolution;
using SharpGP_Structures.Tree;

namespace SharpGP;

public partial class SharpGP
{
    public static Node? Getp2Node(List<Node> p2n, int p1Depth)
    {
        Dictionary<Node, int> nodesToDepth = p2n.ToDictionary(n => n, n => n.GetDepth());
        nodesToDepth = nodesToDepth.Where(t => t.Value <= 1 + (2 * p1Depth)).ToDictionary(t => t.Key, t => t.Value);
        //obliczanie factorów zgodnie ze slajdem nr.4 z wykladu
        var subtreesCount_Lower = nodesToDepth.Count(p => p.Value < p1Depth);
        var subtreesCount_Equal = nodesToDepth.Count(p => p.Value == p1Depth);
        var subtreesCount_Greater = nodesToDepth.Count(p => p.Value > p1Depth);


        if (subtreesCount_Lower == 0 || subtreesCount_Greater == 0)
        {
            if (subtreesCount_Equal == 0)
                //return random node
                return p2n[_rand.Next(p2n.Count)];
            //zastosowanie wyjątku zgodnie ze slajdem nr.5 z wykladu
            var equalNodes = nodesToDepth.Where(p => p.Value == p1Depth).ToList();
            return equalNodes[_rand.Next(0, equalNodes.Count)].Key;
        }

        //Following operations moved from above due to System.InvalidOperationException: Could not get average of empty sequence.
        var avgSize_Lower = nodesToDepth.Where(p => p.Value < p1Depth).Average(p => p.Value);
        var avgSize_Greater = nodesToDepth.Where(p => p.Value > p1Depth).Average(p => p.Value);

        //Ruletka

        //"Wszystkie poddrzewa o mniejszym rozmiarze mają takie samo prawdopodobieństwo wyboru podobnie jak o większym"
        //To po co je liczyć dla kazdego node'a?
        double factor_eq = 1.0 / p1Depth;
        double factor_lt = (1 - p1Depth) / (1 + avgSize_Lower / avgSize_Greater);
        //double factor_gt = (1 - p1Depth) / (1 + avgSize_Greater / avgSize_Lower); //wartość nieużywana

        if (subtreesCount_Equal == 0) //jeżeli nie ma podrzew o równych rozmiarach, rozdziel prawdopodobieństwo proporcjonalnie między większe i mniejsze
        {
            factor_eq = 0;
            double sum = factor_lt + (1 - factor_lt - factor_eq); // factor_lt + factor_gt
            factor_lt /= sum;
        }

        double random = _rand.NextDouble();
        if (random < factor_eq || subtreesCount_Equal != 0)
            //wybierz losowe poddrzewo o takim samym rozmiarze
            return nodesToDepth.Where(p => p.Value == p1Depth).ToArray()[_rand.Next(0, subtreesCount_Equal)].Key;
        if (random < factor_eq + factor_lt)
            //wybierz losowe poddrzewo o mniejszym rozmiarze
            return nodesToDepth.Where(p => p.Value < p1Depth).ToArray()[_rand.Next(0, subtreesCount_Lower)].Key;
        else
            //wybierz losowe poddrzewo o większym rozmiarze
            return nodesToDepth.Where(p => p.Value > p1Depth).ToArray()[_rand.Next(0, subtreesCount_Greater)].Key;

        return null;
    }
    public static (PRogram, PRogram)? CrossProgramsV2(PRogram p1, PRogram p2, TreeConfig tc, CrossoverAction ca)
    {
        PRogram p1c = (PRogram)p1.Clone();
        PRogram p2c = (PRogram)p2.Clone();
        ca.parent1Depth = p1c.GetDepth();
        ca.parent2Depth = p2c.GetDepth();

        List<Node> p1n = p1c.GetNodes(); // get the nodes of the first program
        List<Node> p2n = p2c.GetNodes(); // get the nodes of the second program

        p1n.RemoveAt(0); //drop the root node
        p2n.RemoveAt(0); //drop the root node

        Type toCross = GetTypeToCross(tc);
        var matchingNodesp1n = p1n.Where(n => n.GetType() == toCross).ToList();
        var matchingNodesp2n = p2n.Where(n => n.GetType() == toCross).ToList();
        for (int i = 0; i < 20; i++)
        {
            toCross = GetTypeToCross(tc);
            matchingNodesp1n = p1n.Where(n => n.GetType() == toCross).ToList();
            matchingNodesp2n = p2n.Where(n => n.GetType() == toCross).ToList();
            if (matchingNodesp1n.Count > 0 && matchingNodesp2n.Count > 0) break;
        }
        if (matchingNodesp1n.Count == 0 || matchingNodesp2n.Count == 0) { return null; }

        Node p1Node = matchingNodesp1n[_rand.Next(0, matchingNodesp1n.Count)];
        var p1Depth = p1Node.GetDepth();
        Node? p2Node = Getp2Node(matchingNodesp2n, p1Depth);
        if (p2Node != null)
        {
            Node.CrossNodes(p1Node, p2Node);
            ca.crossedType = toCross.Name;
            ca.child1Depth = p1c.GetDepth();
            ca.child2Depth = p2c.GetDepth();
            return (p1c, p2c);
        }
        throw new Exception("Should never happen");
        return null;
    }
    public static Type GetTypeToCross(TreeConfig tc)
    {
        double r = _rand.NextDouble();
        if (r < tc.CrossoverLoopChance) return typeof(Loop);
        if (r < tc.CrossoverIfChance) return typeof(IfStatement);
        if (r < tc.CrossoverWriteChance) return typeof(Write);
        if (r < tc.CrossoverAssignmentChance) return typeof(Assignment);
        if (r < tc.CrossOverVariableChance) return typeof(Variable);
        if (r < tc.CrossOverConstantChance) return typeof(Constant);
        if (r < tc.CrossOverNestedExpressionChance) return typeof(NestedExpression);
        if (r < tc.CrossOverComparatorChance) return typeof(Comparator);
        if (r < tc.CrossOverOperatorChance) return typeof(Operator);
        //if(r<tc.CrossOverScopeChance) return typeof(Scope);
        return typeof(Scope);
    }
}