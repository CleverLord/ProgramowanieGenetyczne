namespace SharpGP_Structures.Tree;

public abstract class Action : Node {
	public abstract void Invoke(ProgramRunContext prc);
	public static Action NewAction(Program ctx)
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
}

public class Loop : Action {
	Constant repeatTimes => (Constant) children[0];
	Scope scope => (Scope) children[1];
	public override string ToString()
	{
		UpdateIndent();
		return new String(Program.TAB, indend) + "loop " + repeatTimes + " {\n" + scope + new String(Program.TAB, indend) + "}";
	}
	public override void Invoke(ProgramRunContext prc)
	{
		for (int i = 0; i < repeatTimes.value; i++) scope.Invoke(prc);
	}
	public Loop(Constant repeatTimes, Scope scope) => children = new List<Node> {repeatTimes, scope};
	public static Action NewLoop(Program ctx) => new Loop(Constant.NewConstant(ctx), new Scope());
}

public class IfStatement : Action {
	Condition condition => (Condition) children[0];
	Scope scope => (Scope) children[1];
	public override string ToString()
	{
		UpdateIndent();
		return new String(Program.TAB, indend) + "if (" + condition + "){\n" + scope + new String(Program.TAB, indend) + "}";
	}
	public override void Invoke(ProgramRunContext prc)
	{
		if (condition.Evaluate(prc)) scope.Invoke(prc);
	}
	public IfStatement(Condition condition, Scope scope) => children = new List<Node> {condition, scope};
	public static IfStatement NewIfStatement(Program ctx) => new IfStatement(Condition.NewCondition(ctx), new Scope());
}

public class Assignment : Action, IGrowable {
	Variable variable => (Variable) children[0];
	Expression expression { get => (Expression) children[1]; set => children[1] = value; }
	public override string ToString()
	{
		UpdateIndent();
		return new String(Program.TAB, indend) + variable + " = " + expression + ';';
	}
	public override void Invoke(ProgramRunContext prc) => prc.variables[variable.name] = expression.Evaluate(prc);
	public Assignment(Variable variable, Expression expression) => children = new List<Node> {variable, expression};
	public static Assignment NewAssignment(Program ctx) => new Assignment(Variable.RandomOrNew(ctx), Expression.NewExpression(ctx));
	public void Grow(Program ctx) => expression = expression.Grown(ctx);
}

public class Write : Action, IGrowable {
	Expression expression { get => (Expression) children[0]; set => children[0] = value; }
	public Write(Expression expr) => children = new List<Node> {expr};
	public override string ToString()
	{
		return new String(Program.TAB, indend) + "write(" + expression + ");";
	}
	public override void Invoke(ProgramRunContext prc) => expression.Evaluate(prc);
	public static Write NewWrite(Program ctx) => new Write(Expression.NewExpression(ctx));
	public void Grow(Program ctx) => expression = expression.Grown(ctx);
}

public class Scope : Node, IGrowable {
	public List<Action> actions => children.Select(c => c as Action).ToList();
	public override string ToString()
	{
		UpdateIndent();
		String s = "";
		foreach (var action in actions) s += action + "\n";
		return s;
	}
	public Scope() => children = new List<Node>();
	public Scope(List<Node> children) => this.children = children;
	public void Add(Action action) => children.Add(action);
	public void Invoke(ProgramRunContext prc) => actions.ForEach(a => a.Invoke(prc));
	public void Grow(Program ctx) => children.Add(Action.NewAction(ctx));
}