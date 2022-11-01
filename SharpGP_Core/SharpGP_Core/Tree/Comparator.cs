namespace SharpGP_Core.Tree;
public enum ComparatorEnum {
	Equal,
	NotEqual,
	LessThan,
	LessThanOrEqual,
	GreaterThan,
	GreaterThanOrEqual
}
public class Comparator : Node {
	public ComparatorEnum op;

	public override string ToString()
	{
		switch (op)
		{
			case ComparatorEnum.Equal:
				return "==";
			case ComparatorEnum.NotEqual:
				return "!=";
			case ComparatorEnum.LessThan:
				return "<";
			case ComparatorEnum.LessThanOrEqual:
				return "<=";
			case ComparatorEnum.GreaterThan:
				return ">";
			case ComparatorEnum.GreaterThanOrEqual:
				return ">=";
		}
		return "!!"; // should never happen
	}
}