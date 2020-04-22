using System.Linq;
using LamiaSharp.Expressions;
using LamiaSharp.Keywords;
using LamiaSharp.Values;
using Xunit;

namespace LamiaSharp.Tests
{
    public class ParserTests
    {
        public ParserTests()
        {
            Parser.Boot();
        }

        [Fact]
        public void TestParseConstants()
        {
            var constant = Parser.Parse("42");

            Assert.IsType<ExpressionList>(constant);
            Assert.Equal("42", constant.Op);

            var first = constant.First();

            Assert.IsType<Integer>(first);
            Assert.Equal("42", first.ToString());
        }

        [Fact]
        public void TestParseList()
        {
            var list = Parser.Parse("(42 42)");

            Assert.IsType<ExpressionList>(list);

            var first = list.First();

            Assert.IsType<Integer>(first);
            Assert.Equal("42", first.ToString());

            var second = list.Skip(1).First();

            Assert.IsType<Integer>(second);
            Assert.Equal("42", second.ToString());
        }

        [Fact]
        public void TestParseNestedList()
        {
            var list = Parser.Parse("((42 42) 42)");

            Assert.IsType<ExpressionList>(list);

            var first = list.First();

            Assert.IsType<ExpressionList>(first);
            Assert.Equal("(42 42)", first.ToString());

            var second = list.Skip(1).First();

            Assert.IsType<Integer>(second);
            Assert.Equal("42", second.ToString());
        }

        [Fact]
        public void TestParseProcedure()
        {
            var procedure = Parser.Parse(@"
(let fib (lambda (n) ;; Fibonacci!
    (if (< n 2)
        1
        (+ (fib (- n 1)) (fib (- n 2)) )
    )
))");

            Assert.IsAssignableFrom<ExpressionList>(procedure);
            Assert.IsAssignableFrom<BinaryExpression>(procedure);
            Assert.IsType<InternalKeywords.Base.Let>(procedure);
            Assert.Equal("let", procedure.Op);

            var first = procedure.First();

            Assert.IsType<Symbol>(first);
            Assert.Equal("let", first.ToString());
            
            var second = procedure.Skip(1).First();

            Assert.IsType<Symbol>(second);
            Assert.Equal("fib", second.ToString());
            
            var third = procedure.Skip(2).First();

            Assert.IsType<InternalKeywords.Base.Lambda>(third);
            Assert.StartsWith("(λ", third.ToString());
        }
    }
}
