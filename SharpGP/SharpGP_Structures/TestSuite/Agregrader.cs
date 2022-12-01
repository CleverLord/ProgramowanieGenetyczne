namespace SharpGP_Structures.TestSuite;

public class Agregrader
{
    public string agregradingFunctionName;
    public Func<List<double>, double> agregradingFunctionDelegate;
    
    public Agregrader(string agregradingFunction)
    {
        agregradingFunctionName = agregradingFunction;
        var method = GetType().GetMethod(agregradingFunction);
        if (method == null) { throw new Exception("Grading function called " + agregradingFunction + " does not exist"); }
        agregradingFunctionDelegate = method.CreateDelegate<Func<List<double>, double>>();
    }
    
    public double Agregrade(List<double> grades)
    {
        return agregradingFunctionDelegate(grades);
    }

    public double sum(List<double> values) => values.Sum();
    public double avg(List<double> values) => values.Average();
}