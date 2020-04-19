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
            public class BitNot : UnaryExpression
            {
                public const string Token = "~";

                protected override IEnumerable<string> AllowedTypes => new[] { Types.Integer };

                public override string Type { get; set; } = Types.Integer;

                public BitNot() : base(Token)
                {
                }

                public override IExpression Call(Environment env, string op, IExpression operand)
                {
                    var o = operand.Evaluate(env);

                    if (!(o is IValue v))
                    {
                        throw new System.Exception($"Except value, got {o}");
                    }

                    if (!(v.Boxed is long integer))
                    {
                        throw new System.Exception($"Except integer, got {v.Boxed}");
                    }

                    return new Integer(~integer);
                }
            }
        }
    }
}
