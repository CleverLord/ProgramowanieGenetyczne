namespace SharpGP_Structures.Tree;

public class PRogram : Node, IGrowable {
	public static char TAB = ' ';
	public Random rand = new Random();
	public List<Action> Actions => children.Cast<Action>().ToList();
	public List<String> Variables => Nodes.Where(x => x is Variable).Cast<Variable>().Select(x => x.ToString()).Distinct().ToList();
	public List<Node> Nodes => GetNestedNodes();
	public List<IGrowable> Growables => Nodes.Where(x => x is IGrowable).Cast<IGrowable>().ToList();

	public int minConst = 0;
	public int maxConst = 5;

	public PRogram() => children = new List<Node>();
	public PRogram(int seed) : this() => rand = new Random(seed);
	public PRogram(List<Node> children) => this.children = children;
	public void AddAction(Action action) => children.Add(action);
	public void Invoke(ProgramRunContext prc) => Actions.ForEach(a => a.Invoke(prc));
	public override string ToString()
	{
		String s = "";
		foreach (var child in children) 
			s += child + "\n";
		return s;
	}
	public void Grow() // grow whole program
	{
		var x = Growables;
		x[rand.Next(0, x.Count)].Grow(this);
		UpdateParents();
	}
	public void Grow(PRogram ctx) => children.Add(Action.NewAction(this)); // grow program node itself
}

public class ProgramRunContext {
	private List<int> input = new List<int>();
	private List<int> output = new List<int>();
	public Dictionary<string, double> variables = new Dictionary<string, double>();
	public double Pop()
	{
		if (input.Count == 0) return 0;
		var result = input[0];
		input.RemoveAt(0);
		return result;
	}

	public void Push(double value)
	{
		output.Add((int) value);
	}
}