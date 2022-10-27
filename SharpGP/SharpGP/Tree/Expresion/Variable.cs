namespace SharpGP.Tree {
	public class Variable : Expresion {
		private string name;
		public override double Evaluate(Tree context)
		{
			return context.GetVariable(name);
		}

		public override string ToString()
		{
			throw new System.NotImplementedException();
		}
	}
}