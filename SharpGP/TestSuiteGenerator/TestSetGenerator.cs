using Newtonsoft.Json;
using SharpGP_Structures.TestSuite;

public static class TestSetGenerator
{
    public static void SaveTestSuite(TestSet ts, string filename, string fileExtension = ".SharpGpTestSuite", string folder = "")
    {
        if (folder == "")
        {
            var binFolder = Directory.GetCurrentDirectory();
            folder = Directory.GetParent(binFolder).Parent.Parent.CreateSubdirectory("TestSuites").FullName;
        }
        string path = Path.Combine(folder, filename + fileExtension);
        File.WriteAllText(path, JsonConvert.SerializeObject(ts, Formatting.Indented));
    }
    public static void GenerateConstantValueTS(double constant) // you can add parameters to this funciton if you wish
    {
        TestSet ts = new TestSet();
        Random rnd = new Random();
        for(int i = 0; i < 100; i++)
        {
            double x = rnd.NextDouble() * 100;
            double y = rnd.NextDouble() * 100;
            double sum = x + y;
            ts.testCases.Add(new TestCase() { input = new List<double>() { x,y}, 
                targetOutput = new List<double>() { sum } });
        }
        
        ts.stages.Add( new (){grader =  new Grader("bestAbsoluteError"), 
            ag =  new Agregrader("sum"){}} );
        ts.stages.Add( new (){grader =  new Grader("hasTargetValue"), 
            ag =  new Agregrader("sum"){}} );
        ts.stages.Add( new (){grader =  new Grader("atFirstPlace"), 
            ag =  new Agregrader("sum"){}} );
        ts.stages.Add( new (){grader =  new Grader("justOneTargetValue"), 
            ag =  new Agregrader("sum"){}} );
        
        SaveTestSuite(ts, "ConstantValueTS_" + constant);
    }

    public static void GenerateFor_1_1_A()
    {
        double threshold = 0;
        TestSet ts = new TestSet();
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_1_A"), threshold = threshold});
        ts.testCases.Add(new TestCase() {targetOutput = new List<double>() {1}});
        ts.testCases.Add(new TestCase() {input = new List<double>(), targetOutput = new List<double>() {1}});
        ts.testCases.Add(new TestCase() {input = new List<double>(){1}, targetOutput = new List<double>() {1}});
        ts.testCases.Add(new TestCase() {input = new List<double>(){123}, targetOutput = new List<double>() {1}});
        SaveTestSuite(ts, "TestSet_1_1_A");
    }

    public static void GenerateFor_1_1_B()
    { 
        double threshold = 0;
        TestSet ts = new TestSet();
        ts.stages.Add(new TestStage() { grader = new Grader("target_1_1_B"), threshold = threshold });
        ts.testCases.Add(new TestCase() { targetOutput = new List<double>() { 789 } });
        ts.testCases.Add(new TestCase() { input = new List<double>(), targetOutput = new List<double>() { 789 } });
        ts.testCases.Add(new TestCase()
            { input = new List<double>() { 1 }, targetOutput = new List<double>() { 789 } });
        ts.testCases.Add(new TestCase()
            { input = new List<double>() { 123 }, targetOutput = new List<double>() { 789 } });
        SaveTestSuite(ts, "TestSet_1_1_B");
    }
    public static void GenerateFor_1_1_C()
    {
        double threshold = 0;
        TestSet ts = new TestSet();
        ts.stages.Add(new TestStage() { grader = new Grader("target_1_1_C"), threshold = threshold });
        ts.testCases.Add(new TestCase() { targetOutput = new List<double>() { 31415 } });
        ts.testCases.Add(new TestCase() { input = new List<double>(), targetOutput = new List<double>() { 31415 } });
        ts.testCases.Add(new TestCase()
            { input = new List<double>() { 1 }, targetOutput = new List<double>() { 31415 } });
        ts.testCases.Add(new TestCase()
            { input = new List<double>() { 123 }, targetOutput = new List<double>() { 31415 } });
        SaveTestSuite(ts, "TestSet_1_1_C");
    }

    public static void GenerateFor_1_1_D()
    {
        double threshold = 0;
        TestSet ts = new TestSet();
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_1_D__0"), threshold = threshold});
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_2_D__1"), threshold = threshold});
        ts.testCases.Add(new TestCase() { targetOutput = new List<double>() { 1 } });
        SaveTestSuite(ts, "TestSet_1_1_D");
    }
    public static void GenerateFor_1_1_E()
    {
        double threshold = 0;
        TestSet ts = new TestSet();
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_1_E__0"), threshold = threshold});
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_2_E__1"), threshold = threshold});
        ts.testCases.Add(new TestCase() { targetOutput = new List<double>() { 789 } });
        SaveTestSuite(ts, "TestSet_1_1_E");
    }
    public static void GenerateFor_1_1_F()
    {
        double threshold = 0;
        TestSet ts = new TestSet();
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_1_F__0"), threshold = threshold});
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_1_F__1"), threshold = threshold});
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_1_F__2"), threshold = 0.05 });
        ts.testCases.Add(new TestCase() { targetOutput = new List<double>() { 1 } });
        SaveTestSuite(ts, "TestSet_1_1_F");
    }
    public static void GenerateFor_1_2_A()
    {
        double range = 9;
        double threshold = range * 100 * 0.05;
        TestSet ts = new TestSet();
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_2_A__0"), threshold = threshold});
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_2_A__1"), threshold = threshold});
        for (int i = 0; i < 100; i++)
        {
            double a = getRandomDouble(0, 9);
            double b = getRandomDouble(0, 9);
            double c = a + b;
            ts.testCases.Add(new TestCase() {input = new List<double>() {a, b}, targetOutput = new List<double>() {c}});
        }
        SaveTestSuite(ts, "TestSet_1_2_A");
    }
    public static void GenerateFor_1_2_B()
    {
        double range = 9;
        double threshold = 2 * range * 100 * 0.05;
        TestSet ts = new TestSet();
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_2_B__0"), threshold = threshold});
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_2_B__1"), threshold = threshold});
        for (int i = 0; i < 100; i++)
        {
            double a = getRandomDouble(-range, range);
            double b = getRandomDouble(-range, range);
            double c = a + b;
            ts.testCases.Add(new TestCase() {input = new List<double>() {a, b}, targetOutput = new List<double>() {c}});
        }
        SaveTestSuite(ts, "TestSet_1_2_B");
    }
    public static void GenerateFor_1_2_C()
    {
        double range = 9999;
        double threshold = 2 * range * 100 * 0.05;
        TestSet ts = new TestSet();
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_2_C__0"), threshold = threshold});
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_2_C__1"), threshold = threshold});
        for (int i = 0; i < 100; i++)
        {
            double a = getRandomDouble(-range,range);
            double b = getRandomDouble(-range,range);
            double c = a + b;
            ts.testCases.Add(new TestCase() {input = new List<double>() {a, b}, targetOutput = new List<double>() {c}});
        }
        SaveTestSuite(ts, "TestSet_1_2_C");
    }
    public static void GenerateFor_1_2_D()
    {
        double range = 9999;
        double threshold = 2 * range * 100 * 0.05;
        TestSet ts = new TestSet();
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_2_D__0"), threshold = threshold});
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_2_D__1"), threshold = threshold});
        for (int i = 0; i < 100; i++)
        {
            double a = getRandomDouble(-range,range);
            double b = getRandomDouble(-range,range);
            double c = a - b;
            ts.testCases.Add(new TestCase() {input = new List<double>() {a, b}, targetOutput = new List<double>() {c}});
        }
        SaveTestSuite(ts, "TestSet_1_2_D");
    }
    public static void GenerateFor_1_2_E()
    {
        double range = 9999;
        double threshold = 2 * range * 100 * 0.05;
        TestSet ts = new TestSet();
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_2_E__0"), threshold = threshold});
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_2_E__1"), threshold = threshold});
        for (int i = 0; i < 100; i++)
        {
            double a = getRandomDouble(-range,range);
            double b = getRandomDouble(-range,range);
            double c = a * b;
            ts.testCases.Add(new TestCase() {input = new List<double>() {a, b}, targetOutput = new List<double>() {c}});
        }
        SaveTestSuite(ts, "TestSet_1_2_E");
    }
    public static void GenerateFor_1_3_A()
    {
        double range = 9;
        double threshold = range * 100 * 0.05;
        TestSet ts = new TestSet();
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_3_A__0"), threshold = threshold});
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_3_A__1"), threshold = threshold});
        for (int i = 0; i < 100; i++)
        {
            double a = getRandomDouble(0, range);
            double b = getRandomDouble(0, range);
            double c = a < b ? a : b;
            ts.testCases.Add(new TestCase() {input = new List<double>() {a, b}, targetOutput = new List<double>() {c}});
        }
        SaveTestSuite(ts, "TestSet_1_3_A");
    }
    public static void GenerateFor_1_3_B()
    {
        double range = 9999;
        double threshold = 2*range * 100 * 0.05;
        TestSet ts = new TestSet();
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_3_B__0"), threshold = threshold});
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_3_B__1"), threshold = threshold});
        for (int i = 0; i < 100; i++)
        {
            double a = getRandomDouble(-9999,9999);
            double b = getRandomDouble(-9999,9999);
            double c = a < b ? a : b;
            ts.testCases.Add(new TestCase() {input = new List<double>() {a, b}, targetOutput = new List<double>() {c}});
        }
        SaveTestSuite(ts, "TestSet_1_3_B");
    }
    
    public static void GenerateFor_1_4_A()
    {
        double range = 99;
        double threshold = 2 *range * 100 * 0.05 * 10;
        TestSet ts = new TestSet();
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_4_A__0"), threshold = threshold});
        ts.stages.Add(new TestStage() {grader = new Grader("target_1_4_A__1"), threshold = threshold});

        for (int i = 0; i < 100; i++)
        {
            double[] tab = new double[10];
            //generate random numbers
            for (int j = 0; j < 10; j++) { tab[j] = getRandomDouble(-range, range); }
            //calculate avg
            double avg = tab.ToList().Average();
            
            ts.testCases.Add(new TestCase() {input = tab.ToList(), targetOutput = new List<double>() {avg}});
        }

        SaveTestSuite(ts, "TestSet_1_4_A");
    }

    public static void GenerateFor_1_4_B()
    {
        double range = 99;
        double threshold = range * 100 * 0.05;
        TestSet ts = new TestSet();
        ts.stages.Add(new TestStage() { grader = new Grader("target_1_4_B__0"), threshold = threshold });
        ts.stages.Add(new TestStage() { grader = new Grader("target_1_4_B__1"), threshold = threshold });
        
        for (int j = 0; j < 100; j++)
        {
            int numberOfTests = rnd.Next(0, 99);
            double sum = 0;
            for (var i = 0; i < numberOfTests; i++)
            {
                sum += getRandomDouble(-range, range);
            }
            double avg = Math.Round(sum / numberOfTests);
            ts.testCases.Add(new TestCase() 
                {input = {numberOfTests}, targetOutput = new List<double>() {avg}});
        }

        SaveTestSuite(ts, "TestSet_1_4_B");
    }

    #region RandomRegion
    static Random rnd = new Random();
    public static double getRandomDouble(double min, double max)
    {
        return rnd.NextDouble() % (max - min) + min;
    }
    #endregion

}