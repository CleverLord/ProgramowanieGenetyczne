namespace SharpGP_Core.Tree;

public abstract class Expression : Node {
	protected Expression expression
	{
		get => (Expression) children[0];
		set => children[0] = value.GetType() == typeof(Expression) ? value : children[0];
	}
	protected Operator opeartor
	{
		get => (Operator) children[1];
		set => children[1] = value.GetType() == typeof(Operator) ? value : children[1];
	}
	protected Expression expression2
	{
		get => (Expression) children[2];
		set => children[2] = value.GetType() == typeof(Expression) ? value : children[2];
	}
	public abstract double Evaluate();
}

public class NestedExpression : Expression {
	public NestedExpression(Expression expression, Operator opeartor, Expression expression2)
	{
		this.expression = expression;
		this.opeartor = opeartor;
		this.expression2 = expression2;
	}
	public override double Evaluate() => opeartor.Evaluate(expression.Evaluate(), expression2.Evaluate());
}

public class Variable : Expression {
	public string name;
	public double value = 0;
	public Variable(string name) => this.name = name;
	public override string ToString() => new String('\t', indend) + name;
	public override double Evaluate() => value;
}

public class Constant : Expression {
	public int value;
	public Constant(int value) => this.value = value;
	public override string ToString() => new String('\t', indend) + value;
	public override double Evaluate() => value;
}

public class Read : Expression {
	public override string ToString() => new String('\t', indend) + "read()";
	public override double Evaluate() => ProgramInput.Pop();
}