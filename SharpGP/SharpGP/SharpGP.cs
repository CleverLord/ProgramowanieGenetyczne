using Antlr4.Runtime;
using SharpGP_Structures.Generator;
using SharpGP_Structures.Tree;

namespace SharpGP;

public static class SharpGP {
	private static Random rand = new Random();
	private static int minNodeCount = 12; // grow trees with at least 12 nodes
	private static int maxDepth = 4; //max tree depth
	public static PRogram GenerateProgram(int seed=-1)
	{
		PRogram p = new PRogram();
		if (seed != -1) p = new PRogram(seed);
		while (p.Nodes.Count < minNodeCount)
		{
			p.Grow();
			if(p.GetDepth() > maxDepth) break;	
			//Console.WriteLine(p.GetDepth());
		}
		return p;
	}
	public static PRogram LoadProgramFromFile(string filename)
	{
		ICharStream input = CharStreams.fromPath(filename);
		var parser = getParser(input);
		var tree = parser.program();
		AntlrToProgram programVisitor = new AntlrToProgram();
		PRogram p = (PRogram) programVisitor.Visit(tree);
		return p;
	}
	public static PRogram LoadProgramFromString(string textToParse)
	{
		ICharStream input = CharStreams.fromString(textToParse);
		var parser = getParser(input);
		var tree = parser.program();
		AntlrToProgram programVisitor = new AntlrToProgram();
		PRogram p = (PRogram) programVisitor.Visit(tree);
		return p;
	}
	private static SharpParser getParser(ICharStream input)
	{
		var lexer = new SharpLexer(input);
		CommonTokenStream tokens = new CommonTokenStream(lexer);
		return new SharpParser(tokens);
	}
	public static (PRogram,PRogram)? CrossPrograms(PRogram p1, PRogram p2)
	{
		PRogram p1c = (PRogram) p1.Clone();
		PRogram p2c = (PRogram) p2.Clone();
		//p1c.UpdateParents();
		//p2c.UpdateParents();
		List<Node> p1n = p1c.Nodes; // get the nodes of the first program
		List<Node> p2n = p2c.Nodes; // get the nodes of the second program
		
		p1n.RemoveAt(0); //drop the root node
		p2n.RemoveAt(0); //drop the root node
		
		while (p1n.Count > 0)
		{
			int p1Index = rand.Next(0, p1n.Count);
			Node p1Node = p1n[p1Index];
			p1n.RemoveAt(p1Index);
			var matchingNodes= p2n.Where(n => n.GetType() == p1Node.GetType()).ToList();
			if (matchingNodes.Count == 0) continue;
			int p2Index = rand.Next(0, matchingNodes.Count);
			Node p2Node = matchingNodes[p2Index];
			
			Node.CrossNodes(p1Node,p2Node); //this is in node, since children are protected
			return (p1c,p2c);
		}
		return null;
	}
}