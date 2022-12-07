using SharpGP_Structures;
using SharpGP.Utils;

//TODO: rework following code to test single functions, instead of what the work was focused on last time
//Here test generating with max depth

//Here test generating with max node count

//Here test loading from file

//Here test loading from string

//Here test program crossing

//Here test program mutation

// OLD stuff below //

var p = TreeGenerator.GenerateProgram_MaxDepth(15);
var p2 = TreeGenerator.GenerateProgram_NodeCount();
var e = TreeGenerator.LoadProgramFromFile("testProgram.txt");
//PRogram p = SharpGP.SharpGP.LoadProgramFromFile("test1.txt");
//PRogram p2 = SharpGP.SharpGP.LoadProgramFromFile("test2.txt");

//PRogram p = SharpGP.SharpGP.LoadProgramFromString("x_0 = (1 + 3);");
//PRogram p2 = SharpGP.SharpGP.LoadProgramFromString("x_2 = (3 + 2);");
var x = SharpGP.SharpGP.CrossPrograms(p, p2);

Console.WriteLine("Parent1:\n" + p);
Console.WriteLine("Parent2:\n" + p2);
Console.WriteLine("Child1:\n" + x.Value.Item1);
Console.WriteLine("Child2:\n" + x.Value.Item2);

var prc = new ProgramRunContext();
var p3 = TreeGenerator.GenerateProgram_MaxDepth(50);
Console.WriteLine("P3:\n" + p3);
p3.Invoke(prc);
Console.WriteLine("Result:\n" + prc);

Console.WriteLine("--------------------------------------------");
for (var i = 1; i < 10; i++)
{
    var p4 = TreeGenerator.GenerateProgram_MaxDepth(15);
    Console.WriteLine("P4:\n" + p4);
    //print depth
    Console.WriteLine("Depth: " + p4.GetDepth());
    Console.WriteLine("--------------");
}