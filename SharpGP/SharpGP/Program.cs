using SharpGP_Structures.Generator;
using SharpGP_Structures.Tree;

PRogram p = SharpGP.GenerateProgram();
PRogram p2 = SharpGP.GenerateProgram();
//p = Generator.LoadProgramFromFile("testProgram.txt")
var x = SharpGP.CrossPrograms(p, p2);

Console.WriteLine("Parent1:\n " + p.ToString());
Console.WriteLine("Parent2:\n " + p2.ToString());
Console.WriteLine("Child1:\n " + x.Value.Item1.ToString());
Console.WriteLine("Child2:\n " + x.Value.Item2.ToString());

ProgramRunContext prc=new ProgramRunContext();
p.Invoke(prc);
Console.WriteLine("Result:\n" + prc);