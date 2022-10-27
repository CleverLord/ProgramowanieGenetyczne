using System;

namespace SharpGP.Tree {
	public class Condition {
		const double doubleEpsilon = 0.00001;
		public Expresion Left { get; set; }
		public Expresion Right { get; set; }
		public ConditionType Type { get; set; }
		public Condition(Expresion left, Expresion right, ConditionType type) {
			Left = left;
			Right = right;
			Type = type;
		}
		public override string ToString() {
			return string.Format("{0} {1} {2}", Left, Type, Right);
		}

		public bool Evaluate(Tree context)
		{
			switch (Type)
			{
				case ConditionType.Equals:
					return Math.Abs(Left.Evaluate(context) - Right.Evaluate(context)) < doubleEpsilon;
				case ConditionType.NotEquals:
					return Math.Abs(Left.Evaluate(context) - Right.Evaluate(context)) > doubleEpsilon;
				case ConditionType.Greater:
					return Left.Evaluate(context) > Right.Evaluate(context);
				case ConditionType.GreaterOrEquals:
					return Left.Evaluate(context) >= Right.Evaluate(context);
				case ConditionType.Less:
					return Left.Evaluate(context) < Right.Evaluate(context);
				case ConditionType.LessOrEquals:
					return Left.Evaluate(context) <= Right.Evaluate(context);
			}
			return false; // should never happen
		}

		public enum ConditionType { Equals, NotEquals, Greater, GreaterOrEquals, Less, LessOrEquals }
	}
}