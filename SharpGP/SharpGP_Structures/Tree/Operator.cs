namespace SharpGP_Structures.Tree;

public class Operator : Node, IMutable
{
    public static List<string> operators = new() { "+", "-", "*", "/" };
    public string op;

    public Operator(string op)
    {
        if (!operators.Contains(op))
            throw new Exception("There was an attempt to create an Operator using string with value: " + op +
                                "\n Only the following operators are allowed: +, -, *, /");
        this.op = op;
    }

    public void Mutate(PRogram ctx)
    {
        op = operators[ctx.rand.Next(0, operators.Count)];
    }

    public override string ToString()
    {
        return op;
    }

    public double Evaluate(double a, double b)
    {
        switch (op)
        {
            case "+": return a + b;
            case "-": return a - b;
            case "*": return a * b;
            case "/":
                if (b < 0.00001) return a;
                return a / b;
        }

        return 0; //should never happen
    }

    public static Operator NewOperator(PRogram ctx)
    {
        return new(operators[ctx.rand.Next(0, operators.Count)]);
    }
}