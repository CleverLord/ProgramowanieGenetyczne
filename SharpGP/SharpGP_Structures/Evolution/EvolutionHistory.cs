﻿namespace SharpGP_Structures.Evolution;

[Serializable]
public class EvolutionHistory
{
    public List<EvolutionGeneration> generations = new List<EvolutionGeneration>();
}
[Serializable]
public class EvolutionGeneration
{
    public int generationIndex = -1;
    public DateTime generationStartTime;
    public long generationCreationTime=-1;
    public long generationEvaluationTime=-1;
    public List<double> fitnesses = new List<double>(); // mark for each individual in the generation
    public Dictionary<double, int> fitnesToUnits = new Dictionary<double, int>();
    public List<int> populationDepths = new List<int>(); // depth of each individual in the generation
    public Dictionary<int, int> popDepthToUnits = new Dictionary<int, int>();
    public List<GeneticAction> actions = new List<GeneticAction>(); // actions taken to create each individual in the generation]
    public EvolutionGeneration()
    {
        generationStartTime = DateTime.Now;
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
    public string crossedGene;
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