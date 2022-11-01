namespace SharpGP_Structures.Tree;

public class Condition : Node {
	Expression expression => (Expression) children[0];
	CompareOp compareOp => (CompareOp) children[1];
	Expression expression2 => (Expression) children[2];

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

	public static Condition NewCondition(Program ctx) => new Condition()
		{children = new List<Node>() {Expression.NewExpression(ctx), CompareOp.NewCompareOp(ctx), Expression.NewExpression(ctx)}};
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
		op = (ComparatorEnum) Program.rand.Next(0, 6);
	}

	public static CompareOp NewCompareOp(Program ctx)
	{
		return new CompareOp();
	}
}