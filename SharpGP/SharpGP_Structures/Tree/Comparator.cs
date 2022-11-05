namespace SharpGP_Structures.Tree;

public class Condition : Node, IGrowable {
	private const double TOLERANCE = 0.00001;
	protected Expression expression { get { return (Expression) children[0]; } set { children[0] = value; } }
	CompareOp compareOp => (CompareOp) children[1];
	protected Expression expression2 { get { return (Expression) children[2]; } set { children[2] = value; } }

	public override string ToString() => expression + " " + compareOp + " " + expression2;
	public bool Evaluate(ProgramRunContext prc)
	{
		switch (compareOp.op)
		{
			case "==": return Math.Abs(expression.Evaluate(prc) - expression2.Evaluate(prc)) < TOLERANCE;
			case "!=": return Math.Abs(expression.Evaluate(prc) - expression2.Evaluate(prc)) > TOLERANCE;
			case ">": return expression.Evaluate(prc) < expression2.Evaluate(prc);
			case "<=": return expression.Evaluate(prc) <= expression2.Evaluate(prc);
			case "<": return expression.Evaluate(prc) > expression2.Evaluate(prc);
			case ">=": return expression.Evaluate(prc) >= expression2.Evaluate(prc);
		}
		return false; // should never happen
	}
	public static Condition NewCondition(PRogram ctx) => new Condition(Expression.NewExpression(ctx), CompareOp.NewCompareOp(ctx), Expression.NewExpression(ctx));
	public Condition(Expression expression, CompareOp compareOp, Expression expression2) => children = new List<Node>() {expression, compareOp, expression2};
	//this is unsafe, but makes AntlrToProgram look nicer
	public Condition(Node expression, Node compareOp, Node expression2) => children = new List<Node>() {expression, compareOp, expression2};
	public void Grow(PRogram ctx)
	{
		if (ctx.rand.Next(0, 2) == 0)
			expression = expression.Grown(ctx);
		else
			expression2 = expression2.Grown(ctx);
	}
}

public class CompareOp : Node, IMutable {
	public string op;
	public static List<String> comparatorStrings = new List<String>()
	{
		"==",
		"!=",
		"<",
		"<=",
		">",
		">="
	};
	public override string ToString() => op;

	//public CompareOp() => op = comparatorStrings[Program.rand.Next(0, comparatorStrings.Count)];
	public CompareOp(string op)
	{
		if (comparatorStrings.Contains(op))
			this.op = op;
		else
			throw new Exception("Invalid comparator string");
	}
	public static CompareOp NewCompareOp(PRogram ctx) => new CompareOp(comparatorStrings[ctx.rand.Next(0, comparatorStrings.Count)]);
	public void Mutate(PRogram ctx) => op = comparatorStrings[ctx.rand.Next(0, comparatorStrings.Count)];
}