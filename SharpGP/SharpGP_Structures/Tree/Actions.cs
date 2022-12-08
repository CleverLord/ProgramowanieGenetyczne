namespace SharpGP_Structures.Tree;

public abstract class Action : Node
{
    public abstract void Invoke(ProgramRunContext prc);

    public static Action NewAction(PRogram ctx)
    {
        Action? result = null;
        Type toCreate = ctx.config.ActionToCreate();
        if (toCreate == typeof(Assignment))
            result = Assignment.NewAssignment(ctx);
        if (toCreate == typeof(IfStatement))
            result = IfStatement.NewIfStatement(ctx);
        if (toCreate == typeof(Loop))
            result = Loop.NewLoop(ctx);
        if (toCreate == typeof(Write))
            result = Write.NewWrite(ctx);
        
        return result ?? Write.NewWrite(ctx); //write is just in case
    }

    public abstract void FullGrow(PRogram ctx, int targetDepth);
}

public class Loop : Action
{
    Constant repeatTimes => (Constant)children[0];
    Scope scope => (Scope)children[1];

    public Loop(Constant repeatTimes, Scope scope) => children = new List<Node> { repeatTimes, scope };
    public static Action NewLoop(PRogram ctx) => new Loop(Constant.NewConstant(ctx), new Scope());

    public override void Invoke(ProgramRunContext prc)
    {
        for (int i = 0; i < repeatTimes.value; i++)
            scope.Invoke(prc);
    }

    public override void FullGrow(PRogram ctx, int targetDepth)
    {
        while (scope.GetDepth() < targetDepth)
            scope.Grow(ctx);
        scope.FullGrow(ctx, targetDepth);
    }

    public override string ToString()
    {
        UpdateIndent();
        return new String(PRogram.TAB, indend) + "loop " + repeatTimes + " {\n" + scope +
               new String(PRogram.TAB, indend) + "}";
    }
}

public class IfStatement : Action
{
    Condition condition => (Condition)children[0];
    Scope scope => (Scope)children[1];

    public IfStatement(Condition condition, Scope scope) => children = new List<Node> { condition, scope };
    public static IfStatement NewIfStatement(PRogram ctx) => new IfStatement(Condition.NewCondition(ctx), new Scope());

    public override void Invoke(ProgramRunContext prc)
    {
        if (condition.Evaluate(prc))
            scope.Invoke(prc);
    }

    public override string ToString()
    {
        UpdateIndent();
        return new String(PRogram.TAB, indend) + "if (" + condition + "){\n" + scope + new String(PRogram.TAB, indend) +
               "}";
    }

    public override void FullGrow(PRogram ctx, int targetDepth)
    {
        while (scope.GetDepth() < targetDepth)
            scope.Grow(ctx);
        scope.FullGrow(ctx, targetDepth);
    }
}

public class Assignment : Action, IGrowable
{
    Variable variable => (Variable)children[0];

    Expression expression
    {
        get => (Expression)children[1];
        set => children[1] = value;
    }

    public Assignment(Variable variable, Expression expression) => children = new List<Node> { variable, expression };

    public static Assignment NewAssignment(PRogram ctx) =>
        new Assignment(Variable.RandomOrNew(ctx), Expression.NewExpression(ctx));

    public override void Invoke(ProgramRunContext prc) => prc.variables[variable.name] = expression.Evaluate(prc);
    public override string ToString() => new String(PRogram.TAB, indend) + variable + " = " + expression + ';';

    public void Grow(PRogram ctx) => expression = expression.Grown(ctx);

    public override void FullGrow(PRogram ctx, int targetDepth)
    {
        while (expression.GetDepth() < targetDepth)
            this.Grow(ctx);
    }
}

public class Write : Action, IGrowable
{
    Expression expression
    {
        get => (Expression)children[0];
        set => children[0] = value;
    }

    public Write(Expression expr) => children = new List<Node> { expr };
    public static Write NewWrite(PRogram ctx) => new Write(Expression.NewExpression(ctx));

    public override void Invoke(ProgramRunContext prc) => prc.Push(expression.Evaluate(prc));
    public override string ToString() => new String(PRogram.TAB, indend) + "write(" + expression + ");";

    public void Grow(PRogram ctx) => expression = expression.Grown(ctx);

    public override void FullGrow(PRogram ctx, int targetDepth)
    {
        while (expression.GetDepth() < targetDepth)
            this.Grow(ctx);
    }
}

public class Scope : Node, IGrowable, IMutable
{
    public List<Action> actions => children.Select(c => c as Action).ToList();
    public List<IGrowable> Growables => actions.Where(x => x is IGrowable).Cast<IGrowable>().ToList();

    public Scope() => children = new List<Node>();
    public Scope(List<Node> children) => this.children = children;

    public void Add(Action action) => children.Add(action);

    public void Invoke(ProgramRunContext prc)
    {
        foreach (var a in actions)
            a.Invoke(prc);
    }

    public override string ToString()
    {
        UpdateIndent();
        String s = "";
        foreach (var action in actions)
            s += action + "\n";
        return s;
    }

    public void Grow(PRogram ctx) => children.Add(Action.NewAction(ctx));

    public void FullGrow(PRogram ctx, int targetDepth)
    {
        while (GetDepth() < targetDepth)
        {
            var x = Growables;
            for (int i = 0; i < 10; i++)
            {
                Type t = ctx.config.TypeToGrow();
                var growable = Growables.Where(x => x.GetType() == t).ToList();
                if (growable.Count != 0)
                {
                    growable[ctx.rand.Next(growable.Count)].Grow(ctx);
                    UpdateParents();
                    return;
                }
            }
            x.Add(this);
            x[ctx.rand.Next(0, x.Count)].Grow(ctx);
        }

        foreach (var action in actions)
            action.FullGrow(ctx, targetDepth - 1);
    }

    public void Mutate(PRogram ctx) //mutate program node itself
    {
        Random random = new Random();
        double expType = random.NextDouble();
        if (expType < ctx.config.MutationRemoveChance)
        {
            if (actions.Count > 0)
                actions.RemoveAt(random.Next(0, actions.Count));
        }
        else
        {
            Node n = children[ctx.rand.Next(0, children.Count)];
            children.Remove(n);
            children.Insert(ctx.rand.Next(0, children.Count), n);
        }
    }
}