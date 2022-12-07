namespace SharpGP_Structures.Tree;

public abstract class Action : Node
{
    public abstract void Invoke(ProgramRunContext prc);

    public static Action NewAction(PRogram ctx)
    {
        Action? result = null;
        switch (ctx.rand.Next(0, 4))
        {
            case 0:
                result = Assignment.NewAssignment(ctx);
                break;
            case 1:
                result = IfStatement.NewIfStatement(ctx);
                break;
            case 2:
                result = Loop.NewLoop(ctx);
                break;
            case 3:
                result = Write.NewWrite(ctx);
                break;
        }

        return result ?? Write.NewWrite(ctx); //write is just in case
    }

    public abstract void FullGrow(PRogram ctx, int targetDepth);
}

public class Loop : Action
{
    public Loop(Constant repeatTimes, Scope scope)
    {
        children = new List<Node> { repeatTimes, scope };
    }

    private Constant repeatTimes => (Constant)children[0];
    private Scope scope => (Scope)children[1];

    public static Action NewLoop(PRogram ctx)
    {
        return new Loop(Constant.NewConstant(ctx), new Scope());
    }

    public override void Invoke(ProgramRunContext prc)
    {
        for (var i = 0; i < repeatTimes.value; i++)
            scope.Invoke(prc);
    }

    public override void FullGrow(PRogram ctx, int targetDepth)
    {
        while (scope.GetDepth() < targetDepth) scope.Grow(ctx);

        scope.FullGrow(ctx, targetDepth);
    }

    public override string ToString()
    {
        UpdateIndent();
        return new string(PRogram.TAB, indend) + "loop " + repeatTimes + " {\n" + scope +
               new string(PRogram.TAB, indend) + "}";
    }
}

public class IfStatement : Action
{
    public IfStatement(Condition condition, Scope scope)
    {
        children = new List<Node> { condition, scope };
    }

    private Condition condition => (Condition)children[0];
    private Scope scope => (Scope)children[1];

    public static IfStatement NewIfStatement(PRogram ctx)
    {
        return new(Condition.NewCondition(ctx), new Scope());
    }

    public override void Invoke(ProgramRunContext prc)
    {
        if (condition.Evaluate(prc))
            scope.Invoke(prc);
    }

    public override string ToString()
    {
        UpdateIndent();
        return new string(PRogram.TAB, indend) + "if (" + condition + "){\n" + scope + new string(PRogram.TAB, indend) +
               "}";
    }

    public override void FullGrow(PRogram ctx, int targetDepth)
    {
        while (scope.GetDepth() < targetDepth) scope.Grow(ctx);

        scope.FullGrow(ctx, targetDepth);
    }
}

public class Assignment : Action, IGrowable
{
    public Assignment(Variable variable, Expression expression)
    {
        children = new List<Node> { variable, expression };
    }

    private Variable variable => (Variable)children[0];

    private Expression expression
    {
        get => (Expression)children[1];
        set => children[1] = value;
    }

    public void Grow(PRogram ctx)
    {
        expression = expression.Grown(ctx);
    }

    public static Assignment NewAssignment(PRogram ctx)
    {
        return new(Variable.RandomOrNew(ctx), Expression.NewExpression(ctx));
    }

    public override void Invoke(ProgramRunContext prc)
    {
        prc.variables[variable.name] = expression.Evaluate(prc);
    }

    public override string ToString()
    {
        return new string(PRogram.TAB, indend) + variable + " = " + expression + ';';
    }

    public override void FullGrow(PRogram ctx, int targetDepth)
    {
        while (expression.GetDepth() < targetDepth)
            Grow(ctx);
    }
}

public class Write : Action, IGrowable
{
    public Write(Expression expr)
    {
        children = new List<Node> { expr };
    }

    private Expression expression
    {
        get => (Expression)children[0];
        set => children[0] = value;
    }

    public void Grow(PRogram ctx)
    {
        expression = expression.Grown(ctx);
    }

    public static Write NewWrite(PRogram ctx)
    {
        return new(Expression.NewExpression(ctx));
    }

    public override void Invoke(ProgramRunContext prc)
    {
        prc.Push(expression.Evaluate(prc));
    }

    public override string ToString()
    {
        return new string(PRogram.TAB, indend) + "write(" + expression + ");";
    }

    public override void FullGrow(PRogram ctx, int targetDepth)
    {
        while (expression.GetDepth() < targetDepth)
            Grow(ctx);
    }
}

public class Scope : Node, IGrowable, IMutable
{
    public Scope()
    {
        children = new List<Node>();
    }

    public Scope(List<Node> children)
    {
        this.children = children;
    }

    public List<Action> actions => children.Select(c => c as Action).ToList();
    public List<IGrowable> Growables => actions.Where(x => x is IGrowable).Cast<IGrowable>().ToList();

    public void Grow(PRogram ctx)
    {
        children.Add(Action.NewAction(ctx));
    }

    public void Mutate(PRogram ctx)
    {
        var random = new Random();
        var expType = random.NextDouble();
        if (expType < ctx.config.MutateScopeRemoveChance)
        {
            if (actions.Count > 0) actions.RemoveAt(random.Next(0, actions.Count));
        }
        else
        {
            var n = children[ctx.rand.Next(0, children.Count)];
            children.Remove(n);
            children.Insert(ctx.rand.Next(0, children.Count), n);
        }
    }

    public void Add(Action action)
    {
        children.Add(action);
    }

    public void Invoke(ProgramRunContext prc)
    {
        foreach (var a in actions)
            a.Invoke(prc);
    }

    public override string ToString()
    {
        UpdateIndent();
        var s = "";
        foreach (var action in actions)
            s += action + "\n";
        return s;
    }

    public void FullGrow(PRogram ctx, int targetDepth)
    {
        while (GetDepth() < targetDepth)
        {
            var x = Growables;
            x.Add(this);
            x[ctx.rand.Next(0, x.Count)].Grow(ctx);
        }

        foreach (var action in actions)
            action.FullGrow(ctx, targetDepth - 1);
    }
}