using SharpGP_Structures.Tree;
using Action = SharpGP_Structures.Tree.Action;
using static SharpParser;

namespace SharpGP_Structures.Generator;

public class AntlrToProgram : SharpBaseVisitor<Node> {
	public override Node VisitProgram(ProgramContext ctx) => new PRogram(ctx.children.Select(Visit).ToList());
	public override Node VisitAction(ActionContext ctx) => Visit(ctx.GetChild(0));
	public override Node VisitScope(ScopeContext ctx) => new Scope(ctx.children.Select(Visit).ToList().GetRange(1, ctx.children.Count - 2));
	public override Node VisitLoop(LoopContext ctx) => new Loop((Constant) Visit(ctx.constant()), (Scope) Visit(ctx.scope()));
	public override Node VisitRead(ReadContext ctx) => new Read();
	public override Node VisitWrite(WriteContext ctx) => new Write((Expression) Visit(ctx.GetChild(2)));
	public override Node VisitIfStatement(IfStatementContext ctx) => new IfStatement((Condition) Visit(ctx.GetChild(1)), (Scope) Visit(ctx.GetChild(3)));
	public override Node VisitCondition(ConditionContext ctx) => new Condition(Visit(ctx.GetChild(0)), Visit(ctx.comparator()), Visit(ctx.GetChild(2)));
	public override Node VisitAssignment(AssignmentContext ctx) => new Assignment((Variable) Visit(ctx.variable()), (Expression) Visit(ctx.expression()));
	public override Node VisitExpression(ExpressionContext ctx) => Visit(ctx.GetChild(0));
	public override Node VisitNestedExp(NestedExpContext ctx) => new NestedExpression(new List<Node>() {Visit(ctx.GetChild(1)), Visit(ctx.@operator()), Visit(ctx.GetChild(3))});
	public override Node VisitVariable(VariableContext ctx) => new Variable(int.Parse(ctx.GetText().Substring(2))); 	//following will only work for variables that have two characters and then a number
	public override Node VisitConstant(ConstantContext ctx) => new Constant(int.Parse(ctx.GetChild(0).GetText()));
	public override Node VisitOperator(OperatorContext ctx) => new Operator(ctx.GetChild(0).GetText());
	public override Node VisitComparator(ComparatorContext ctx) => new Comparator(ctx.GetChild(0).GetText());
}

