using System.Collections.Generic;
using LamiaSharp.Expressions;
using LamiaSharp.Values;

// ReSharper disable once CheckNamespace
namespace LamiaSharp.Keywords
{
    internal static partial class InternalKeywords
    {
        public static partial class Bitwise
        {
            [Alias(Token)]
            public class BitXor : BinaryExpression
            {
                public const string Token = "^";

                protected override IEnumerable<string> LeftAllowedTypes => new[] { Types.Integer };
                protected override IEnumerable<string> RightAllowedTypes => new[] { Types.Integer };

                public override string Type { get; set; } = Types.Integer;

                public BitXor() : base(Token)
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

                    if (!(lv.Boxed is long lvalue))
                    {
                        throw new System.Exception($"Except integer, got {rv.Boxed}");
                    }

                    if (!(rv.Boxed is long rvalue))
                    {
                        throw new System.Exception($"Except integer, got {rv.Boxed}");
                    }

                    return new Integer(lvalue ^ rvalue);
                }
            }
        }
    }
}
