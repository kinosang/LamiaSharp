using LamiaSharp.Expressions;
using LamiaSharp.Values;
using Xunit;

namespace LamiaSharp.Tests
{
    public class TypesTests
    {
        [Fact]
        public void TestNilType()
        {
            var nil = Expression.From("nil");
            Assert.IsType<Nil>(nil);
            Assert.Equal("nil", nil.ToString());
        }

        [Fact]
        public void TestBooleanType()
        {
            var @true = Expression.From("true");
            Assert.IsType<Boolean>(@true);
            Assert.Equal("true", @true.ToString());

            var @false = Expression.From("false");
            Assert.IsType<Boolean>(@false);
            Assert.Equal("false", @false.ToString());
        }

        [Fact]
        public void TestStringType()
        {
            var @string = Expression.From("\"Hello World\"");
            Assert.IsType<String>(@string);
            Assert.Equal("\"Hello World\"", @string.ToString());
        }

        [Fact]
        public void TestIntegerType()
        {
            var integer = Expression.From("42");
            Assert.IsType<Integer>(integer);
            Assert.IsAssignableFrom<Number>(integer);
            Assert.Equal("42", integer.ToString());
        }

        [Fact]
        public void TestDoubleType()
        {
            var number = Expression.From("4.2");
            Assert.IsType<Double>(number);
            Assert.IsAssignableFrom<Number>(number);
            Assert.Equal("4.2", number.ToString());
        }

        [Fact]
        public void TestRealType()
        {
            var real = Expression.From("4.2m");
            Assert.IsType<Real>(real);
            Assert.IsAssignableFrom<Number>(real);
            Assert.Equal("4.2m", real.ToString());
        }
    }
}
