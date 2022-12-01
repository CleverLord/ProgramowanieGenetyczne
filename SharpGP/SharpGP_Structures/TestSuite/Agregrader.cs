namespace SharpGP_Structures.TestSuite;

public class Agregrader
{
    public Agregrader(string mean)
    {
        
    }
    public double Agregrade(List<double> values) => values.Sum();
}