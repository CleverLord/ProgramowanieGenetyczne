using SharpGP_Structures.Tree;
using Action = SharpGP_Structures.Tree.Action;
using Antlr4.Runtime;
namespace SharpGP_Structures.Generator;

public static class Generator {
	private static int minNodes = 22; // grow trees with at least 12 nodes

	public static Program GenerateProgram(int seed=-1)
	{
		Program p = new Program();
		if (seed != -1)
			p.rand = new Random(seed);
		while(p.nodes.Count<minNodes)
			p.Grow();
		return p;
	}
	public static Program LoadProgramFromFile(string filename)
	{
		ICharStream input = CharStreams.fromPath(filename);
		var parser = getParser(input);
		var tree = parser.program();
		AntlrToProgram programVisitor = new AntlrToProgram();
		Program p = (Program) programVisitor.Visit(tree);
		return p;
	}
	public static Program LoadProgramFromString(string textToParse)
	{
		ICharStream input = CharStreams.fromString(textToParse);
		var parser = getParser(input);
		var tree = parser.program();
		AntlrToProgram programVisitor = new AntlrToProgram();
		Program p = (Program) programVisitor.Visit(tree);
		return p;
	}
	private static SharpParser getParser(ICharStream input)
	{
		var lexer = new SharpLexer(input);
		CommonTokenStream tokens = new CommonTokenStream(lexer);
		return new SharpParser(tokens);
	}
}