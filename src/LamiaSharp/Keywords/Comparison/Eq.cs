using System.Collections.Generic;
using LamiaSharp.Exceptions;
using LamiaSharp.Expressions;
using LamiaSharp.Values;

// ReSharper disable once CheckNamespace
namespace LamiaSharp.Keywords
{
    internal static partial class InternalKeywords
    {
        public static partial class Comparison
        {
            [Alias(Token)]
            public class Eq : BinaryExpression
            {
                public const string Token = "=";

                protected override IEnumerable<string> LeftAllowedTypes => Types.AnyTypes;
                protected override IEnumerable<string> RightAllowedTypes => Types.AnyTypes;

                public override string Type { get; set; } = Types.Boolean;

                public Eq() : base(Token)
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
                        return System.Convert.ToDecimal(lv.Boxed) == System.Convert.ToDecimal(rv.Boxed) ? Boolean.True : Boolean.False;
                    }

                    if (lv.Boxed is double || rv.Boxed is double)
                    {
                        // MARK: use .00001 as tolerance
                        return System.Math.Abs(System.Convert.ToDouble(lv.Boxed) - System.Convert.ToDouble(rv.Boxed)) < .00001 ? Boolean.True : Boolean.False;
                    }

                    if (lv.Boxed is long || rv.Boxed is long)
                    {
                        return System.Convert.ToInt64(lv.Boxed) == System.Convert.ToInt64(rv.Boxed) ? Boolean.True : Boolean.False;
                    }

                    return lv.Boxed.Equals(rv.Boxed) ? Boolean.True : Boolean.False;
                }
            }
        }
    }
}
