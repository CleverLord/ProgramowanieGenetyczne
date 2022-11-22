using SharpGP_Structures;
using SharpGP_Structures.Generator;
using SharpGP_Structures.Tree;

PRogram p = SharpGP.SharpGP.GenerateProgram_MaxDepth();
PRogram p2 = SharpGP.SharpGP.GenerateProgram_NodeCount();

//PRogram p = SharpGP.SharpGP.LoadProgramFromFile("test1.txt");
//PRogram p2 = SharpGP.SharpGP.LoadProgramFromFile("test2.txt");

//PRogram p = SharpGP.SharpGP.LoadProgramFromString("x_0 = (1 + 3);");
//PRogram p2 = SharpGP.SharpGP.LoadProgramFromString("x_2 = (3 + 2);");
var x = SharpGP.SharpGP.CrossPrograms(p, p2);

Console.WriteLine("Parent1:\n" + p.ToString());
Console.WriteLine("Parent2:\n" + p2.ToString());
Console.WriteLine("Child1:\n" + x.Value.Item1.ToString());
Console.WriteLine("Child2:\n" + x.Value.Item2.ToString());

ProgramRunContext prc=new ProgramRunContext();
p.Invoke(prc);
Console.WriteLine("Result:\n" + prc);