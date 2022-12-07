﻿namespace SharpGP_Structures;

public class ProgramRunContext
{
    private static readonly List<double> input = new();
    public long ElapsedMilliseconds = -1; //obsolete
    public long ElapsedTicks = -1;
    private List<double> inputCopy = new(input);
    private readonly List<double> output = new();
    public Random rand = new();

    private readonly Strategy strategy = Strategy.InputOrZero;
    public Dictionary<string, double> variables = new();

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

                if (input.Count > 0)
                {
                    return input[0];
                }

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
        var result = "";
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

    private enum Strategy
    {
        InputOrZero,
        LockLastInput,
        LoopInput
    }
}