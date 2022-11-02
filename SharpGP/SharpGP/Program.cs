using SharpGP_Structures.Generator;

int seed = new Random().Next();

SharpGP_Structures.Tree.Program p = Generator.GenerateProgram(seed);
//p = Generator.LoadProgramFromFile("testProgram.txt")

Console.WriteLine(p);
Console.WriteLine("Seed: "+ seed);
