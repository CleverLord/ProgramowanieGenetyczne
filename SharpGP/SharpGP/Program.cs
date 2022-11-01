// See https://aka.ms/new-console-template for more information
using SharpGP_Structures.Generator;

//good seeds:
//2101181429 1921049680 94118497 1364541322 1913066927

//bad seeds:
//1033321198 908716397 539831539

SharpGP_Structures.Tree.Program p = Generator.GenerateProgram();
Console.WriteLine("");
int seed =new Random().Next();
p.rand=new Random(seed);
Console.WriteLine("Seed: "+ seed);
Console.WriteLine(p);
