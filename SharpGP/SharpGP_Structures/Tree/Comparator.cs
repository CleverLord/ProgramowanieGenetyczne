namespace SharpGP_Core.Tree;
public class Condition : Node {
	Expression expression
	{
		get => (Expression) children[0];
		set => children = value.GetType() == typeof(Expression) ? new List<Node>() {value} : children;
	}
	CompareOp compareOp
	{
		get => (CompareOp) children[1];
		set => children[1] = value.GetType() == typeof(CompareOp) ? value : children[1];
	}
	Expression expression2
	{
		get => (Expression) children[2];
		set => children[2] = value.GetType() == typeof(Expression) ? value : children[2];
	}

	public override string ToString()
	{
		UpdateIndent();
		return expression + " " + compareOp + " " + expression2;
	}
	public bool Evaluate()
	{
		switch (compareOp.op)
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
	public static Condition NewCondition(Program ctx)
	{
		Expression expression = Expression.NewExpression(ctx);
		CompareOp compareOp = CompareOp.NewCompareOp(ctx);
		Expression expression2 = Expression.NewExpression(ctx);
		return new Condition() {expression = expression, compareOp = compareOp, expression2 = expression2};
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

public class CompareOp : Node {
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
	public CompareOp()
	{
		op = (ComparatorEnum) Generator.Generator.r.Next(0, 6);
	}
	public static CompareOp NewCompareOp(Program ctx)
	{
		return new CompareOp();
	}
}

