namespace SharpGP_Structures.Tree;

public abstract class Expression : Node {
	public abstract double Evaluate(ProgramRunContext prc);
	public static Expression NewExpression(Program ctx)
	{
		int expType = ctx.rand.Next(0, 3);
		Variable? rand = Variable.Random(ctx); //null protection
		switch (expType)
		{
			case 0: return Constant.NewConstant(ctx);
			case 1: return rand != null ? rand : Constant.NewConstant(ctx);
			case 2: return new Read();
		}
		return new Read(); //should never happen
	}

	public Expression Grown(Program ctx)
	{
		if (ctx.rand.Next(0, 2) == 0) return new NestedExpression(NewExpression(ctx), Operator.NewOperator(ctx), this);
		return new NestedExpression(this, Operator.NewOperator(ctx), NewExpression(ctx));
	}
}

public class NestedExpression : Expression, IGrowable {
	protected Expression expression { get { return (Expression) children[0]; } set { children[0] = value; } }
	protected Operator opeartor => (Operator) children[1];
	protected Expression expression2 { get { return (Expression) children[2]; } set { children[2] = value; } }

	public NestedExpression(Expression expression, Operator opeartor, Expression expression2) => children = new List<Node>() {expression, opeartor, expression2};
	//this is unsafe but it makes antlr look nicer
	public NestedExpression(List<Node> children) => this.children = children;
	public override double Evaluate(ProgramRunContext prc) => opeartor.Evaluate(expression.Evaluate(prc), expression2.Evaluate(prc));
	public override string ToString() => $"({expression} {opeartor} {expression2})";

	public void Grow(Program ctx)
	{
		if (ctx.rand.Next(0, 2) == 0)
			expression = expression.Grown(ctx);
		else
			expression2 = expression2.Grown(ctx);
	}
}

public class Variable : Expression {
	public string name;
	public Variable(int idx) => this.name = $"x_{idx}";
	public override string ToString() => name;
	public override double Evaluate(ProgramRunContext prc) => prc.variables[name];
	public static Variable RandomOrNew(Program ctx)
	{
		int varCount = ctx.variables.Count;
		int varIdx = ctx.rand.Next(-1, varCount);
		return varIdx == -1 ? new Variable(varCount) : new Variable(varIdx);
	}
	public static Variable? Random(Program ctx)
	{
		if (ctx.variables.Count == 0) return null;
		int varIdx = ctx.rand.Next(0, ctx.variables.Count);
		return new Variable(varIdx);
	}
	//public void SetValue(ProgramRunContext prc, double d) => prc.variables[name] = d;
}

public class Constant : Expression {
	public int value;
	public Constant(int value) => this.value = value;
	public override string ToString() => value.ToString();
	public override double Evaluate(ProgramRunContext prc) => value;
	public static Constant NewConstant(Program ctx)
	{
		int value = ctx.rand.Next(ctx.minConst, ctx.maxConst + 1);
		return new Constant(value);
	}
}

public class Read : Expression {
	public override string ToString() => "read()";
	public override double Evaluate(ProgramRunContext prc) => prc.Pop();
}