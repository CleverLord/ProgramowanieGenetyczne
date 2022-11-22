using System.Linq.Expressions;
using Newtonsoft.Json;

namespace SharpGP_Structures.TestSuite;

public class TestCase
{
    public List<double> input;
    public List<double> targetOutput;
}

public class TestSuite
{
    public TestConfig config;
    public List<TestCase> testCases;
    public string gradingFunction;
    [NonSerialized] public Grader grader;
}

public class TestConfig
{
    public int minNodeCount = 15; // grow trees with at least 15 nodes
    public int maxDepth = 6; //max tree depth
}