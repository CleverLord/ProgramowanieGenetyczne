namespace SharpGP_Structures.Tree;

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
	public static Expression NewExpression(Program ctx)
	{
		int expType = Program.rand.Next(0,3);
		Variable? rand = Variable.Random(ctx); //null protection
		switch (expType)
		{
			case 0:
				return Constant.NewConstant(ctx);
			case 1:
				return rand!=null ? rand:Constant.NewConstant(ctx);
			case 2:
				return new Read();
		}
		return new Read();//should never happen
	}
}

public class NestedExpression : Expression {
	public NestedExpression(Expression expression, Operator opeartor, Expression expression2)
	{
		this.expression = expression;
		this.opeartor = opeartor;
		this.expression2 = expression2;
	}
	public override double Evaluate() => opeartor.Evaluate(expression.Evaluate(), expression2.Evaluate());
	public override string ToString() => $"({expression} {opeartor} {expression2})";

	public override void Grow(Program ctx)
	{
		switch(Program.rand.Next(0, 2)) 
		{
			case 0:
				expression.Grow(ctx);
				break;
			case 1:
				expression2.Grow(ctx);
				break;
		}
	}
}

public class Variable : Expression {
	public string name;
	public double value = 0;
	public Variable(int idx) => this.name = $"x_{idx}";
	public override string ToString() => name;
	public override double Evaluate() => value;
	public static Variable RandomOrNew(Program ctx)
	{
		int varIdx = Program.rand.Next(-1,ctx.variables.Count);
		return varIdx == -1 ? new Variable(ctx.variables.Count) : ctx.variables[varIdx];
	}
	public static Variable? Random(Program ctx)
	{
		if(ctx.variables.Count == 0) return null;
		int varIdx = Program.rand.Next(0,ctx.variables.Count);
		return varIdx == -1 ? new Variable(ctx.variables.Count) : ctx.variables[varIdx];
	}
}

public class Constant : Expression {
	public int value;
	public Constant(int value) => this.value = value;
	public override string ToString() => value.ToString();
	public override double Evaluate() => value;
	public static Constant NewConstant( Program ctx)
	{
		int value = Program.rand.Next(ctx.minConst, ctx.maxConst+1);
		return new Constant(value);
	}
}

public class Read : Expression {
	public override string ToString() => "read()";
	public override double Evaluate() => ProgramInOut.Pop();
}