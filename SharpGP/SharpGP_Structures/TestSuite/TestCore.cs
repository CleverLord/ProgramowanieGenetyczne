namespace SharpGP_Structures.TestSuite;

public class TestSet
{
    public TreeConfig config;
    public List<TestStage> stages;
    public List<TestCase> testCases;
}

public class TestCase
{
    public List<double> input;
    public List<double> targetOutput;
}

public class TestStage
{
    public Agregrader ag;
    public Grader grader;
    public double threshold = 0.95;
}