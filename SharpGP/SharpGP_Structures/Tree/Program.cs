﻿namespace SharpGP_Structures.Tree;

public class PRogram : Node, IGrowable, IMutable {
	public static char TAB = ' ';
	public Random rand = new Random();
	public List<Action> Actions => children.Cast<Action>().ToList();
	public List<String> Variables => Nodes.Where(x => x is Variable).Cast<Variable>().Select(x => x.ToString()).Distinct().ToList();
	public List<Node> Nodes => GetNestedNodes();
	public List<IGrowable> Growables => Nodes.Where(x => x is IGrowable).Cast<IGrowable>().ToList();
	public List<IMutable> Mutables => Nodes.Where(x => x is IMutable).Cast<IMutable>().ToList();
	public int minConst = 0;
	public int maxConst = 5;

	public PRogram() => children = new List<Node>();
	public PRogram(int seed) : this() => rand = new Random(seed);
	public PRogram(List<Node> children) => this.children = children;
	public void AddAction(Action action) => children.Add(action);
	public void Invoke(ProgramRunContext prc) => Actions.ForEach(a => a.Invoke(prc));
	public override string ToString()
	{
		UpdateIndent();
		String s = "";
		foreach (var child in children) s += child + "\n";
		return s;
	}
	public void Grow() // grow whole program
	{
		var x = Growables;
		x[rand.Next(0, x.Count)].Grow(this);
		UpdateParents();
	}
	public void Grow(PRogram ctx) => children.Add(Action.NewAction(this)); // grow program node itself
	public void Mutate() //mutate something in the program
	{
		var x = Mutables;
		x[rand.Next(0, x.Count)].Mutate(this);
	}
	public void Mutate(PRogram ctx) //mutate program node itself
	{
		Node n = children[rand.Next(0, children.Count)];
		children.Remove(n);
		children.Insert(rand.Next(0, children.Count), n);
	}
}

public class ProgramRunContext {
	private List<int> input = new List<int>();
	private List<int> output = new List<int>();
	public Dictionary<string, double> variables = new Dictionary<string, double>();
	public double Pop()
	{
		if (input.Count == 0) return 0;
		var result = input[0];
		input.RemoveAt(0);
		return result;
	}

	public void Push(double value)
	{
		output.Add((int) value);
	}
	public override string ToString()
	{
		String result = "";
		result+="Input: ";
		foreach (var i in input) result += i + ", ";
		result += "\nOutput: ";
		foreach (var i in output) result += i + ", ";
		result += "\nVariables: ";
		foreach (var i in variables) result += i.Key + " = " + i.Value + ", ";
		return result;
	}
}