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
        ts.gradingFunction = "accuracyScore";

        // TODO: add some test cases here

        SaveTestSuite(ts, "ConstantValueTS_" + constant);
    }
}