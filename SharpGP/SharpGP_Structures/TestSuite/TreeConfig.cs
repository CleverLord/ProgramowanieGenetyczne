namespace SharpGP_Structures;

public class TreeConfig
{
    //Tree Basic Data
    public int minNodeCount = 15; // grow trees with at least 15 nodes
    public int maxDepth = 6; //max tree depth
    
    //Tree Variables range
    public int minVariableValue = 0; //min value of variables
    public int maxVariableValue = 25; //max value of variables
    
    //Tree Generated Structure Data
    //Actions
    public double NewAssignmentChanceFactor = 6;
    public double NewIfStatementChanceFactor = 6;
    public double NewLoopChanceFactor = 7;
    public double NewWriteChanceFactor = 1;
    //Precalculated Values
    [NonSerialized] public double NewAssignmentChance;
    [NonSerialized] public double NewIfStatementChance;
    [NonSerialized] public double NewLoopChance;
    [NonSerialized] public double NewWriteChance;
    
    //Tree Evolution Data
    public double MutationChance = 0.1;
    public double CrossoverChance => 1 - MutationChance;

    public TreeConfig()
    {
        PrecalculateActions();
    }
    
    public void PrecalculateActions()
    {
        //Sum all chances
        double sum = NewAssignmentChanceFactor + NewIfStatementChanceFactor + NewLoopChanceFactor + NewWriteChanceFactor;
        //Calculate chances and offset them
        NewAssignmentChance = NewAssignmentChanceFactor / sum;
        NewIfStatementChance = NewIfStatementChanceFactor / sum + NewAssignmentChance;
        NewLoopChance = NewLoopChanceFactor / sum + NewIfStatementChance;
        NewWriteChance = NewWriteChanceFactor / sum + NewLoopChance;
    }
}