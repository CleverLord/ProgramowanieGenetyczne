using SharpGP_Structures.Tree;

namespace SharpGP_Structures;

public class TreeConfig
{
    // New Generation Data
    public int CopiedTreesFactor = 5;
    public int maxDepth = 6; // max tree depth

    // Run Data
    public int MaxOperationCount = 1000;

    public int maxVariableValue = 25; // max value of variables

    // Tree Basic Data
    public int minNodeCount = 15; // grow trees with at least 15 nodes

    // Tree Variables range
    public int minVariableValue = 0; // min value of variables
    public int NewTreesByCrossoverFactor = 45;
    public int NewTreesByMutationFactor = 30;

    [NonSerialized] public Random r = new();

    public TreeConfig()
    {
        PrecalculateGrow();
        PrecalculateMutation();
        PrecalculateCrossOver();
    }

    public Type ActionToCreate()
    {
        var chance = r.NextDouble();
        if (chance < NewAssignmentChance)
            return typeof(Assignment);
        if (chance < NewIfStatementChance)
            return typeof(IfStatement);
        if (chance < NewLoopChance)
            return typeof(Loop);
        if (chance < NewWriteChance)
            return typeof(Write);
        throw new Exception("Invalid chance");
    }

    public Type ExpressionToCreate()
    {
        var chance = r.NextDouble();
        if (chance < NewVariableChance)
            return typeof(Variable);
        if (chance < NewConstantChance)
            return typeof(Constant);
        if (chance < NewReadChance)
            return typeof(Read);
        throw new Exception("Invalid chance");
    }

    public Type TypeToMutate()
    {
        var chance = r.NextDouble();
        if (chance < MutateProgramChance)
            return typeof(PRogram);
        if (chance < MutateVariableChance)
            return typeof(Variable);
        if (chance < MutateConstantChance)
            return typeof(Constant);
        if (chance < MutateComparatorChance)
            return typeof(Comparator);
        if (chance < MutateOperatorChance)
            return typeof(Operator);
        if (chance < MutateScopeChance)
            return typeof(Scope);
        throw new Exception("Invalid chance");
    }

    public Type TypeToCrossOver()
    {
        var chance = r.NextDouble();
        if (chance < CrossoverLoopChance)
            return typeof(Loop);
        if (chance < CrossoverIfChance)
            return typeof(IfStatement);
        if (chance < CrossoverWriteChance)
            return typeof(Write);
        if (chance < CrossoverAssignmentChance)
            return typeof(Assignment);
        if (chance < CrossOverVariableChance)
            return typeof(Variable);
        if (chance < CrossOverConstantChance)
            return typeof(Constant);
        if (chance < CrossOverNestedExpressionChance)
            return typeof(NestedExpression);
        if (chance < CrossOverComparatorChance)
            return typeof(Comparator);
        if (chance < CrossOverOperatorChance)
            return typeof(Operator);
        if (chance < CrossOverScopeChance)
            return typeof(Scope);
        throw new Exception("Invalid chance");
    }

    #region Tree Generated Structure Data

    // Actions
    private readonly double NewAssignmentChanceFactor = 6;
    private readonly double NewIfStatementChanceFactor = 6;
    private readonly double NewLoopChanceFactor = 7;

    private readonly double NewWriteChanceFactor = 1;

    // Precalculated Values
    [NonSerialized] public double NewAssignmentChance;
    [NonSerialized] public double NewIfStatementChance;
    [NonSerialized] public double NewLoopChance;

    [NonSerialized] public double NewWriteChance;

    // Expressions
    private readonly double NewVariableChanceFactor = 2;
    private readonly double NewConstantChanceFactor = 6;

    private readonly double NewReadChanceFactor = 1;

    // Precalculated Values
    [NonSerialized] public double NewVariableChance;
    [NonSerialized] public double NewConstantChance;
    [NonSerialized] public double NewReadChance;

    #endregion

    #region Tree Evolution Data

    [NonSerialized] public double MutationChance = 0.1;
    public double CrossoverChance => 1 - MutationChance;

    #region Mutation

    private readonly double MutateProgramFactor = 0.5;
    private readonly double MutateVariableFactor = 0.5;
    private readonly double MutateConstantFactor = 0.5;
    private readonly double MutateComparatorFactor = 0.5;
    private readonly double MutateOperatorFactor = 0.5;

    private readonly double MutateScopeFactor = 0.5;

    //Precalculated Values
    [NonSerialized] public double MutateProgramChance;
    [NonSerialized] public double MutateVariableChance;
    [NonSerialized] public double MutateConstantChance;
    [NonSerialized] public double MutateComparatorChance;
    [NonSerialized] public double MutateOperatorChance;

    [NonSerialized] public double MutateScopeChance;

    //scope
    private readonly double MutateScopeAddFactor = 2;

    private readonly double MutateScopeRemoveFactor = 8;

    //Precalculated Values
    [NonSerialized] public double MutateScopeRemoveChance;
    public double MutateScopeAddChance => 1 - MutateScopeRemoveChance;

    #endregion

    #region Crossover

    private readonly double CrossoverLoopFactor = 25;
    private readonly double CrossoverIfFactor = 25;
    private readonly double CrossoverWriteFactor = 0; //basically a nexted expression crossover
    private readonly double CrossoverAssignmentFactor = 25;
    private readonly double CrossOverVariableFactor = 0; //basically a mutation
    private readonly double CrossOverConstantFactor = 0; //basically a mutation
    private readonly double CrossOverNestedExpressionFactor = 25;
    private readonly double CrossOverComparatorFactor = 0; //basically a mutation
    private readonly double CrossOverOperatorFactor = 0; //basically a mutation
    private readonly double CrossOverScopeFactor = 0; //more sense is to crossover loop or if

    //Precalculated Values
    [NonSerialized] public double CrossoverLoopChance;
    [NonSerialized] public double CrossoverIfChance;
    [NonSerialized] public double CrossoverWriteChance;
    [NonSerialized] public double CrossoverAssignmentChance;
    [NonSerialized] public double CrossOverVariableChance;
    [NonSerialized] public double CrossOverConstantChance;
    [NonSerialized] public double CrossOverNestedExpressionChance;
    [NonSerialized] public double CrossOverComparatorChance;
    [NonSerialized] public double CrossOverOperatorChance;
    [NonSerialized] public double CrossOverScopeChance;

    #endregion

    #endregion

    #region Precalculators

    public void PrecalculateGrow()
    {
        PrecalculateGrowActions();
        PrecalculateGrowExpressions();
    }

    public void PrecalculateGrowActions()
    {
        // Sum all chances
        var sum = NewAssignmentChanceFactor + NewIfStatementChanceFactor + NewLoopChanceFactor + NewWriteChanceFactor;
        // Calculate chances and offset them
        NewAssignmentChance = NewAssignmentChanceFactor / sum;
        NewIfStatementChance = NewIfStatementChanceFactor / sum + NewAssignmentChance;
        NewLoopChance = NewLoopChanceFactor / sum + NewIfStatementChance;
        NewWriteChance = NewWriteChanceFactor / sum + NewLoopChance;
    }

    public void PrecalculateGrowExpressions()
    {
        // Sum all chances
        var sum = NewVariableChanceFactor + NewConstantChanceFactor + NewReadChanceFactor;
        // Calculate chances and offset them
        NewVariableChance = NewVariableChanceFactor / sum;
        NewConstantChance = NewConstantChanceFactor / sum + NewVariableChance;
        NewReadChance = NewReadChanceFactor / sum + NewConstantChance;
    }

    public void PrecalculateMutation()
    {
        //sum all chances
        var sum = MutateProgramFactor + MutateVariableFactor + MutateConstantFactor + MutateComparatorFactor +
                  MutateOperatorFactor + MutateScopeFactor;
        //calculate chances and offset them
        MutateProgramChance = MutateProgramFactor / sum;
        MutateVariableChance = MutateVariableFactor / sum + MutateProgramChance;
        MutateConstantChance = MutateConstantFactor / sum + MutateVariableChance;
        MutateComparatorChance = MutateComparatorFactor / sum + MutateConstantChance;
        MutateOperatorChance = MutateOperatorFactor / sum + MutateComparatorChance;
        MutateScopeChance = MutateScopeFactor / sum + MutateOperatorChance;

        MutateScopeRemoveChance = MutateScopeRemoveFactor / (MutateScopeRemoveFactor + MutateScopeAddFactor);
    }

    public void PrecalculateCrossOver()
    {
        //sum all chances
        var sum = CrossoverLoopFactor + CrossoverIfFactor + CrossoverWriteFactor + CrossoverAssignmentFactor +
                  CrossOverVariableFactor + CrossOverConstantFactor + CrossOverNestedExpressionFactor +
                  CrossOverComparatorFactor + CrossOverOperatorFactor + CrossOverScopeFactor;
        //calculate chances and offset them
        CrossoverLoopChance = CrossoverLoopFactor / sum;
        CrossoverIfChance = CrossoverIfFactor / sum + CrossoverLoopChance;
        CrossoverWriteChance = CrossoverWriteFactor / sum + CrossoverIfChance;
        CrossoverAssignmentChance = CrossoverAssignmentFactor / sum + CrossoverWriteChance;
        CrossOverVariableChance = CrossOverVariableFactor / sum + CrossoverAssignmentChance;
        CrossOverConstantChance = CrossOverConstantFactor / sum + CrossOverVariableChance;
        CrossOverNestedExpressionChance = CrossOverNestedExpressionFactor / sum + CrossOverConstantChance;
        CrossOverComparatorChance = CrossOverComparatorFactor / sum + CrossOverNestedExpressionChance;
        CrossOverOperatorChance = CrossOverOperatorFactor / sum + CrossOverComparatorChance;
        CrossOverScopeChance = CrossOverScopeFactor / sum + CrossOverOperatorChance;
    }

    #endregion
}