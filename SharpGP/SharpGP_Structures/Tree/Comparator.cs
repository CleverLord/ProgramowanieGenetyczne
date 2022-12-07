namespace SharpGP_Structures.Tree;

public class Condition : Node, IGrowable
{
    private const double TOLERANCE = 0.00001;

    public Condition(Expression expression, Comparator comparator, Expression expression2)
    {
        children = new List<Node> { expression, comparator, expression2 };
    }

    //this is unsafe, but makes AntlrToProgram look nicer
    public Condition(Node expression, Node compareOp, Node expression2)
    {
        children = new List<Node> { expression, compareOp, expression2 };
    }

    protected Expression expression
    {
        get => (Expression)children[0];
        set => children[0] = value;
    }

    private Comparator Comparator => (Comparator)children[1];

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

    public override string ToString()
    {
        return expression + " " + Comparator + " " + expression2;
    }

    public bool Evaluate(ProgramRunContext prc)
    {
        switch (Comparator.op)
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

    public static Condition NewCondition(PRogram ctx)
    {
        return new(Expression.NewExpression(ctx), Comparator.NewCompareOp(ctx), Expression.NewExpression(ctx));
    }
}

public class Comparator : Node, IMutable
{
    public static List<string> comparatorStrings = new()
    {
        "==",
        "!=",
        "<",
        "<=",
        ">",
        ">="
    };

    public string op;

    //public CompareOp() => op = comparatorStrings[Program.rand.Next(0, comparatorStrings.Count)];
    public Comparator(string op)
    {
        if (comparatorStrings.Contains(op))
            this.op = op;
        else
            throw new Exception("Invalid comparator string");
    }

    public void Mutate(PRogram ctx)
    {
        op = comparatorStrings[ctx.rand.Next(0, comparatorStrings.Count)];
    }

    public override string ToString()
    {
        return op;
    }

    public static Comparator NewCompareOp(PRogram ctx)
    {
        return new(comparatorStrings[ctx.rand.Next(0, comparatorStrings.Count)]);
    }
}