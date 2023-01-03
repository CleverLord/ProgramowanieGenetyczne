namespace SharpGP_Structures.TestSuite;

[Serializable]
public class Agregrader
{
    public string agregradingFunctionName;
    public Func<List<double>, double> agregradingFunctionDelegate;
    
    public void Initialize()
    {
        var method = GetType().GetMethod(agregradingFunctionName);
        if (method == null) { throw new Exception("Grading function called " + agregradingFunctionName + " does not exist"); }
        //check for returned type
        if (method.ReturnType != typeof(double)) { throw new Exception("Grading function called " + agregradingFunctionName + " does not return a double"); }
        //check for parameters
        var parameters = method.GetParameters();
        if (parameters.Length != 1) { throw new Exception("Grading function called " + agregradingFunctionName + " does not have exactly one parameter"); }
        if (parameters[0].ParameterType != typeof(List<double>)) { throw new Exception("Grading function called " + agregradingFunctionName + " does not have a parameter of type List<double>"); }
        //create delegate
        agregradingFunctionDelegate = method.CreateDelegate<Func<List<double>, double> >();
    }
    
    public Agregrader(string agregradingFunction)
    {
        agregradingFunctionName = agregradingFunction;
        Initialize();
    }
    
    public double Agregrade(List<double> grades)
    {
        return agregradingFunctionDelegate(grades);
    }

    public static double sum(List<double> values)
    {
        return values.Sum();
    }

    public static double avg(List<double> values)
    {
        return values.Average();
    }
}