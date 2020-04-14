using System;

namespace LamiaSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var lisp = @"
    (define fib (lambda (n) ;; Fibonacci!
    (if (< n 2)
      1
  (+ (fib (- n 1)) (fib (- n 2))))))
";

            var expressions = Parser.Parse(lisp);

            Console.WriteLine(expressions);
        }
    }
}
