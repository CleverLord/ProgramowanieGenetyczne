using SharpGP_Core.Tree;
using Action = SharpGP_Core.Tree.Action;

namespace SharpGP_Core.Generator; 

public class Generator {
	
	public static Random r= new Random();

	private int maxDepth;
	private int minNodes=12; // grow trees with at least 12 nodes
	private int maxNodes=15; // stop growing trees when they have 15 or more nodes
	
	public void Create()
	{
		Program p = new Program();
	}
	
	
}