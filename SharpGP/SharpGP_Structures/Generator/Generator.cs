using SharpGP_Structures.Tree;
using Action = SharpGP_Structures.Tree.Action;

namespace SharpGP_Structures.Generator;

public static class Generator {
	public static Random rand = new Random();
	private static int minNodes = 12; // grow trees with at least 12 nodes

	public static Program GenerateProgram()
	{
		Program p = new Program();
		while(p.nodes.Count<minNodes)
			p.Grow();
		return p;
	}
	public static Program LoadProgram(string filename)
	{
		Program p = new Program();
		
		return p;
	}
	
}