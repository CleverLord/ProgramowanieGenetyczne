using System.Reflection;

namespace SharpGP_Structures.TestSuite;

[Serializable]
public partial class Grader
{
    public string gradingFunctionName;
    [NonSerialized] public Func<TestCase, ProgramRunContext, double> gradingFunctionDelegate;

    public void Initialize()
    {
        var method = GetType().GetMethod(gradingFunctionName);
        if (method == null) { throw new Exception("Grading function called " + gradingFunctionName + " does not exist"); }
        //check for return type
        if (method.ReturnType != typeof(double)) { throw new Exception("Grading function called " + gradingFunctionName + " does not return a double"); }
        //check for params
        var parameters = method.GetParameters();
        if (parameters.Length != 2) { throw new Exception("Grading function called " + gradingFunctionName + " does not have 2 parameters"); }
        if (parameters[0].ParameterType != typeof(TestCase))
        {
            throw new Exception("Grading function called " + gradingFunctionName + " does not have a TestCase as its first parameter");
        }
        if (parameters[1].ParameterType != typeof(ProgramRunContext))
        {
            throw new Exception("Grading function called " + gradingFunctionName + " does not have a ProgramRunContext as its second parameter");
        }
        //create delegate
        gradingFunctionDelegate = method.CreateDelegate<Func<TestCase, ProgramRunContext, double>>();
    }
    public Grader(string gradingFunction)
    {
        gradingFunctionName = gradingFunction;
        Initialize();
    }
    public Grader() // this is needed for deserialization
    { }
    public double Grade(TestCase tc, ProgramRunContext prc)
    {
        //keep in mind, here prc is already populated with the output of the program
        return gradingFunctionDelegate(tc, prc);
    }
    
    #region helper functions

    internal static bool isClose(double a, double b, double Threshold = 0.0001)
    {
        return Math.Abs(a - b) < Threshold;
    }

    #endregion
}

