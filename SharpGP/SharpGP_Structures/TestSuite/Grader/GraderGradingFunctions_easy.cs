using SharpGP_Structures;
using SharpGP_Structures.TestSuite;
namespace SharpGP_Structures.TestSuite;
public partial class Grader
{
    public static double target_1_1_A(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_1_B(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_1_C(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_1_D__0(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_1_D__1(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        double target = tc.targetOutput[0];
        return isClose(output[0], target) ? 0 : 1;
    }
    public static double target_1_1_E__0(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_1_E__1(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        double target = tc.targetOutput[0];
        return isClose(output[0], target) ? 0 : 1;
    }
    public static double target_1_1_F__0(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_1_F__1(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        double target = tc.targetOutput[0];
        return isClose(output[0], target) ? 0 : 1;
    }
    public static double target_1_1_F__2(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count != 1) { return Double.MaxValue; }
        return prc.GetOutput()[0] - tc.targetOutput[0];
    }
    
    public static double target_1_2_A__0(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_2_A__1(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        if (prc.GetOutput().Count != 1) { return prc.GetOutput().Count; }
        var absDiff = prc.GetOutput()[0]- tc.targetOutput[0];
        return absDiff;
    } 

    public static double target_1_2_B__0(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_2_B__1(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        if (prc.GetOutput().Count != 1) { return prc.GetOutput().Count; }
        var absDiff = prc.GetOutput()[0]- tc.targetOutput[0];
        return absDiff;
    } 
    public static double target_1_2_C__0(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_2_C__1(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        if (prc.GetOutput().Count != 1) { return prc.GetOutput().Count; }
        var absDiff = prc.GetOutput()[0]- tc.targetOutput[0];
        return absDiff;
    } 
    public static double target_1_2_D__0(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_2_D__1(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        if (prc.GetOutput().Count != 1) { return prc.GetOutput().Count; }
        var absDiff = prc.GetOutput()[0]- tc.targetOutput[0];
        return absDiff;
    } 
    public static double target_1_2_E__0(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_2_E__1(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        if (prc.GetOutput().Count != 1) { return prc.GetOutput().Count; }
        var absDiff = prc.GetOutput()[0]- tc.targetOutput[0];
        return absDiff;
    } 

    public static double target_1_3_A__0(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_3_A__1(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        if (prc.GetOutput().Count != 1) { return prc.GetOutput().Count; }
        var absDiff = prc.GetOutput()[0]- tc.targetOutput[0];
        return absDiff;
    }
    public static double target_1_3_B__0(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        var absDiff = prc.GetOutput().Select(x => Math.Abs(x - 1)).ToList();
        return absDiff.Min();
    }
    public static double target_1_3_B__1(TestCase tc, ProgramRunContext prc)
    {
        if (prc.GetOutput().Count == 0) { return Double.MaxValue; }
        if (prc.GetOutput().Count != 1) { return prc.GetOutput().Count; }
        var absDiff = prc.GetOutput()[0]- tc.targetOutput[0];
        return absDiff;
    } 
    
    public static double target_1_4_A__0(TestCase tc, ProgramRunContext prc)
    {
        return prc.GetOutput().Count - 1;
    }
    public static double target_1_4_A__1(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        double target = tc.targetOutput[0];

        if (output.Count != 1) return double.MaxValue;

        return target - output[0];
    }
    public static double target_1_4_B__0(TestCase tc, ProgramRunContext prc)
    {
        return prc.GetOutput().Count - 1;
    }
    public static double target_1_4_B__1(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        double target = tc.targetOutput[0];

        if (output.Count != 1) return double.MaxValue;

        return target - output[0];
    }
}