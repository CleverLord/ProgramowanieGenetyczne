using SharpGP_Core.Tree;
namespace SharpGP_Core.Generator; 

public class Generator {
	private Program r;
	private int maxDepth;
	private int maxNodes;

	public void Create()
	{
		Variable[] variables = new Variable[2];
		variables[0] = new Variable("x_0");
		variables[1] = new Variable("x_1");
		
		Program p = new Program();
		p.actions.Add(new Assignment());
	}

	public void Grow(Program p)
	{
		Random r = new Random();
		int next = r.Next() % 2;
		
	}
}