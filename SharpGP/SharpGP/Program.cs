using SharpGP_Structures;
using SharpGP_Structures.TestSuite;
using Newtonsoft.Json;
using SharpGP_Structures.Evolution;

var ProjectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent;
List<TestSet> problems = new List<TestSet>();
foreach (var file in Directory.GetFiles(ProjectDirectory + "/TestSuites/"))
{
    problems.Add(JsonConvert.DeserializeObject<TestSet>(File.ReadAllText(file)));
    problems[problems.Count - 1].name = Path.GetFileNameWithoutExtension(file);
}
var ResultsDirectory = ProjectDirectory + "/Results/";
foreach (var p in problems)
{
    EvolutionHistory eh= SharpGP.SharpGP.PerformEvolution(p);
    //Save the results
    File.WriteAllText(ResultsDirectory + p.name + ".json", JsonConvert.SerializeObject(eh));
}
