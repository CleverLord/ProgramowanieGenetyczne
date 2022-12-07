namespace SharpGP;

public class RandomTester
{
    public static void TestRandom()
    {
        var _rand = new Random();
        //generate 10000 values between 0 and 1 and put them in a list
        var values = new List<double>();
        for (var i = 0; i < 100000; i++) values.Add(_rand.NextDouble());
        //create a 100 bucket histogram
        var histogram = new int[100];
        //for each value, find the bucket it belongs to and increment the count
        foreach (var value in values)
        {
            var bucket = (int)(value * 100);
            histogram[bucket]++;
        }

        //print the histogram
        for (var i = 0; i < 100; i++) Console.WriteLine("{0} - {1}", i, histogram[i]);
    }
}