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


public class Variable : Node {
	public string name;
	public double value = 0;

	public override string ToString()
	{
		return new String('\t', indend) + name;
	}
	public Variable(string name)
	{
		this.name = name;
	}
}

public class Constant : Node {
	public int value;

	public override string ToString()
	{
		return new String('\t', indend) + value;
	}
}