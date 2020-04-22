using LamiaSharp.Values;
using Xunit;

namespace LamiaSharp.Tests
{
    public class EvaluationTests
    {
        [Fact]
        public void TestEvaluateConstant()
        {
            var constant = new Integer(42);

            Assert.Equal(constant, constant.Evaluate(new Environment()));
            Assert.Equal(42L, constant.Source);
        }

        [Fact]
        public void TestEvaluateSymbol()
        {
            var env = new Environment();

            var constant = new Integer(42);
            var symbol = new Symbol("n");

            env[symbol.Source] = constant;

            Assert.Equal(constant, symbol.Evaluate(env));
        }
    }
}
