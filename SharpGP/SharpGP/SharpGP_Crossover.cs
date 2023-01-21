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
            //zastosowanie wyjątku zgodnie ze slajdem nr.5 z wykladu
            if (subtreesCount_Equal == 0) return null;
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

            var p1Depth = p1Node.GetDepth();
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