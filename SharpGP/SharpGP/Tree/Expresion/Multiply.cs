namespace SharpGP.Tree {
	public class Multiply : Expresion {
		public override double Evaluate(Tree context) => leftSubTree.Evaluate(context) * rightSubTree.Evaluate(context);
		public override string ToString() => "(" + leftSubTree.ToString() + " * " + rightSubTree.ToString() + ")";
	}
}