using SharpGP_Structures.Tree;
using Action = SharpGP_Structures.Tree.Action;

namespace SharpGP_Structures.Generator;

public static class Generator {
	public static Random rand = new Random();
	private static int minNodes = 1200; // grow trees with at least 12 nodes

	public static Program GenerateProgram()
	{
		Program p = new Program();
		//p.rand = new Random(123);
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