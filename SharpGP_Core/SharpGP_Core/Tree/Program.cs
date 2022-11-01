namespace SharpGP_Core.Tree;

public class Program {
	public List<Action> actions = new List<Action>();
	public List<Variable> variables => nodes.Where(x => x is Variable).Cast<Variable>().ToList();
	public List<Node> nodes => actions.SelectMany(a => a.getNestedNodes()).ToList();
	public void ClearVariables() {
		nodes.Select(n => n as Variable).Where(v => v != null).ToList().ForEach(v => v.value = 0);
	}
	public void Invoke() {
		ClearVariables();
		actions.ForEach(a => a.Invoke());
	}
}

public abstract class Action : Node {
	public abstract void Invoke();
}

public static class ProgramInput {
	private static List<int> input;

	public static int Pop()
	{
		var result = input[0];
		input.RemoveAt(0);
		return result;
	}
}