using SharpGP_Structures;
using SharpGP_Structures.TestSuite;
namespace SharpGP_Structures.TestSuite;
public partial class Grader
{
    public static double target_final_1__0(TestCase tc, ProgramRunContext prc)
    {
        return Math.Abs(prc.GetOutput().Count - 1);
    }
    public static double target_final_1__1(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        double target = tc.targetOutput[0];

        if (output.Count != 1) return double.MaxValue;

        return Math.Abs(target - output[0]);
    }
    public static double target_final_2__0(TestCase tc, ProgramRunContext prc)
    {
        return Math.Abs(prc.GetOutput().Count - 1);
    }
    public static double target_final_2__1(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        double target = tc.targetOutput[0];

        if (output.Count != 1) return double.MaxValue;

        return Math.Abs(target - output[0]);
    }
    
    public static double target_final_3__0(TestCase tc, ProgramRunContext prc)
    {
        return Math.Abs(prc.GetOutput().Count - 1);
    }
    public static double target_final_3__1(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        if (output.Count != 1) return double.MaxValue;
        if(!prc.input.Contains(output[0])) return double.MaxValue;
        return 0;
    }
    public static double target_final_3__2(TestCase tc, ProgramRunContext prc)
    {
        
        List<double> output = prc.GetOutput();
        if (output.Count != 1) return double.MaxValue;
        double target = tc.targetOutput[0];
        if(target == output[0]) return 0;
        return 1;
    }
    
    public static double target_final_4_1__0(TestCase tc, ProgramRunContext prc)
    {
        return Math.Abs(prc.GetOutput().Count - 1);
    }
    public static double target_final_4_1__1(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        if (output.Count != 1) return double.MaxValue;
        double target = tc.targetOutput[0];
        if(target == output[0]) return 0;
        return 1;
    }
    
    public static double target_final_4_2_1__0(TestCase tc, ProgramRunContext prc)
    {
        return Math.Abs(prc.GetOutput().Count - 1);
    }
    public static double target_final_4_2_1__1(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        if (output.Count != 1) return double.MaxValue;
        double target = tc.targetOutput[0];
        if(target == output[0]) return 0;
        return 1;
    }

    public static double target_final_4_2_2__0(TestCase tc, ProgramRunContext prc)
    {
        return Math.Abs(prc.GetOutput().Count - 1);
    }
    public static double target_final_4_2_2__1(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        if (output.Count != 1) return double.MaxValue;
        double target = tc.targetOutput[0];
        if(target == output[0]) return 0;
        return 1;
    }
    public static double target_final_4_2_3__0(TestCase tc, ProgramRunContext prc)
    {
        return Math.Abs(prc.GetOutput().Count - 1);
    }
    public static double target_final_4_2_3__1(TestCase tc, ProgramRunContext prc)
    {
        List<double> output = prc.GetOutput();
        if (output.Count != 1) return double.MaxValue;
        double target = tc.targetOutput[0];
        if(target == output[0]) return 0;
        return 1;
    }
}