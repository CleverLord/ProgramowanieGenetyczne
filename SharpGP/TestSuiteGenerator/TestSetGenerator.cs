using Newtonsoft.Json;
using SharpGP_Structures.TestSuite;

public static class TestSetGenerator
{
    public static void SaveTestSuite(TestSet ts, string filename, string fileExtension = ".SharpGpTestSuite",
        string folder = "")
    {
        if (folder == "")
        {
            var binFolder = Directory.GetCurrentDirectory();
            folder = Directory.GetParent(binFolder).Parent.Parent.CreateSubdirectory("TestSuites").FullName;
        }

        var path = Path.Combine(folder, filename + fileExtension);
        File.WriteAllText(path, JsonConvert.SerializeObject(ts, Formatting.Indented));
    }

    public static void GenerateConstantValueTS(double constant) // you can add parameters to this funciton if you wish
    {
        var ts = new TestSet();

        ts.stages.Add(new TestStage { grader = new Grader("accuracyScore") });

        // TODO: add some test cases here


        SaveTestSuite(ts, "ConstantValueTS_" + constant);
    }

    public static void GenerateFor_1_1_A()
    {
        var ts = new TestSet();
        ts.stages.Add(new TestStage { grader = new Grader("hasTargetValue"), threshold = 0.001 });
        ts.testCases.Add(new TestCase { targetOutput = new List<double> { 1 } });
        ts.testCases.Add(new TestCase { input = new List<double>(), targetOutput = new List<double> { 1 } });
        ts.testCases.Add(new TestCase { input = new List<double> { 1 }, targetOutput = new List<double> { 1 } });
        ts.testCases.Add(new TestCase { input = new List<double> { 123 }, targetOutput = new List<double> { 1 } });
        SaveTestSuite(ts, "TestSet_1_1_A");
    }

    public static void GenerateFor_1_3_A()
    {
        var ts = new TestSet();

        ts.stages.Add(new TestStage { grader = new Grader("target_1_3_A"), threshold = 0.001 });

        for (var i = 0; i < 100; i++)
        {
            var a = getRandomDouble(0, 9);
            var b = getRandomDouble(0, 9);
            var c = a < b ? a : b;
            ts.testCases.Add(new TestCase { input = new List<double> { a, b }, targetOutput = new List<double> { c } });
        }

        SaveTestSuite(ts, "TestSet_1_3_A");
    }

    public static void GenerateFor_1_4_A()
    {
        var ts = new TestSet();

        ts.stages.Add(new TestStage { grader = new Grader("target_1_4_A__1"), threshold = 0.001 });
        ts.stages.Add(new TestStage { grader = new Grader("target_1_4_A__0"), threshold = 0.001 });

        for (var i = 0; i < 100; i++)
        {
            var tab = new double[10];
            //generate random numbers
            for (var j = 0; j < 10; j++) tab[j] = getRandomDouble(-99, 99);
            //calculate avg
            var avg = tab.ToList().Average();

            ts.testCases.Add(new TestCase { input = tab.ToList(), targetOutput = new List<double> { avg } });
        }

        SaveTestSuite(ts, "TestSet_1_4_A");
    }

    #region RandomRegion

    private static readonly Random rnd = new();

    public static double getRandomDouble(double min, double max)
    {
        return rnd.NextDouble() % (max - min) + min;
    }

    #endregion
}