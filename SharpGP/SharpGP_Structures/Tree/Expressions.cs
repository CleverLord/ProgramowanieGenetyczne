namespace SharpGP_Structures.Tree;

public abstract class Expression : Node
{
    public abstract double Evaluate(ProgramRunContext prc);

    static Random random = new Random();
    static double probability = 0.10;

    public static Expression NewExpression(PRogram ctx)
    {
        //changed probability of read
        double expType = random.NextDouble();
        /*Variable? rand = Variable.Random(ctx); //null protection
        if (expType < (1-probability)/2) {
            return Constant.NewConstant(ctx);
        }
        else if (expType < (1-probability)) {
            return rand != null ? rand : Constant.NewConstant(ctx);
        } 
        else {
            return new Read();
        }
        return new Read(); //should never happen
        */
        /*Type target = ctx.config.ExpressionToCreate();
        if (target == typeof(Variable))
            return Variable.Random(ctx);
        if (target == typeof(Constant))
            return Constant.NewConstant(ctx);
        if (target == typeof(Read))
            return new Read();*/
        
        Func<PRogram, Expression> creator = ctx.config.ExpressionToCreate();
        return creator.Invoke(ctx);

        //make it later raport to runtimeContext instead of crashing whole evolution process
        throw new Exception("Invalid Expression Type");
    }

    public Expression Grown(PRogram ctx)
    {
        if (ctx.rand.Next(0, 2) == 0) return new NestedExpression(NewExpression(ctx), Operator.NewOperator(ctx), this);
        return new NestedExpression(this, Operator.NewOperator(ctx), NewExpression(ctx));
    }
}

public class NestedExpression : Expression, IGrowable
{
    protected Expression expression
    {
        get { return (Expression)children[0]; }
        set { children[0] = value; }
    }

    protected Operator opeartor => (Operator)children[1];

    protected Expression expression2
    {
        get { return (Expression)children[2]; }
        set { children[2] = value; }
    }

    public NestedExpression(Expression expression, Operator opeartor, Expression expression2) =>
        children = new List<Node>() { expression, opeartor, expression2 };

    //this is unsafe but it makes antlr look nicer
    public NestedExpression(List<Node> children) => this.children = children;

    public override double Evaluate(ProgramRunContext prc) =>
        opeartor.Evaluate(expression.Evaluate(prc), expression2.Evaluate(prc));

    public override string ToString() => $"({expression} {opeartor} {expression2})";

    public void Grow(PRogram ctx)
    {
        if (ctx.rand.Next(0, 2) == 0)
            expression = expression.Grown(ctx);
        else
            expression2 = expression2.Grown(ctx);
    }
}

public class Variable : Expression, IMutable
{
    public string name;
    public Variable(int idx) => this.name = $"x_{idx}";
    public override string ToString() => name;

    public override double Evaluate(ProgramRunContext prc)
    {
        prc.variables.TryGetValue(name, out double value);
        return value;
    }

    public static Variable RandomOrNew(PRogram ctx)
    {
        int varCount = ctx.VariableCount;
        int varIdx = ctx.rand.Next(-1, varCount);
        return varIdx == -1 ? new Variable(varCount) : new Variable(varIdx);
    }

    public static Variable? Random(PRogram ctx)
    {
        if (ctx.VariableCount == 0) return null;
        int varIdx = ctx.rand.Next(0, ctx.VariableCount);
        return new Variable(varIdx);
    }

    public void Mutate(PRogram ctx)
    {
        int idx = ctx.rand.Next(0,ctx.VariableCount+1);
        name = $"x_{idx}";
    }
}

public class Constant : Expression, IMutable
{
    public int value;
    public Constant(int value) => this.value = value;
    public override string ToString() => value.ToString();

    public override double Evaluate(ProgramRunContext prc) => value;

    public static Constant NewConstant(PRogram ctx)
    {
        int value = ctx.rand.Next(ctx.config.minVariableValue, ctx.config.maxVariableValue + 1);
        return new Constant(value);
    }

    public void Mutate(PRogram ctx)
    {
        value = ctx.rand.Next(ctx.config.minVariableValue, ctx.config.maxVariableValue + 1);
    }
}

public class Read : Expression
{
    public override string ToString() => "read()";
    public override double Evaluate(ProgramRunContext prc) => prc.Pop();
    public static Read NewRead(PRogram ctx) => new Read();
}