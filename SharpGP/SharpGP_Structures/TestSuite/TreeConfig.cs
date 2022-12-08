using SharpGP_Structures.Tree;

namespace SharpGP_Structures;

public class TreeConfig
{
    // Tree Basic Data
    public int minNodeCount = 15; // grow trees with at least 15 nodes
    public int maxDepth = 6; // max tree depth
    
    // Tree Variables range
    public int minVariableValue = 0; // min value of variables
    public int maxVariableValue = 25; // max value of variables
    
    #region Tree Structure Generation Data
    // Actions
    private double NewAssignmentChanceFactor = 6;
    private double NewIfStatementChanceFactor = 6;
    private double NewLoopChanceFactor = 7;
    private double NewWriteChanceFactor = 1;
    // Precalculated Values
    [NonSerialized] public double NewAssignmentChance;
    [NonSerialized] public double NewIfStatementChance;
    [NonSerialized] public double NewLoopChance;
    [NonSerialized] public double NewWriteChance;
    // Expressions
    private double NewVariableChanceFactor = 2;
    private double NewConstantChanceFactor = 6;
    private double NewReadChanceFactor = 1;
    // Precalculated Values
    [NonSerialized] public double NewVariableChance;
    [NonSerialized] public double NewConstantChance;
    [NonSerialized] public double NewReadChance;
    // Grow
    private double GrowProgramChanceFactor = 5;
    private double GrowIfStatementChanceFactor = 2;
    private double GrowWriteChanceFactor = 2;
    private double GrowAssignmentChanceFactor = 2;
    private double GrowNestedExpressionChanceFactor = 2;
    private double GrowConditionChanceFactor = 2;
    private double GrowScopeChanceFactor = 5;
    
    // Precalculated Values
    [NonSerialized] public double GrowProgramChance;
    [NonSerialized] public double GrowIfStatementChance;
    [NonSerialized] public double GrowWriteChance;
    [NonSerialized] public double GrowAssignmentChance;
    [NonSerialized] public double GrowNestedExpressionChance;
    [NonSerialized] public double GrowConditionChance;
    [NonSerialized] public double GrowScopeChance;
    #endregion
    
    #region Tree Evolution Data
    public double MutationChance = 0.1;
    //public double CrossoverChance => 1 - MutationChance;
    
    #region Mutation
    private double MutateProgramFactor = 0.5;
    private double MutateVariableFactor = 0.5;
    private double MutateConstantFactor = 0.5;
    private double MutateComparatorFactor = 0.5;
    private double MutateOperatorFactor = 0.5;
    private double MutateScopeFactor = 0.1;
    //Precalculated Values
    [NonSerialized] public double MutateProgramChance;
    [NonSerialized] public double MutateVariableChance;
    [NonSerialized] public double MutateConstantChance;
    [NonSerialized] public double MutateComparatorChance;
    [NonSerialized] public double MutateOperatorChance;
    [NonSerialized] public double MutateScopeChance;
    #endregion
    private double MutationRemoveChanceFactor = 0.5;
    private double MutationSwapChanceFactor = 0.5;
    //Precalculated Values
    [NonSerialized] public double MutationRemoveChance;
    //public double MutationAddChance => 1 - MutationRemoveChance;
    #region Crossover
    private double CrossoverLoopFactor = 25;
    private double CrossoverIfFactor = 25;
    private double CrossoverWriteFactor = 0; //basically a nexted expression crossover
    private double CrossoverAssignmentFactor = 25;
    private double CrossOverVariableFactor = 0; //basically a mutation
    private double CrossOverConstantFactor = 0; //basically a mutation
    private double CrossOverNestedExpressionFactor = 25;
    private double CrossOverComparatorFactor = 0; //basically a mutation
    private double CrossOverOperatorFactor = 0; //basically a mutation
    private double CrossOverScopeFactor = 0; //more sense is to crossover loop or if
    
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

    // New Generation Data
    public int CopiedTreesFactor = 5;
    public int NewTreesByMutationFactor = 30;
    public int NewTreesByCrossoverFactor = 45;
    
    // Run Data
    public int MaxOperationCount = 1000;
    
    public TreeConfig()
    {
        PrecalculateGrow();
        PrecalculateMutation();
        PrecalculateCrossOver();
    }

    #region Precalculators
    public void PrecalculateGrow()
    {
        PrecalculateGrowActions();
        PrecalculateGrowExpressions();
        PrecalculateGrowProgram();
    }
    public void PrecalculateGrowActions()
    {
        // Sum all chances
        double sum = NewAssignmentChanceFactor + NewIfStatementChanceFactor + NewLoopChanceFactor + NewWriteChanceFactor;
        // Calculate chances and offset them
        NewAssignmentChance = NewAssignmentChanceFactor / sum;
        NewIfStatementChance = NewIfStatementChanceFactor / sum + NewAssignmentChance;
        NewLoopChance = NewLoopChanceFactor / sum + NewIfStatementChance;
        NewWriteChance = NewWriteChanceFactor / sum + NewLoopChance;
    }
    public void PrecalculateGrowExpressions()
    {
        // Sum all chances
        double sum = NewVariableChanceFactor + NewConstantChanceFactor + NewReadChanceFactor;
        // Calculate chances and offset them
        NewVariableChance = NewVariableChanceFactor / sum;
        NewConstantChance = NewConstantChanceFactor / sum + NewVariableChance;
        NewReadChance = NewReadChanceFactor / sum + NewConstantChance;
    }

    public void PrecalculateGrowProgram()
    {
        //Sum all chances
        double sum = GrowProgramChanceFactor + GrowIfStatementChanceFactor + GrowWriteChanceFactor + GrowAssignmentChanceFactor + GrowNestedExpressionChanceFactor + GrowConditionChanceFactor + GrowScopeChanceFactor;
        // Calculate chances and offset them
        GrowProgramChance = GrowProgramChanceFactor / sum;
        GrowIfStatementChance = GrowIfStatementChanceFactor / sum + GrowProgramChance;
        GrowWriteChance = GrowWriteChanceFactor / sum + GrowIfStatementChance;
        GrowAssignmentChance = GrowAssignmentChanceFactor / sum + GrowWriteChance;
        GrowNestedExpressionChance = GrowNestedExpressionChanceFactor / sum + GrowAssignmentChance;
        GrowConditionChance = GrowConditionChanceFactor / sum + GrowNestedExpressionChance;
        GrowScopeChance = GrowScopeChanceFactor / sum + GrowConditionChance;
    }
    public void PrecalculateMutation()
    {
        //sum all chances
        double sum = MutateProgramFactor + MutateVariableFactor + MutateConstantFactor + MutateComparatorFactor + MutateOperatorFactor + MutateScopeFactor;
        //calculate chances and offset them
        MutateProgramChance = MutateProgramFactor / sum;
        MutateVariableChance = MutateVariableFactor / sum + MutateProgramChance;
        MutateConstantChance = MutateConstantFactor / sum + MutateVariableChance;
        MutateComparatorChance = MutateComparatorFactor / sum + MutateConstantChance;
        MutateOperatorChance = MutateOperatorFactor / sum + MutateComparatorChance;
        MutateScopeChance = MutateScopeFactor / sum + MutateOperatorChance;
        
        MutationRemoveChance = MutationRemoveChanceFactor / (MutationRemoveChanceFactor + MutationSwapChanceFactor);
    }
    public void PrecalculateCrossOver()
    {
        //sum all chances
        double sum = CrossoverLoopFactor + CrossoverIfFactor + CrossoverWriteFactor + CrossoverAssignmentFactor + CrossOverVariableFactor + CrossOverConstantFactor + CrossOverNestedExpressionFactor + CrossOverComparatorFactor + CrossOverOperatorFactor + CrossOverScopeFactor;
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

    [NonSerialized] public Random r = new Random();
    public Type ActionToCreate()
    {
        double chance = r.NextDouble();
        if (chance < NewAssignmentChance)
            return typeof(Assignment);
        else if (chance < NewIfStatementChance)
            return typeof(IfStatement);
        else if (chance < NewLoopChance)
            return typeof(Loop);
        else if (chance < NewWriteChance)
            return typeof(Write);
        else
            throw new Exception("Invalid chance");
    }
    public Type ExpressionToCreate()
    {
        double chance = r.NextDouble();
        if (chance < NewVariableChance)
            return typeof(Variable);
        else if (chance < NewConstantChance)
            return typeof(Constant);
        else if (chance < NewReadChance)
            return typeof(Read);
        else
            throw new Exception("Invalid chance");
    }
    public Type TypeToMutate()
    {
        double chance = r.NextDouble();
        if (chance < MutateProgramChance)
            return typeof(PRogram);
        else if (chance < MutateVariableChance)
            return typeof(Variable);
        else if (chance < MutateConstantChance)
            return typeof(Constant);
        else if (chance < MutateComparatorChance)
            return typeof(Comparator);
        else if (chance < MutateOperatorChance)
            return typeof(Operator);
        else if (chance < MutateScopeChance)
            return typeof(Scope);
        else
            throw new Exception("Invalid chance");
    }
    public Type TypeToCrossOver()
    {
        double chance = r.NextDouble();
        if (chance < CrossoverLoopChance)
            return typeof(Loop);
        else if (chance < CrossoverIfChance)
            return typeof(IfStatement);
        else if (chance < CrossoverWriteChance)
            return typeof(Write);
        else if (chance < CrossoverAssignmentChance)
            return typeof(Assignment);
        else if (chance < CrossOverVariableChance)
            return typeof(Variable);
        else if (chance < CrossOverConstantChance)
            return typeof(Constant);
        else if (chance < CrossOverNestedExpressionChance)
            return typeof(NestedExpression);
        else if (chance < CrossOverComparatorChance)
            return typeof(Comparator);
        else if (chance < CrossOverOperatorChance)
            return typeof(Operator);
        else if (chance < CrossOverScopeChance)
            return typeof(Scope);
        else
            throw new Exception("Invalid chance");
    }
    public Type TypeToGrow()
    {
        double chance = r.NextDouble();
        if(chance<GrowProgramChance)
            return typeof(PRogram);
        else if (chance < GrowIfStatementChance)
            return typeof(IfStatement);
        else if (chance < GrowWriteChance)
            return typeof(Write);
        else if (chance < GrowAssignmentChance)
            return typeof(Assignment);
        else if (chance < GrowNestedExpressionChance)
            return typeof(NestedExpression);
        else if (chance < GrowConditionChance)
            return typeof(Condition);
        else if (chance < GrowScopeChance)
            return typeof(Scope);
        else
            throw new Exception("Invalid chance");
    }
}