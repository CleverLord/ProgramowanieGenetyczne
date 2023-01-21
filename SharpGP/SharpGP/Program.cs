using SharpGP_Structures;
using SharpGP_Structures.TestSuite;
using Newtonsoft.Json;

var ProjectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent;
TestSet problem1 = JsonConvert.DeserializeObject<TestSet>(File.ReadAllText(ProjectDirectory + "/TestSuites/TestSet_1_1_A.SharpGpTestSuite"));
//repeat 100 times
for (int i = 0; i < 100; i++) { SharpGP.SharpGP.PerformEvolution(problem1); }