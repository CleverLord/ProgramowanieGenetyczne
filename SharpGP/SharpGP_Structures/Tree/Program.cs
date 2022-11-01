namespace SharpGP_Structures.Tree;

public class Program : Node {
	public static Random rand = new Random();
	public List<Action> Actions => children.Cast<Action>().ToList();
	public List<Variable> variables => nodes.Where(x => x is Variable).Cast<Variable>().ToList();
	public List<Node> nodes => GetNestedNodes();
	public void ClearVariables() {
		nodes.Select(n => n as Variable).Where(v => v != null).ToList().ForEach(v => v.value = 0);
	}
	public void Invoke() {
		ClearVariables();
		Actions.ForEach(a => a.Invoke());
	}

	public int minConst = -5;
	public int maxConst = 5;

	public void Grow()
	{
		int target = Program.rand.Next(-1, children.Count);
		if(target == -1) {
			children.Add(Action.NewAction(this));
		} else {
			Actions[target].Grow(this);
		}
	}
}

public static class ProgramInOut {
	private static List<int> input=new List<int>();
	private static List<int> output=new List<int>();

	public static double Pop()
	{
		if (input.Count == 0) return 0;
		
		var result = input[0];
		input.RemoveAt(0);
		return result;
	}

	public static void Push(double value)
	{
		output.Add((int)value);
	}
}