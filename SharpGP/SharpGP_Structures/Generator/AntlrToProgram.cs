using SharpGP_Structures.Tree;

namespace SharpGP_Structures.Generator;

public class AntlrToProgram : SharpBaseVisitor<Node> {
	public override Node VisitProgram(SharpParser.ProgramContext context) { return base.VisitProgram(context); }
	public override Node VisitAction(SharpParser.ActionContext context) { return base.VisitAction(context); }
	public override Node VisitScope(SharpParser.ScopeContext context) { return base.VisitScope(context); }
	public override Node VisitLoop(SharpParser.LoopContext context) { return base.VisitLoop(context); }
	public override Node VisitRead(SharpParser.ReadContext context) { return base.VisitRead(context); }
	public override Node VisitWrite(SharpParser.WriteContext context)
	{
		return new Write( (Expression)VisitExpression(context.GetChild<SharpParser.ExpressionContext>(2)));
	}
	public override Node VisitIfStatement(SharpParser.IfStatementContext context) { return base.VisitIfStatement(context); }
	public override Node VisitCondition(SharpParser.ConditionContext context) { return base.VisitCondition(context); }
	public override Node VisitAssignment(SharpParser.AssignmentContext context) { return base.VisitAssignment(context); }
	public override Node VisitExpression(SharpParser.ExpressionContext context) { return base.VisitExpression(context); }
	public override Node VisitConstant(SharpParser.ConstantContext context) { return base.VisitConstant(context); }
	public override Node VisitVariable(SharpParser.VariableContext context) { return base.VisitVariable(context); }
	public override Node VisitOperand(SharpParser.OperandContext context) { return base.VisitOperand(context); }
	public override Node VisitCompareOp(SharpParser.CompareOpContext context) { return base.VisitCompareOp(context); }
}