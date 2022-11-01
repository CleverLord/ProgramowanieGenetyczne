namespace SharpGP_Core.Tree; 

public class Loop : Action {
	Constant repeatTimes
	{
		get => (Constant) children[0];
		set => children[0] = value.GetType() == typeof(Constant) ? value : children[0];
	}
	Scope scope
	{
		get => (Scope) children[1];
		set => children[1] = value.GetType() == typeof(Scope) ? value : children[1];
	}

	public override string ToString()
	{
		UpdateIndent();
		return new String('\t', indend) + "loop " + repeatTimes + scope;
	}

	public override void Invoke()
	{
		for (int i = 0; i < repeatTimes.value; i++) scope.Invoke();
	}
}

public class IfStatement : Action {
	Condition condition
	{
		get => (Condition) children[0];
		set => children[0] = value.GetType() == typeof(Condition) ? value : children[0];
	}
	Scope scope
	{
		get => (Scope) children[1];
		set => children[1] = value.GetType() == typeof(Scope) ? value : children[1];
	}

	public override string ToString()
	{
		UpdateIndent();
		return new String('\t', indend) + "if ( " + condition + scope;
	}
	public override void Invoke()
	{
		if (condition.Evaluate()) scope.Invoke();
	}
}

public class Assignment : Action {
	Variable variable
	{
		get => (Variable) children[0];
		set => children[0] = value.GetType() == typeof(Variable) ? value : children[0];
	}
	Expression expression
	{
		get => (Expression) children[1];
		set => children[1] = value.GetType() == typeof(Expression) ? value : children[1];
	}

	public override string ToString()
	{
		UpdateIndent();
		return new String('\t', indend) + variable + " = " + expression;
	}

	public override void Invoke()
	{
		variable.value = expression.Evaluate();
	}
}

public class Scope : Node {
	public List<Action> actions => children.Select(c => c as Action).ToList();
	public override string ToString()
	{
		UpdateIndent();
		string s = new String('\t', indend) + "{";
		foreach (var action in actions) s += action.ToString();
		s += new String('\t', indend) + "}";
		return s;
	}
	public void Invoke()
	{
		actions.ForEach(a => a.Invoke());
	}
}