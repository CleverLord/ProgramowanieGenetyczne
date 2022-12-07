using Antlr4.Runtime;
using SharpGP_Structures;
using SharpGP_Structures.Generator;
using SharpGP_Structures.Tree;

namespace SharpGP.Utils;

public static class TreeGenerator
{
    public static PRogram GenerateProgram_NodeCount(int minNodeCount = 12, int seed = -1)
    {
        PRogram p = new PRogram();
        if (seed != -1) { p = new PRogram(seed); }
        while (p.Nodes.Count < minNodeCount) { p.Grow(); }
        return p;
    }
    public static PRogram GenerateProgram_MaxDepth(int maxDepth = 12, int seed = -1)
    {
        PRogram p = new PRogram();
        if (seed != -1) { p = new PRogram(seed); }
        while (p.GetDepth() < maxDepth) { p.Grow(); }
        return p;
    }
    public static PRogram GenerateProgram_FromConfig(TreeConfig tsConfig, double depthPercentage = 100)
    {
        PRogram p = new PRogram(tsConfig);
        if ((int)depthPercentage == 100)
        {
            p.FullGrow();
            return p;
        }
        while (p.GetDepth() < tsConfig.maxDepth * depthPercentage / 100) { p.Grow(); }
        return p;
    }
    public static PRogram LoadProgramFromFile(string filename)
    {
        ICharStream input = CharStreams.fromPath(filename);
        var parser = getParser(input);
        var tree = parser.program();
        AntlrToProgram programVisitor = new AntlrToProgram();
        PRogram p = (PRogram)programVisitor.Visit(tree);
        return p;
    }
    public static PRogram LoadProgramFromString(string textToParse)
    {
        ICharStream input = CharStreams.fromString(textToParse);
        var parser = getParser(input);
        var tree = parser.program();
        AntlrToProgram programVisitor = new AntlrToProgram();
        PRogram p = (PRogram)programVisitor.Visit(tree);
        return p;
    }
    private static SharpParser getParser(ICharStream input)
    {
        var lexer = new SharpLexer(input);
        CommonTokenStream tokens = new CommonTokenStream(lexer);
        return new SharpParser(tokens);
    }

}