namespace SharpGP_Structures;

public class ProgramRunContext
{
    private static List<double> input = new List<double>();
    private List<double> inputCopy = new List<double>(input);
    private List<double> output = new List<double>();
    public Dictionary<string, double> variables = new Dictionary<string, double>();
    public Random rand = new Random();
    enum Strategy
    {
        InputOrZero,
        LockLastInput,
        LoopInput
    }

    Strategy strategy = Strategy.InputOrZero;
    public double Pop()
    {
        double result = 0;
        switch (strategy)
        {
            case Strategy.InputOrZero:
                if (input.Count > 0)
                {
                    result = input[0];
                    input.RemoveAt(0);
                    return result;
                }
                return 0;

            case Strategy.LockLastInput:
                if (input.Count > 1)
                {
                    result = input[0];
                    input.RemoveAt(0);
                    return result;
                }
                else if(input.Count>0)
                    return input[0];
                return 0;

            case Strategy.LoopInput:
                if (input.Count == 0)
                    return 0;
                if (inputCopy.Count == 0)
                    inputCopy = new List<double>(input);
                result = inputCopy[0];
                inputCopy.RemoveAt(0);
                return result;
        }
        return result; //should never happen
    }

    public void Push(double value)
    {
        output.Add((int)value);
    }
    public override string ToString()
    {
        String result = "";
        result += "Input: ";
        foreach (var i in input)
            result += i + ", ";
        result += "\nOutput: ";
        foreach (var i in output)
            result += i + ", ";
        result += "\nVariables: ";
        foreach (var i in variables)
            result += i.Key + " = " + i.Value + ", ";
        return result;
    }
    public List<double> GetOutput()
    {
        return new List<double>(output);
    }
}