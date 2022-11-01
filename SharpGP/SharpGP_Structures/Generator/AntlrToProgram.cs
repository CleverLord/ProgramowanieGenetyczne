using SharpGP_Structures.Tree;
using Action = SharpGP_Structures.Tree.Action;

namespace SharpGP_Structures.Generator;

public class AntlrToProgram : SharpBaseVisitor<Node> {
	public override Node VisitProgram(SharpParser.ProgramContext context)
	{
		Program program = new Program();
		foreach (var item in context.children) { program.AddAction((Action) item.Accept(this)); }
		return program;
	}
	public override Node VisitAction(SharpParser.ActionContext context) => Visit(context.GetChild(0));
	public override Node VisitScope(SharpParser.ScopeContext context)
	{
		Scope s = new Scope();
		foreach (var item in context.children)
			if (item is SharpParser.ActionContext)
				s.Add((Action) Visit(item as SharpParser.ActionContext));
		return s;
	}
	public override Node VisitLoop(SharpParser.LoopContext context) => new Loop((Constant) Visit(context.constant()), (Scope) Visit(context.scope()));
	public override Node VisitRead(SharpParser.ReadContext context) => new Read();
	public override Node VisitWrite(SharpParser.WriteContext context) => new Write((Expression) Visit(context.GetChild(2)));
	public override Node VisitIfStatement(SharpParser.IfStatementContext context) => new IfStatement((Condition) Visit(context.GetChild(1)), (Scope) Visit(context.GetChild(3)));
	public override Node VisitCondition(SharpParser.ConditionContext context) =>
		new Condition((Expression) Visit(context.GetChild(0)), (CompareOp) Visit(context.compareOp()), (Expression) Visit(context.GetChild(2)));
	public override Node VisitAssignment(SharpParser.AssignmentContext context) => new Assignment((Variable) Visit(context.variable()), (Expression) Visit(context.expression()));
	public override Node VisitExpression(SharpParser.ExpressionContext context) => Visit(context.GetChild(0)); 
	public override Node VisitNestedExp(SharpParser.NestedExpContext context) =>
		new NestedExpression((Expression) Visit(context.GetChild(1)), (Operator) Visit(context.operand()), (Expression) Visit(context.GetChild(3)));
	public override Node VisitVariable(SharpParser.VariableContext context) => new Variable(int.Parse(context.GetChild(0).GetText()));
	public override Node VisitConstant(SharpParser.ConstantContext context) => new Constant(int.Parse(context.GetChild(0).GetText()));
	public override Node VisitOperand(SharpParser.OperandContext context) => new Operator(context.GetChild(0).GetText());
	public override Node VisitCompareOp(SharpParser.CompareOpContext context) => new CompareOp(context.GetChild(0).GetText());
}