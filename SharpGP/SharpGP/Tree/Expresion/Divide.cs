namespace SharpGP.Tree {
	public class Divide : Expresion {
		public override double Evaluate(Tree context)
		{
			double right = rightSubTree.Evaluate(context);
			return (right < 0.001) ? leftSubTree.Evaluate(context) : leftSubTree.Evaluate(context) / right;
		}

		public override string ToString() => $"sin({leftSubTree})";
	}
}