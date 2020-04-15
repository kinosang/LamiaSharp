using System;

namespace LamiaSharp
{
    class Program
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

            var expressions = Parser.Parse(lisp);

            foreach (var expression in expressions)
            {
                Console.WriteLine(expression);
            }
        }
    }
}
