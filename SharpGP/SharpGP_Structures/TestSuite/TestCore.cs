using System.Linq.Expressions;
using Newtonsoft.Json;

namespace SharpGP_Structures.TestSuite;

public class TestSet
{
    public TreeConfig config = new TreeConfig();
    public List<TestCase> testCases=new List<TestCase>();
    public List<TestStage> stages=new List<TestStage>();
}

public class TestCase
{
    public List<double> input;
    public List<double> targetOutput;
}

public class TestStage
{
    public Grader grader;
    public Agregrader ag;
    public double threshold = 0.95;
}
