using System;

namespace SharpGP.Tree {
	public class Cos : Expresion {
		public override double Evaluate(Tree context) => Math.Cos(leftSubTree.Evaluate(context));
		public override string ToString() => $"cos({leftSubTree})";
	}
}