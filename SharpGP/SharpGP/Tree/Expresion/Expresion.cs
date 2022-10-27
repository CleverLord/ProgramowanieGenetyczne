using System.Collections.Generic;

namespace SharpGP.Tree {
	public abstract class Expresion {
		protected Expresion leftSubTree;
		protected Expresion rightSubTree;

		public abstract double Evaluate(Tree context);
		public abstract string ToString();
	}
	public static class ExpSupport {
		public static Expresion idToExp(int id)
		{
			switch (id)
			{
				case 0: return new Add(); break;
				case 1: return new Subtract() ; break;
				case 2: return new Multiply(); break;
				case 3: return new Divide(); break;
				case 4: return new Sin(); break;
				case 5: return new Cos(); break;
			}
			return null;
		}
	}
}