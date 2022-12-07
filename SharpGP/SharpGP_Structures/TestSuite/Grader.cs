namespace SharpGP_Structures.TestSuite;

[Serializable]
public class Grader
{
    public Func<TestCase, ProgramRunContext, double> gradingFunctionDelegate;
    public string gradingFunctionName;

    public Grader(string gradingFunction)
    {
        gradingFunctionName = gradingFunction;
        Initialize();
    }

    public void Initialize()
    {
        var method = GetType().GetMethod(gradingFunctionName);
        if (method == null) throw new Exception("Grading function called " + gradingFunctionName + " does not exist");
        gradingFunctionDelegate = method.CreateDelegate<Func<TestCase, ProgramRunContext, double>>();
    }

    public static double JustTargetValue(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count != 1) return 1;
        return prc.GetOutput()[0] - tc.targetOutput[0] < 0.0001 ? 0 : 1;
    }

    public double Grade(TestCase tc, ProgramRunContext prc)
    {
        //keep in mind, here prc is already populated with the output of the program
        return gradingFunctionDelegate(tc, prc);
    }


    public static double target_1_1_A(TestCase tc, ProgramRunContext prc)
    {
        //Program powinien wygenerować na wyjściu (na dowolnej pozycji w danych wyjściowych) liczbę 1. Poza liczbą 1 może też zwrócić inne liczby.
        //róznicę którejkolwiek z liczb trzeba minimalizować

        if (prc.GetOutput().Count == 0) return double.MaxValue;
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }

    public static double target_1_3_A(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) return double.MaxValue;
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }

    public static double target_1_4_A__1(TestCase tc, ProgramRunContext prc)
    {
        return prc.GetOutput().Count - 1;
    }

    public static double target_1_4_A__0(TestCase tc, ProgramRunContext prc)
    {
        var output = prc.GetOutput();
        var target = tc.targetOutput[0];

        if (output.Count != 1)
            return double.MaxValue;

        return target - output[0];
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