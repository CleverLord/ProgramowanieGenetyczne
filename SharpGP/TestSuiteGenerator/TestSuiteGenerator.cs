using Newtonsoft.Json;
using SharpGP_Structures.TestSuite;

public static class TestSuiteGenerator
{
    public static void SaveTestSuite(TestSuite ts, string filename, string fileExtension = ".SharpGpTestSuite", string folder = "")
    {
        if (folder == "") { folder = Directory.GetCurrentDirectory(); }
        File.WriteAllTextAsync(folder + filename + fileExtension, JsonConvert.SerializeObject(ts));
    }
    public static void GenerateConstantValueTS(double constant) // you can add parameters to this funciton if you wish
    {
        TestSuite ts = new TestSuite();
        ts.gradingFunction = "accuracyScore";

        // TODO: add some test cases here

        SaveTestSuite(ts, "ConstantValueTS_" + constant);
    }
}