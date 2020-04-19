using System.Collections.Generic;
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
            public class Minus : BinaryExpression
            {
                public const string Token = "-";

                protected override IEnumerable<string> LeftAllowedTypes => Types.NumericTypes;
                protected override IEnumerable<string> RightAllowedTypes => Types.NumericTypes;

                // TODO: Update Type to actual
                public override string Type { get; set; } = Types.Any;

                public Minus() : base(Token)
                {
                }

                public override IExpression Call(Environment env, string op, IExpression left, IExpression right)
                {
                    var l = left.Evaluate(env);
                    var r = right.Evaluate(env);

                    if (!(l is IValue lv))
                    {
                        throw new System.Exception($"Except value, got {l}");
                    }

                    if (!(r is IValue rv))
                    {
                        throw new System.Exception($"Except value, got {r}");
                    }

                    if (lv.Boxed is decimal || rv.Boxed is decimal)
                    {
                        var result = (lv.Boxed as decimal?) - (rv.Boxed as decimal?);
                        return new Real(result ?? 0);
                    }

                    if (lv.Boxed is double || rv.Boxed is double)
                    {
                        var result = (lv.Boxed as double?) - (rv.Boxed as double?);
                        return new Double(result ?? 0);
                    }

                    var final = (lv.Boxed as long?) - (rv.Boxed as long?);
                    return new Integer(final ?? 0);
                }
            }
        }
    }
}
