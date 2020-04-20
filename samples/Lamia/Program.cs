using System.IO;
using LamiaSharp;
using LamiaSharp.Values;
using Console = System.Console;
using ConsoleColor = System.ConsoleColor;

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
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Λ ");
                    Console.ResetColor();

                    var line = Console.ReadLine();

                    if (line == "exit" || line == "quit")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Bye~");
                        Console.ResetColor();

                        return;
                    }

                    try
                    {
                        var program = Parser.Parse(line);

                        program.Environment = env;

                        var result = program.Call(new IValue[0], false);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("= ");
                        Console.ResetColor();

                        Console.WriteLine(result);
                    }
                    catch (System.Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("× ");
                        Console.ResetColor();

                        Console.WriteLine(ex.Message);
                    }
                }
            }

            Usage();
            return;
        }
    }
}
