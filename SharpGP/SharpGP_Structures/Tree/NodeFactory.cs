namespace SharpGP_Structures.Tree;

public static class NodeFactory
{
    public static Action getNewAction(TreeConfig tc, PRogram pr)
    {
        double chance = pr.rand.NextDouble();
        if (chance < tc.NewAssignmentChance)
            return Assignment.NewAssignment(pr);
        else if (chance < tc.NewIfStatementChance)
            return IfStatement.NewIfStatement(pr);
        else if (chance < tc.NewLoopChance)
            return Loop.NewLoop(pr);
        //else if (chance < tc.NewWriteChance) //NewWriteChance is 1
        return Write.NewWrite(pr);
    }
    public static Expression getNewExpression(TreeConfig tc, PRogram pr)
    {
        double chance = pr.rand.NextDouble();
        if (chance < tc.NewVariableChance)
            return Variable.RandomOrNew(pr);
        else if (chance < tc.NewConstantChance)
            return Constant.NewConstant(pr);
        //else if (chance < tc.NewReadChance) //NewReadChance is 1
        return Read.NewRead(pr);
    }
}