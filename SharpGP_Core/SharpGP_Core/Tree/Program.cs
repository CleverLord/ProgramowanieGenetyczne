namespace SharpGP_Core.Tree;

public class Program {
	public List<Action> actions = new List<Action>();
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

public abstract class Node {
	public List<Node> children = new List<Node>();
	public int indend = 0;
	protected void UpdateIndent() => children.ForEach(n => n.indend = indend + 1);
	public List<Node> getNestedNodes()
	{
		var x = new List<Node>();
		x.Add(this);
		x.AddRange(children.SelectMany(n => n.getNestedNodes()).ToList());
		return x;
	}
}


public enum ExpressionType {
	Constant,
	Variable,
	NestedExpression,
}

public class Expression : Node {
	public ExpressionType type;
	Variable variable
	{
		get => (Variable) children[0];
		set => children = value.GetType() == typeof(Variable) ? new List<Node>(){value} : children;
	}
	Constant constant
	{
		get => (Constant) children[0];
		set => children = value.GetType() == typeof(Constant) ? new List<Node>(){value} : children;
	}
	Expression expression
	{
		get => (Expression) children[0];
		set => children[0] = value.GetType() == typeof(Expression) ? value : children[1];
	}
	Operator opeartor
	{
		get => (Operator) children[1];
		set => children[1] = value.GetType() == typeof(Operator) ? value : children[1];
	}
	Expression expression2
	{
		get => (Expression) children[2];
		set => children[2] = value.GetType() == typeof(Expression) ? value : children[2];
	}

	public double Evaluate()
	{
		if (type == ExpressionType.Constant) return constant.value;
		if (type == ExpressionType.Variable) return variable.value;
		if (type == ExpressionType.NestedExpression) return opeartor.Evaluate(expression.Evaluate(), expression2.Evaluate());
		return 0; //should never happen
	}
}

public enum OperatorEnum {
	Plus,
	Minus,
	Multiply,
	Divide
}

public class Operator : Node {
	public OperatorEnum op;

	public override string ToString()
	{
		switch (op)
		{
			case OperatorEnum.Plus:
				return "+";
			case OperatorEnum.Minus:
				return "-";
			case OperatorEnum.Multiply:
				return "*";
			case OperatorEnum.Divide:
				return "/";
			default:
				return "";
		}
	}

	public double Evaluate(double a, double b)
	{
		switch (op)
		{
			case OperatorEnum.Plus:
				return a + b;
			case OperatorEnum.Minus:
				return a - b;
			case OperatorEnum.Multiply:
				return a * b;
			case OperatorEnum.Divide:
				if (b < 0.00001)
					return a;
				return a / b;
		}
		return 0; //should never happen
	}
}

public enum ComparatorEnum {
	Equal,
	NotEqual,
	LessThan,
	LessThanOrEqual,
	GreaterThan,
	GreaterThanOrEqual
}

public class Comparator : Node {
	public ComparatorEnum op;

	public override string ToString()
	{
		switch (op)
		{
			case ComparatorEnum.Equal:
				return "==";
			case ComparatorEnum.NotEqual:
				return "!=";
			case ComparatorEnum.LessThan:
				return "<";
			case ComparatorEnum.LessThanOrEqual:
				return "<=";
			case ComparatorEnum.GreaterThan:
				return ">";
			case ComparatorEnum.GreaterThanOrEqual:
				return ">=";
		}
		return "!!"; // should never happen
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

public class Variable : Node {
	public string name;
	public double value;

	public override string ToString()
	{
		return new String('\t', indend) + name;
	}
}

public class Constant : Node {
	public int value;

	public override string ToString()
	{
		return new String('\t', indend) + value;
	}
}