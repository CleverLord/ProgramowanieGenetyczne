namespace SharpGP_Structures.Tree;

public class ProgramRunContext {
	private static List<int> input = new List<int>();
	private List<int> inputCopy = new List<int>(input);
	private List<int> output = new List<int>();
	public Dictionary<string, double> variables = new Dictionary<string, double>();
	public Random rand = new Random();
	public double Pop()
	{
		if (inputCopy.Count == 0)
		{
			switch (rand.Next(0, 3))
			{
				case 0:
					return 0;
				case 1:
					return input[0];
				case 2:
					return input[input.Capacity - 1];
			}
			inputCopy = input;
		}
		var result = inputCopy[0];
		inputCopy.RemoveAt(0);
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