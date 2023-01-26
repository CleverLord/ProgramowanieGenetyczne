using SharpGP_Structures.Tree;

namespace SharpGP_Structures.Evolution;

[Serializable]
public class EvolutionHistory
{
    public List<EvolutionGeneration> generations = new List<EvolutionGeneration>();
    public string totalEvolutionTime = "-1";
    public Dictionary<string, int> totalActionsToCount = new Dictionary<string, int>();
    public void AddActionToCount(string actionType, int actionCount)
    {
        if (!totalActionsToCount.ContainsKey(actionType)) totalActionsToCount.Add(actionType, 0);
        totalActionsToCount[actionType] += actionCount;
    }
}

[Serializable]
public class EvolutionGeneration
{
    public int generationIndex = -1;
    public string generationStartTime;
    public long generationCreationTime = -1;
    public long generationEvaluationTime = -1;
    public string gradingFunction;
    public List<double> fitnesses = new List<double>(); // mark for each individual in the generation
    public Dictionary<double, int> fitnesToUnits = new Dictionary<double, int>();
    public List<int> populationDepths = new List<int>(); // depth of each individual in the generation
    public Dictionary<int, int> popDepthToUnits = new Dictionary<int, int>();
    public List<GeneticAction> actions = new List<GeneticAction>(); // actions taken to create each individual in the generation
    public Dictionary<string, int> actionsToCount = new Dictionary<string, int>();
    public string bestProgram;
    public EvolutionGeneration()
    {
        generationStartTime = DateTime.Now.ToString("dd/MM/yy HH:mm:ss.fff");
    }
    public void SetFittness(List<double> f)
    {
        fitnesses = f;
        fitnesToUnits.Clear();
        foreach (var d in f)
        {
            if (!fitnesToUnits.ContainsKey(d)) fitnesToUnits.Add(d, 0);
            fitnesToUnits[d]++;
        }
    }
    public void setDepths(List<int> d)
    {
        populationDepths = d;
        popDepthToUnits.Clear();
        foreach (var i in d)
        {
            if (!popDepthToUnits.ContainsKey(i)) popDepthToUnits.Add(i, 0);
            popDepthToUnits[i]++;
        }
    }
    public void PostProcess()
    {
        foreach (var action in actions)
        {
            if (!actionsToCount.ContainsKey(action.actionType)) 
                actionsToCount.Add(action.actionType, 0);
            actionsToCount[action.actionType]++;
        }
    }
}

[Serializable]
public abstract class GeneticAction
{
    public string actionType;
}

[Serializable]
public class CrossoverAction : GeneticAction
{
    public CrossoverAction()
    {
        actionType = "Crossover";
    }
    public string crossedType;
    public int parent1Depth;
    public int parent2Depth;
    public int child1Depth;
    public int child2Depth;
}

[Serializable]
public class MutationAction : GeneticAction
{
    public MutationAction()
    {
        actionType = "Mutation";
    }
    public string mutatedGene;
}

[Serializable]
public class CopyZeroAction : GeneticAction
{
    public CopyZeroAction()
    {
        actionType = "CopyZero";
    }
    public int count;
}