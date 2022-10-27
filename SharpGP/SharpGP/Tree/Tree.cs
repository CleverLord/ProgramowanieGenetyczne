using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpGP.Tree {
	public class Tree : InstrList {
		protected Dictionary<string, double> variables;
		private Random _random;
		public double GetVariable(string name)
		{
			variables.TryGetValue(name, out var value);
			return value;
		}
		public void SetVariable(string name, double value) => variables[name] = value;

		double creationProbability = 0.1;
		public string getRandomVariableNameOrCreate()
		{
			if (variables.Keys.Count == 0)
				return CreateVariable();

			if (_random.NextDouble() < creationProbability)
				return CreateVariable();
			else
				return getRandomVariableName();
		}

		public string CreateVariable()
		{
			string n= "X";
			int i = 0;
			while (variables.ContainsKey(n + i.ToString()))
				i++;
			return n + i.ToString();
		}
		
		public string getRandomVariableName()
		{
			return variables.Keys.ElementAt(_random.Next(variables.Keys.Count));
		}
	}
}