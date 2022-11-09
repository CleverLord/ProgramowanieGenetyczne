namespace SharpGP_Structures.Tree;

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