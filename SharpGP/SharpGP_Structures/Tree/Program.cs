namespace SharpGP_Structures.Tree;
using System.Diagnostics;
public class PRogram : Node, IGrowable, IMutable {
	public static char TAB = ' ';
	public Random rand = new Random();
	public TreeConfig config;
	
	public List<Action> Actions => children.Cast<Action>().ToList();
	public List<String> Variables => Nodes.Where(x => x is Variable).Cast<Variable>().Select(x => x.ToString()).Distinct().ToList();
	public List<Node> Nodes => GetNestedNodes();
	public List<IGrowable> Growables => Nodes.Where(x => x is IGrowable).Cast<IGrowable>().ToList();
	public List<IMutable> Mutables => Nodes.Where(x => x is IMutable).Cast<IMutable>().ToList();

	public PRogram() => children = new List<Node>();
	public PRogram(int seed) : this() => rand = new Random(seed);
	public PRogram(List<Node> children) => this.children = children;
	public PRogram(TreeConfig config) : this() => this.config = config;
	
	public void AddAction(Action action) => children.Add(action);
	public void Invoke(ProgramRunContext prc)
	{
		Stopwatch sw = new Stopwatch();
		sw.Start();
		
		foreach (var action in Actions)
			action.Invoke(prc);
		
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
		x[rand.Next(0, x.Count)].Grow(this);
		UpdateParents();
	}
	public void Grow(PRogram ctx) => children.Add(Action.NewAction(this)); // grow program node itself
	public void FullGrow()
	{
		while(GetDepth()<config.maxDepth)
			Grow();
		Actions.ForEach(x => x.FullGrow(this, config.maxDepth-1));
	}
	
	public void Mutate() //mutate something in the program
	{
		var x = Mutables;
		x[rand.Next(0, x.Count)].Mutate(this);
	}
	public void Mutate(PRogram ctx) //mutate program node itself
	{
		Node n = children[rand.Next(0, children.Count)];
		children.Remove(n);
		children.Insert(rand.Next(0, children.Count), n);
	}
}