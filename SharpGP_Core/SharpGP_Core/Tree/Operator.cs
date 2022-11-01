namespace SharpGP_Core.Tree;


public enum OperatorEnum {
	Plus,
	Minus,
	Multiply,
	Divide
}

public class Operator : Node {
	public OperatorEnum op;

	public override string ToString()
	{
		switch (op)
		{
			case OperatorEnum.Plus:
				return "+";
			case OperatorEnum.Minus:
				return "-";
			case OperatorEnum.Multiply:
				return "*";
			case OperatorEnum.Divide:
				return "/";
			default:
				return "";
		}
	}

	public double Evaluate(double a, double b)
	{
		switch (op)
		{
			case OperatorEnum.Plus:
				return a + b;
			case OperatorEnum.Minus:
				return a - b;
			case OperatorEnum.Multiply:
				return a * b;
			case OperatorEnum.Divide:
				if (b < 0.00001)
					return a;
				return a / b;
		}
		return 0; //should never happen
	}
}