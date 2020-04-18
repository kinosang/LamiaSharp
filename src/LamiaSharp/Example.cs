using System;
using LamiaSharp.Values;

namespace LamiaSharp
{
    public class Example
    {
        static void Main(string[] args)
        {
            const string lisp = @"
 (let fib (lambda (n) ;; Fibonacci!
     (if (< n 2)
         1
         (+ (fib (- n 1)) (fib (- n 2))))))

 (fib 2)
 ";

            var program = Parser.Parse(lisp);

            foreach (var expression in program)
            {
                Console.WriteLine(expression);
            }

            Console.WriteLine(program.Run(new IValue[0]));
        }
    }
}
