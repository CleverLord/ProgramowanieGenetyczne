namespace SharpGP_Core.Tree;

public abstract class Action : Node {
	public abstract void Invoke();
	
	public static Action NewAction(Program ctx)
	{
		Action result=null;
		switch (Generator.Generator.r.Next(0, 4)) {
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
		return result;
	}
}

public class Loop : Action {
	Constant repeatTimes
	{
		get => (Constant) children[0];
		set => children[0] = value.GetType() == typeof(Constant) ? value : children[0];
	}
	Scope scope
	{
		get => (Scope) children[1];
		set => children[1] = value.GetType() == typeof(Scope) ? value : children[1];
	}

	public override string ToString()
	{
		UpdateIndent();
		return new String('\t', indend) + "loop " + repeatTimes + scope;
	}

	public override void Invoke()
	{
		for (int i = 0; i < repeatTimes.value; i++) scope.Invoke();
	}

	public Loop(Constant repeatTimes, Scope scope)
	{
		this.repeatTimes = repeatTimes;
		this.scope = scope;
	}

	public static Action NewLoop(Program ctx)
	{
		return new Loop(Constant.NewConstant(ctx), new Scope());
	}
}

public class IfStatement : Action {
	Condition condition
	{
		get => (Condition) children[0];
		set => children[0] = value.GetType() == typeof(Condition) ? value : children[0];
	}
	Scope scope
	{
		get => (Scope) children[1];
		set => children[1] = value.GetType() == typeof(Scope) ? value : children[1];
	}

	public override string ToString()
	{
		UpdateIndent();
		return new String('\t', indend) + "if ( " + condition + ")" + scope;
	}

	public override void Invoke()
	{
		if (condition.Evaluate()) scope.Invoke();
	}

	public IfStatement(Condition condition, Scope scope)
	{
		this.condition = condition;
		this.scope = scope;
	}

	public static IfStatement NewIfStatement(Program ctx)
	{
		Condition condition = Condition.NewCondition(ctx);
		return new IfStatement(condition, new Scope());
	}
}

public class Assignment : Action {
	Variable variable
	{
		get => (Variable) children[0];
		set => children[0] = value.GetType() == typeof(Variable) ? value : children[0];
	}
	Expression expression
	{
		get => (Expression) children[1];
		set => children[1] = value.GetType() == typeof(Expression) ? value : children[1];
	}

	public override string ToString()
	{
		UpdateIndent();
		return new String('\t', indend) + variable + " = " + expression;
	}
	public override void Invoke()
	{
		variable.value = expression.Evaluate();
	}
	public Assignment(Variable variable, Expression expression)
	{
		this.variable = variable;
		this.expression = expression;
	}
	public static Assignment NewAssignment(Program ctx)
	{
		Variable var = Variable.RandomOrNew(ctx);
		Expression expr = Expression.NewExpression(ctx);

		return new Assignment(var, expr);
	}
	public override void Grow(Program ctx)
	{
		expression.Grow(ctx);
	}
}

public class Write : Action {
	Expression expression
	{
		get => (Expression) children[0];
		set => children[0] = value.GetType() == typeof(Expression) ? value : children[0];
	}
	public Write(Expression expr)
	{
		expression = expr;
	}

	public override string ToString()
	{
		UpdateIndent();
		return new String('\t', indend) + expression + " = " + expression;
	}
	public override void Invoke()
	{
		expression.Evaluate();
	}
	public static Write NewWrite(Program ctx)
	{
		Expression expr = Expression.NewExpression(ctx);
		return new Write(expr);
	}
}

public class Scope : Node {
	public List<Action> actions => children.Select(c => c as Action).ToList();

	public override string ToString()
	{
		UpdateIndent();
		string s = new String('\t', indend) + "{";
		foreach (var action in actions) s += action.ToString();
		s += new String('\t', indend) + "}";
		return s;
	}
	public void Invoke()
	{
		actions.ForEach(a => a.Invoke());
	}
	public override void Grow(Program ctx)
	{
		int target = Generator.Generator.r.Next(-1, children.Count);
		if(target == -1) {
			children.Add(Action.NewAction(ctx));
		} else {
			actions[target].Grow(ctx);
		}
	}
}