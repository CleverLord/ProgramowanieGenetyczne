namespace SharpGP_Structures.Tree;

public abstract class Action : Node {
	public abstract void Invoke();
	public static Action NewAction(Program ctx)
	{
		Action? result = null;
		switch (Program.rand.Next(0, 4))
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

public class Loop : Action, IGrowable {
	Constant repeatTimes => (Constant) children[0];
	Scope scope => (Scope) children[1];
	public override string ToString()
	{
		UpdateIndent();
		return new String('\t', indend) + "loop " + repeatTimes + scope;
	}
	public override void Invoke()
	{
		for (int i = 0; i < repeatTimes.value; i++) scope.Invoke();
	}
	public Loop(Constant repeatTimes, Scope scope) => children = new List<Node> {repeatTimes, scope};
	public static Action NewLoop(Program ctx) => new Loop(Constant.NewConstant(ctx), new Scope());
	public void Grow(Program ctx) => scope.Grow(ctx);
}

public class IfStatement : Action {
	Condition condition => (Condition) children[0];
	Scope scope => (Scope) children[1];
	public override string ToString()
	{
		UpdateIndent();
		return new String('\t', indend) + "if ( " + condition + ")" + scope;
	}
	public override void Invoke()
	{
		if (condition.Evaluate()) scope.Invoke();
	}
	public IfStatement(Condition condition, Scope scope) => children = new List<Node> {condition, scope};
	public static IfStatement NewIfStatement(Program ctx) => new IfStatement(Condition.NewCondition(ctx), new Scope());
}

public class Assignment : Action, IGrowable {
	Variable variable => (Variable) children[0];
	Expression expression => (Expression) children[1];
	public override string ToString()
	{
		UpdateIndent();
		return new String('\t', indend) + variable + " = " + expression;
	}
	public override void Invoke() => variable.value = expression.Evaluate();
	public Assignment(Variable variable, Expression expression) => children = new List<Node> {variable, expression};
	public static Assignment NewAssignment(Program ctx) => new Assignment(Variable.RandomOrNew(ctx), Expression.NewExpression(ctx));
	public void Grow(Program ctx) => expression.Grow(ctx);
}

public class Write : Action {
	Expression expression => (Expression) children[0];
	public Write(Expression expr) => children = new List<Node> {expr};
	public override string ToString()
	{
		UpdateIndent();
		return new String('\t', indend) + expression;
	}
	public override void Invoke() => expression.Evaluate();
	public static Write NewWrite(Program ctx) => new Write(Expression.NewExpression(ctx));
}

public class Scope : Node, IGrowable {
	public List<Action> actions => children.Select(c => c as Action).ToList();
	public override string ToString()
	{
		UpdateIndent();
		string s = new String('\t', indend) + "{";
		foreach (var action in actions) s += action.ToString();
		s += new String('\t', indend) + "}";
		return s;
	}
	public void Invoke() => actions.ForEach(a => a.Invoke());
	public void Grow(Program ctx)
	{
		List<IGrowable> growables = children.Where(c => c is IGrowable).Select(c => c as IGrowable).ToList();
		int target = Program.rand.Next(-1, growables.Count);
		if (target == -1) { children.Add(Action.NewAction(ctx)); }
		else { growables[target].Grow(ctx); }
	}
}