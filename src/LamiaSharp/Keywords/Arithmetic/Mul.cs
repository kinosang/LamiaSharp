using System.Collections.Generic;
using System.Linq;
using LamiaSharp.Expressions;
using LamiaSharp.Values;

// ReSharper disable once CheckNamespace
namespace LamiaSharp.Keywords
{
    internal static partial class InternalKeywords
    {
        public static partial class Arithmetic
        {
            [Alias(Token)]
            public class Mul : DynamicExpression
            {
                public const string Token = "*";

                // TODO: Update Type to actual
                public override string Type { get; set; } = Types.Any;

                public Mul() : base(Token)
                {
                }

                public override IExpression Call(Environment env, string op, IEnumerable<IExpression> arguments)
                {
                    var values = arguments.Select(a => a.Evaluate(env)).OfType<INumeric>().ToArray();

                    if (values.Any(v => v.Boxed is decimal))
                    {
                        var result = values.Aggregate(1M, (acc, v) => acc * (decimal)v.Boxed);
                        return new Real(result);
                    }

                    if (values.Any(v => v.Boxed is double))
                    {
                        var result = values.Aggregate(1D, (acc, v) => acc * (double)v.Boxed);
                        return new Double(result);
                    }

                    var final = values.Aggregate(1L, (acc, v) => acc * (long)v.Boxed);
                    return new Integer(final);
                }
            }
        }
    }
}
