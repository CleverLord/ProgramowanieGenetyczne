using SharpGP_Structures.Tree;
using static SharpParser;

namespace SharpGP_Structures.Generator;

public class AntlrToProgram : SharpBaseVisitor<Node>
{
    public override Node VisitProgram(ProgramContext ctx)
    {
        return new PRogram(ctx.children.Select(Visit).ToList());
    }

    public override Node VisitAction(ActionContext ctx)
    {
        return Visit(ctx.GetChild(0));
    }

    public override Node VisitScope(ScopeContext ctx)
    {
        return new Scope(ctx.children.Select(Visit).ToList());
    }

    public override Node VisitLoop(LoopContext ctx)
    {
        return new Loop((Constant)Visit(ctx.constant()), (Scope)Visit(ctx.scope()));
    }

    public override Node VisitRead(ReadContext ctx)
    {
        return new Read();
    }

    public override Node VisitWrite(WriteContext ctx)
    {
        return new Write((Expression)Visit(ctx.GetChild(2)));
    }

    public override Node VisitIfStatement(IfStatementContext ctx)
    {
        return new IfStatement((Condition)Visit(ctx.GetChild(1)), (Scope)Visit(ctx.GetChild(3)));
    }

    public override Node VisitCondition(ConditionContext ctx)
    {
        return new Condition(Visit(ctx.GetChild(0)), Visit(ctx.comparator()), Visit(ctx.GetChild(2)));
    }

    public override Node VisitAssignment(AssignmentContext ctx)
    {
        return new Assignment((Variable)Visit(ctx.variable()), (Expression)Visit(ctx.expression()));
    }

    public override Node VisitExpression(ExpressionContext ctx)
    {
        return Visit(ctx.GetChild(0));
    }

    public override Node VisitNestedExp(NestedExpContext ctx)
    {
        return new NestedExpression(new List<Node>
            { Visit(ctx.GetChild(1)), Visit(ctx.@operator()), Visit(ctx.GetChild(3)) });
    }

    public override Node VisitVariable(VariableContext ctx)
    {
        return new Variable(int.Parse(ctx.GetText().Substring(2)));
        //following will only work for variables that have two characters and then a number
    }

    public override Node VisitConstant(ConstantContext ctx)
    {
        return new Constant(int.Parse(ctx.GetChild(0).GetText()));
    }

    public override Node VisitOperator(OperatorContext ctx)
    {
        return new Operator(ctx.GetChild(0).GetText());
    }

    public override Node VisitComparator(ComparatorContext ctx)
    {
        return new Comparator(ctx.GetChild(0).GetText());
    }
}