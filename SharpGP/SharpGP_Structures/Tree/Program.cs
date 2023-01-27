namespace SharpGP_Structures.Tree;

using System.Diagnostics;

public class PRogram : Node, IGrowable, IMutable
{
    public static char TAB = ' ';
    public Random rand = new Random();
    public TreeConfig config = new TreeConfig();

    private List<Action> Actions => children.Cast<Action>().ToList();

    private List<String> Variables => Nodes.Where(x => x is Variable).Cast<Variable>().Select(x => x.ToString()).Distinct().ToList();
    public int VariableCount => Variables.Count;
    private List<Node> Nodes => GetNestedNodes();

    //waning, following function gives access to all nodes, and makes it possible to destroy the tree structure
    public List<Node> GetNodes() => Nodes;
    public List<IGrowable> Growables => Nodes.Where(x => x is IGrowable).Cast<IGrowable>().ToList();
    private List<IMutable> Mutables => Nodes.Where(x => x is IMutable).Cast<IMutable>().ToList();

    public PRogram() => children = new List<Node>();
    public PRogram(int seed) : this() => rand = new Random(seed);
    public PRogram(List<Node> children) => this.children = children;
    public PRogram(TreeConfig config) : this() => this.config = config;

    public void AddAction(Action action) => children.Add(action);
    public void Invoke(ProgramRunContext prc)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        try
        {
            foreach (var action in Actions) action.Invoke(prc);    
        }
        catch(Exception _){}
        

        sw.Stop();
        prc.ElapsedMilliseconds = sw.ElapsedMilliseconds;
    }
    public override string ToString()
    {
        UpdateIndent();
        String s = "";
        foreach (var child in children) s += child + "\n";
        return s;
    }
    public void Grow() // grow whole program
    {
        var x = Growables;
        //apply config percentages
        for (int i = 0; i < 10; i++)
        {
            Type t = config.TypeToGrow();
            var growable = Growables.Where(x => x.GetType() == t).ToList();
            if (growable.Count != 0)
            {
                growable[rand.Next(growable.Count)].Grow(this);
                UpdateParents();
                return;
            }
        }
        x[rand.Next(0, x.Count)].Grow(this);
        UpdateParents();
    }
    public void Grow(PRogram ctx) => children.Add(Action.NewAction(this)); // grow program node itself
    public void FullGrow()
    {
        while (GetDepth() < config.maxDepth) Grow();
        Actions.ForEach(x => x.FullGrow(this, config.maxDepth - 1));
    }

    public void Mutate(Type t) //mutate something in the program
    {
        var x = Mutables;
        //apply config percentages
        var mutable = Mutables.Where(x => x.GetType() == t).ToList();
        if (mutable.Count != 0)
        {
            mutable[rand.Next(mutable.Count)].Mutate(this);
            return;
        }
        Console.WriteLine("This should never be printed" + t + Environment.NewLine + ToString());
        x[rand.Next(0, x.Count)].Mutate(this);
    }
    public void Mutate(PRogram ctx) //mutate program node itself
    {
        Node n = children[rand.Next(0, children.Count)];
        children.Remove(n);
        children.Insert(rand.Next(0, children.Count), n);
    }
    public bool hasNodeOfType(Type t) => Nodes.Any(x => x.GetType() == t);
    public bool canBeMutated() => Mutables.Count != 0;
}