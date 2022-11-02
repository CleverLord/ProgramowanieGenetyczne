using SharpGP_Structures.Generator;
using SharpGP_Structures.Tree;

int seed = new Random().Next();

PRogram p = SharpGP.GenerateProgram(seed);
//p = Generator.LoadProgramFromFile("testProgram.txt")

Console.WriteLine(p);
Console.WriteLine("Seed: "+ seed);
