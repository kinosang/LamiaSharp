using System.IO;
using LamiaSharp;
using LamiaSharp.Values;
using Console = System.Console;

namespace Lamia
{
    class Program
    {
        static void Usage()
        {
            Console.WriteLine("Usage: lamia < -i | file.ls >");

            return;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Lamia script interpreter");

            if (args.Length != 1)
            {
                Usage();
                return;
            }

            if (File.Exists(args[0]))
            {
                var program = Parser.Parse(File.ReadAllText(args[0]));

                Console.WriteLine(program.Run(new IValue[0]));

                return;
            }

            if (args[0] == "-i")
            {
                Console.WriteLine("interactive environment");

                var env = new Environment();

                while (true)
                {
                    Console.Write("Λ ");

                    var line = Console.ReadLine();

                    try
                    {
                        var program = Parser.Parse(line);

                        program.Environment = env;

                        Console.Write("= ");

                        Console.WriteLine(program.Call(new IValue[0], false));
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            Usage();
            return;
        }
    }
}
