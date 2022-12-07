using System.Diagnostics;

namespace SharpGP_Structures.Tree;

public class PRogram : Node, IGrowable, IMutable
{
    public static char TAB = ' ';
    public TreeConfig config;
    public Random rand = new();

    public PRogram()
    {
        children = new List<Node>();
    }

    public PRogram(int seed) : this()
    {
        rand = new Random(seed);
    }

    public PRogram(List<Node> children)
    {
        this.children = children;
    }

    public PRogram(TreeConfig config) : this()
    {
        this.config = config;
    }

    public List<Action> Actions => children.Cast<Action>().ToList();

    public List<string> Variables =>
        Nodes.Where(x => x is Variable).Cast<Variable>().Select(x => x.ToString()).Distinct().ToList();

    public List<Node> Nodes => GetNestedNodes();
    public List<IGrowable> Growables => Nodes.Where(x => x is IGrowable).Cast<IGrowable>().ToList();
    public List<IMutable> Mutables => Nodes.Where(x => x is IMutable).Cast<IMutable>().ToList();

    public void Grow(PRogram ctx)
    {
        children.Add(Action.NewAction(this));
        // grow program node itself
    }

    public void Mutate(PRogram ctx) //mutate program node itself
    {
        var n = children[rand.Next(0, children.Count)];
        children.Remove(n);
        children.Insert(rand.Next(0, children.Count), n);
    }

    public void AddAction(Action action)
    {
        children.Add(action);
    }

    public void Invoke(ProgramRunContext prc)
    {
        var sw = new Stopwatch();
        sw.Start();

        foreach (var action in Actions)
            action.Invoke(prc);

        sw.Stop();
        prc.ElapsedMilliseconds = sw.ElapsedMilliseconds;
    }

    public override string ToString()
    {
        UpdateIndent();
        var s = "";
        foreach (var child in children) s += child + "\n";
        return s;
    }

    public void Grow() // grow whole program
    {
        var x = Growables;
        //apply config percentages
        for (var i = 0; i < 10; i++)
        {
            var t = config.ActionToCreate();
            if (x.Any(y => y.GetType() == t))
            {
                var y = x.Where(y => y.GetType() == t).ToList();
                y[rand.Next(y.Count())].Grow(this);
                UpdateParents();
                return;
            }
        }

        //if no config percentages apply, grow random
        x[rand.Next(0, x.Count)].Grow(this);
        UpdateParents();
    }

    public void FullGrow()
    {
        while (GetDepth() < config.maxDepth)
            Grow();
        Actions.ForEach(x => x.FullGrow(this, config.maxDepth - 1));
    }

    public void Mutate() //mutate something in the program
    {
        var x = Mutables;
        //apply config percentages
        for (var i = 0; i < 10; i++)
        {
            var t = config.TypeToMutate();
            if (x.Any(y => y.GetType() == t))
            {
                var y = x.Where(y => y.GetType() == t).ToList();
                y[rand.Next(y.Count())].Mutate(this);
                return;
            }
        }

        //if no config percentages apply, mutate random
        x[rand.Next(0, x.Count)].Mutate(this);
    }
}