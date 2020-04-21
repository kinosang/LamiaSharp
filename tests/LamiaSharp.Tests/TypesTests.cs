using LamiaSharp.Expressions;
using LamiaSharp.Values;
using Xunit;

namespace LamiaSharp.Tests
{
    public class TypesTests
    {
        private Environment _env = new Environment();

        [Fact]
        public void TestNilType()
        {
            var nil = Expression.From("nil");
            Assert.IsType<Nil>(nil);
            Assert.Equal("nil", nil.ToString());

            Assert.Equal(nil, nil.Evaluate(_env));
        }

        [Fact]
        public void TestBooleanType()
        {
            var @true = Expression.From("true");
            Assert.IsType<Boolean>(@true);
            Assert.Equal("true", @true.ToString());

            Assert.Equal(@true, @true.Evaluate(_env));

            var @false = Expression.From("false");
            Assert.IsType<Boolean>(@false);
            Assert.Equal("false", @false.ToString());

            Assert.Equal(@false, @false.Evaluate(_env));
        }

        [Fact]
        public void TestStringType()
        {
            var @string = Expression.From("\"Hello World\"");
            Assert.IsType<String>(@string);
            Assert.Equal("\"Hello World\"", @string.ToString());

            Assert.Equal(@string, @string.Evaluate(_env));
        }

        [Fact]
        public void TestIntegerType()
        {
            var integer = Expression.From("42");
            Assert.IsType<Integer>(integer);
            Assert.IsAssignableFrom<INumeric>(integer);
            Assert.Equal("42", integer.ToString());

            Assert.Equal(integer, integer.Evaluate(_env));
        }

        [Fact]
        public void TestDoubleType()
        {
            var @double = Expression.From("4.2");
            Assert.IsType<Double>(@double);
            Assert.IsAssignableFrom<INumeric>(@double);
            Assert.Equal("4.2", @double.ToString());

            Assert.Equal(@double, @double.Evaluate(_env));
        }

        [Fact]
        public void TestRealType()
        {
            var real = Expression.From("4.2m");
            Assert.IsType<Real>(real);
            Assert.IsAssignableFrom<INumeric>(real);
            Assert.Equal("4.2m", real.ToString());

            Assert.Equal(real, real.Evaluate(_env));
        }

        [Fact]
        public void TestCharType()
        {
            var @char = Expression.From("\\c");
            Assert.IsType<Char>(@char);
            Assert.Equal("\\c", @char.ToString());

            Assert.Equal(@char, @char.Evaluate(_env));
        }
    }
}
