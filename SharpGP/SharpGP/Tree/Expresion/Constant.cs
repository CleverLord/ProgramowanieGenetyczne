namespace SharpGP.Tree {
	public class Constant : Expresion {
		private double value;
		public override double Evaluate(Tree context)
		{
			return value;
		}

		public override string ToString()
		{
			return value.ToString();
		}
	}
}