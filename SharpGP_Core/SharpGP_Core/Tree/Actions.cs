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


public class Condition : Node {
	Expression expression
	{
		get => (Expression) children[0];
		set => children = value.GetType() == typeof(Expression) ?new List<Node>(){value} : children;
	}
	Comparator comparator
	{
		get => (Comparator) children[1];
		set => children[1] = value.GetType() == typeof(Comparator) ? value : children[1];
	}
	Expression expression2
	{
		get => (Expression) children[2];
		set => children[2] = value.GetType() == typeof(Expression) ? value : children[2];
	}

	public override string ToString()
	{
		UpdateIndent();
		return expression + " " + comparator + " " + expression2;
	}

	public bool Evaluate()
	{
		switch (comparator.op)
		{
			case ComparatorEnum.Equal:
				return expression.Evaluate() == expression2.Evaluate();
			case ComparatorEnum.NotEqual:
				return expression.Evaluate() != expression2.Evaluate();
			case ComparatorEnum.LessThan:
				return expression.Evaluate() < expression2.Evaluate();
			case ComparatorEnum.LessThanOrEqual:
				return expression.Evaluate() <= expression2.Evaluate();
			case ComparatorEnum.GreaterThan:
				return expression.Evaluate() > expression2.Evaluate();
			case ComparatorEnum.GreaterThanOrEqual:
				return expression.Evaluate() >= expression2.Evaluate();
		}
		return false; // should never happen
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
