namespace SharpGP_Structures.Tree;

public abstract class Expression : Node
{
    private static readonly Random random = new();
    private static readonly double probability = 0.10;
    public abstract double Evaluate(ProgramRunContext prc);

    public static Expression NewExpression(PRogram ctx)
    {
        //changed probability of read
        var expType = random.NextDouble();
        var rand = Variable.Random(ctx); //null protection
        if (expType < (1 - probability) / 2)
            return Constant.NewConstant(ctx);
        if (expType < 1 - probability)
            return rand != null ? rand : Constant.NewConstant(ctx);
        return new Read();
        return new Read(); //should never happen
    }

    public Expression Grown(PRogram ctx)
    {
        if (ctx.rand.Next(0, 2) == 0) return new NestedExpression(NewExpression(ctx), Operator.NewOperator(ctx), this);
        return new NestedExpression(this, Operator.NewOperator(ctx), NewExpression(ctx));
    }
}

public class NestedExpression : Expression, IGrowable
{
    public NestedExpression(Expression expression, Operator opeartor, Expression expression2)
    {
        children = new List<Node> { expression, opeartor, expression2 };
    }

    //this is unsafe but it makes antlr look nicer
    public NestedExpression(List<Node> children)
    {
        this.children = children;
    }

    protected Expression expression
    {
        get => (Expression)children[0];
        set => children[0] = value;
    }

    protected Operator opeartor => (Operator)children[1];

    protected Expression expression2
    {
        get => (Expression)children[2];
        set => children[2] = value;
    }

    public void Grow(PRogram ctx)
    {
        if (ctx.rand.Next(0, 2) == 0)
            expression = expression.Grown(ctx);
        else
            expression2 = expression2.Grown(ctx);
    }

    public override double Evaluate(ProgramRunContext prc)
    {
        return opeartor.Evaluate(expression.Evaluate(prc), expression2.Evaluate(prc));
    }

    public override string ToString()
    {
        return $"({expression} {opeartor} {expression2})";
    }
}

public class Variable : Expression, IMutable
{
    public string name;

    public Variable(int idx)
    {
        name = $"x_{idx}";
    }

    public void Mutate(PRogram ctx)
    {
        var x = ctx.Variables;
        x.Add(name);
        name = ctx.Variables[ctx.rand.Next(0, ctx.Variables.Count)];
    }

    public override string ToString()
    {
        return name;
    }

    public override double Evaluate(ProgramRunContext prc)
    {
        prc.variables.TryGetValue(name, out var value);
        return value;
    }

    public static Variable RandomOrNew(PRogram ctx)
    {
        var varCount = ctx.Variables.Count;
        var varIdx = ctx.rand.Next(-1, varCount);
        return varIdx == -1 ? new Variable(varCount) : new Variable(varIdx);
    }

    public static Variable? Random(PRogram ctx)
    {
        if (ctx.Variables.Count == 0) return null;
        var varIdx = ctx.rand.Next(0, ctx.Variables.Count);
        return new Variable(varIdx);
    }
}

public class Constant : Expression, IMutable
{
    public int value;

    public Constant(int value)
    {
        this.value = value;
    }

    public void Mutate(PRogram ctx)
    {
        value = ctx.rand.Next(ctx.config.minVariableValue, ctx.config.maxVariableValue + 1);
    }

    public override string ToString()
    {
        return value.ToString();
    }

    public override double Evaluate(ProgramRunContext prc)
    {
        return value;
    }

    public static Constant NewConstant(PRogram ctx)
    {
        var value = ctx.rand.Next(ctx.config.minVariableValue, ctx.config.maxVariableValue + 1);
        return new Constant(value);
    }
}

public class Read : Expression
{
    public override string ToString()
    {
        return "read()";
    }

    public override double Evaluate(ProgramRunContext prc)
    {
        return prc.Pop();
    }
}