namespace SharpGP_Core.Tree;

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

public enum ExpressionType {
	Constant,
	Variable,
	NestedExpression,
}
