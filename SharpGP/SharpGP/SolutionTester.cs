using Newtonsoft.Json;
using SharpGP_Structures;
using SharpGP_Structures.Evolution;
using SharpGP_Structures.TestSuite;
using SharpGP_Structures.Tree;
using SharpGP.Utils;

namespace SharpGP;

public static class SolutionTester
{
    public static void Main()
    {
        List<SolutionTesterStructure> solutionTesterStructures = new List<SolutionTesterStructure>();
        var ProjectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent;

        foreach (var file in Directory.GetFiles(ProjectDirectory + "/Results/"))
        {
            EvolutionHistory eh = JsonConvert.DeserializeObject<EvolutionHistory>(File.ReadAllText(file));
            SolutionTesterStructure sts = new SolutionTesterStructure();
            // get name from file
            sts.name = Path.GetFileNameWithoutExtension(file);
            sts.program = TreeGenerator.LoadProgramFromString(eh.generations[eh.generations.Count - 1].bestProgram);
            solutionTesterStructures.Add(sts);
        }
        foreach (var file in Directory.GetFiles(ProjectDirectory + "/TestSuites/"))
        {
            TestSet ts = JsonConvert.DeserializeObject<TestSet>(File.ReadAllText(file));
            SolutionTesterStructure sts = solutionTesterStructures.FirstOrDefault(x => x.name == Path.GetFileNameWithoutExtension(file));
            if(sts == null)
                Console.WriteLine("No matching file found for " + Path.GetFileNameWithoutExtension(file));
            else
                sts.testCases = ts.testCases;
        }
        foreach (var sts in solutionTesterStructures)
        {
            foreach (TestCase tc in sts.testCases)
            {
                ProgramRunContext prc = new ProgramRunContext(); // make a constructor that takes a test case
                prc.input = new List<double>(tc.input);
                prc.maxExecutedActions = 100_000;
                sts.program.Invoke(prc);
                if(! prc.hasTimeouted())
                    sts.programRunContexts.Add(prc);
                else
                    Console.WriteLine("Timeout");
            }
        }

        foreach (var sts in solutionTesterStructures)
        {
            for (int i = 0; i < sts.testCases.Count; i++)
            {
                string result="";
                result += sts.name + "\t";
                TestCase tc=sts.testCases[i];
                ProgramRunContext prc=sts.programRunContexts[i];
                result += "input: ";
                foreach (var val in tc.input)
                    result += val + ", ";
                result += "\t";
                result += "t.output: ";
                foreach (var val in tc.targetOutput)
                    result += val + ", ";
                result += "\t";
                result += "p.output: ";
                foreach (var val in prc.GetOutput())
                    result += val + ", ";
                
                sts.x.Add(result);
            }
        }
        foreach (var sts in solutionTesterStructures)
        {
            foreach (var x in sts.x)
                Console.WriteLine(x);
        }
    }
}

public class SolutionTesterStructure
{
    public string name;
    public PRogram program;
    public List<TestCase> testCases = new List<TestCase>();
    public List<ProgramRunContext> programRunContexts = new List<ProgramRunContext>();
    public List<string> x = new List<string>();
}