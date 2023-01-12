using SharpGP_Structures;
using SharpGP_Structures.TestSuite;
using Newtonsoft.Json;

var ProjectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent;
TestSet problem1 = JsonConvert.DeserializeObject<TestSet>(File.ReadAllText(ProjectDirectory+"/TestSuites/TestSet_1_1_A.SharpGpTestSuite"));
SharpGP.SharpGP.PerformEvolution(problem1);