using System.Collections.Generic;
using LamiaSharp.Exceptions;
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
            public class Lt : BinaryExpression
            {
                public const string Token = "<";

                protected override IEnumerable<string> LeftAllowedTypes => Types.NumericTypes;
                protected override IEnumerable<string> RightAllowedTypes => Types.NumericTypes;

                public override string Type { get; set; } = Types.Boolean;

                public Lt() : base(Token)
                {
                }

                public override IExpression Call(Environment env, string op, IExpression left, IExpression right)
                {
                    var l = left.Evaluate(env);
                    var r = right.Evaluate(env);

                    if (!(l is IValue lv))
                    {
                        throw new RuntimeException($"Except value, got {l}");
                    }

                    if (!(r is IValue rv))
                    {
                        throw new RuntimeException($"Except value, got {r}");
                    }

                    if (lv.Boxed is decimal || rv.Boxed is decimal)
                    {
                        return lv.Boxed as decimal? < (rv.Boxed as decimal?) ? Boolean.True : Boolean.False;
                    }

                    if (lv.Boxed is double || rv.Boxed is double)
                    {
                        return lv.Boxed as double? < (rv.Boxed as double?) ? Boolean.True : Boolean.False;
                    }

                    return lv.Boxed as long? < (rv.Boxed as long?) ? Boolean.True : Boolean.False;
                }
            }
        }
    }
}
