using System.Linq.Expressions;
using Newtonsoft.Json;

namespace SharpGP_Structures.TestSuite;

public class TestSet
{
    public string name="Name not set";
    public TreeConfig config = new TreeConfig();
    public List<TestCase> testCases = new List<TestCase>();
    public List<TestStage> stages = new List<TestStage>();
}

public class TestCase
{
    public List<double> input = new List<double>();
    public List<double> targetOutput = new List<double>();
}

public class TestStage
{
    public Grader grader;
    public Agregrader ag;
    public double threshold = 0;
}