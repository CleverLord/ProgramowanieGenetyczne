using System;

namespace SharpGP.Tree {
	public class Sin : Expresion {
		public override double Evaluate(Tree context) => Math.Sin(leftSubTree.Evaluate(context));
		public override string ToString() => $"sin({leftSubTree})";
	}
}