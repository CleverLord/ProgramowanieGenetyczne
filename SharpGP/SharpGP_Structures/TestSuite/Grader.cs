using System.Reflection;

namespace SharpGP_Structures.TestSuite;

public class Grader
{
    public Func<TestCase, ProgramRunContext, double> gradingFunctionDelegate;

    public Grader(string gradingFunction)
    {
        var method = GetType().GetMethod(gradingFunction, BindingFlags.Instance | BindingFlags.Public);
        if (method == null) { throw new Exception("Grading function called " + gradingFunction + " does not exist"); }
        gradingFunctionDelegate = method.CreateDelegate<Func<TestCase, ProgramRunContext, double>>();
    }
    
    public double Grade(TestCase tc, ProgramRunContext prc)
    {
        return gradingFunctionDelegate(tc, prc);
    }
    
    private double accuracyScore(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        if (tc.targetOutput.Count != output.Count) { return 0; }
        output.Select((x, i) => Math.Abs(x - tc.targetOutput[i])); //create diff list
        return output.All(d => d < 0.0001) ? 1 : 0; //if all diffs are less than 0.0001, then we have a perfect score
    }
    
    private double hasTargetValue(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        double target = tc.targetOutput[0];
        return hasTargetValue(target, output) ? 1 : 0;
    }

    #region helper functions
    private bool hasTargetValue(double target, List<double> output)
    {
        return output.Any(d => Math.Abs(d - target) < 0.0001);
    }
    #endregion
}