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
    public List<TestCase> testCases;
    public string gradingFunction;
    [NonSerialized] public Grader grader;
}