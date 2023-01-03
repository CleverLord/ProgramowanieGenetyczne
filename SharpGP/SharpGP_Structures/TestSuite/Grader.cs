using System.Reflection;

namespace SharpGP_Structures.TestSuite;

[Serializable]
public class Grader
{
    public string gradingFunctionName;
    public Func<TestCase, ProgramRunContext, double> gradingFunctionDelegate;

    public void Initialize()
    {
        var method = GetType().GetMethod(gradingFunctionName);
        if (method == null) { throw new Exception("Grading function called " + gradingFunctionName + " does not exist"); }
        //check for return type
        if(method.ReturnType != typeof(double)) { throw new Exception("Grading function called " + gradingFunctionName + " does not return a double"); }
        //check for params
        var parameters = method.GetParameters();
        if(parameters.Length != 2) { throw new Exception("Grading function called " + gradingFunctionName + " does not have 2 parameters"); }
        if(parameters[0].ParameterType != typeof(TestCase)) { throw new Exception("Grading function called " + gradingFunctionName + " does not have a TestCase as its first parameter"); }
        if(parameters[1].ParameterType != typeof(ProgramRunContext)) { throw new Exception("Grading function called " + gradingFunctionName + " does not have a ProgramRunContext as its second parameter"); }
        //create delegate
        gradingFunctionDelegate = method.CreateDelegate<Func<TestCase, ProgramRunContext, double>>();
    }
    public Grader(string gradingFunction)
    {
        gradingFunctionName = gradingFunction;
        Initialize();
    }
    public double Grade(TestCase tc, ProgramRunContext prc)
    {
        //keep in mind, here prc is already populated with the output of the program
        return gradingFunctionDelegate(tc, prc);
    }
    
    public static double bestAbsoluteError(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        var absDiffs = output.Select((x, i) => Math.Abs(x - tc.targetOutput[i])).ToList(); //array index out of range
        return absDiffs.Min();
    }

    public static double target_1_1_A(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_2_A(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }

    public static double target_1_2_B(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count != 1) { return Double.MaxValue; }
        var absDiff = Math.Abs(prc.GetOutput()[0] - (tc.input[0] + tc.input[1]));
        return absDiff;
    }
    public static double target_1_2_C(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count != 1) { return Double.MaxValue; }
        var absDiff = Math.Abs(prc.GetOutput()[0] - (tc.input[0] * tc.input[1]));
        return absDiff;
    }
    public static double target_1_2_D(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count != 1) { return Double.MaxValue; }
        var absDiff = Math.Abs(prc.GetOutput()[0] - (tc.input[0] / tc.input[1]));
        return absDiff;
    }
    public static double target_1_2_E(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count != 1) { return Double.MaxValue; }
        var absDiff = Math.Abs(prc.GetOutput()[0] - (tc.input[0] / tc.input[1]));
        return absDiff;
    }

    public static double target_1_3_A(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_3_B(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_4_A__1(TestCase tc, ProgramRunContext prc)
    {
        return prc.GetOutput().Count - 1;
    }
    public static double target_1_4_A__0(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        double target = tc.targetOutput[0];

        if (output.Count != 1)
            return double.MaxValue;

        return target - output[0];
    }
    public static double target_1_4_B__1(TestCase tc, ProgramRunContext prc)
    {
        return prc.GetOutput().Count - 1;
    }
    public static double target_1_4_B__0(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        double target = tc.targetOutput[0];

        if (output.Count != 1)
            return double.MaxValue;

        return target - output[0];
    }
    public static double AtFirstPlace(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        double target = tc.targetOutput[0];
        return Close(output[0], target) ? 0 : 1;
    }
    public static double justOneTargetValue(TestCase tc, ProgramRunContext prc)
    {
        if(prc.GetOutput().Count != 1) { return 1; }
        return  Close(prc.GetOutput()[0], tc.targetOutput[0]) ? 0 : 1;
    }

    public static bool Close(double a, double b)
    {
        return Math.Abs(a - b) < 0.0001;
    }
    
    #region helper functions
    public static bool hasTargetValue(double target, List<double> output)
    {
        return output.Any(d => isClose(d, target));
    }
    public static bool isClose(double a, double b, double Threshold = 0.0001)
    {
        return Math.Abs(a - b) < Threshold;
    }
    #endregion
}