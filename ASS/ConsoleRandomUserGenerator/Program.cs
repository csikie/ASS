using System;
using ASS.RandomUserGenerator.Generators;

namespace ConsoleRandomUserGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            RandomNameGenerator hunNameGenerator = new RandomNameGenerator(rand, "HUN");
            RandomNameGenerator enNameGenerator = new RandomNameGenerator(rand, "EN");
            for (int i = 0; i < 100; i++)
            {
                string name = hunNameGenerator.Generate((Sex)rand.Next(2), rand.NextDouble() < 0.3 ? 1 : 0);
                Console.WriteLine($"{name} {NeptunCodeGenerator.GenerateNeptunCode()}");
            }
        }
    }
}
