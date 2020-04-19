using LamiaSharp.Exceptions;
using LamiaSharp.Expressions;
using LamiaSharp.Values;

// ReSharper disable once CheckNamespace
namespace LamiaSharp.Keywords
{
    internal static partial class InternalKeywords
    {
        public static partial class Base
        {
            public class Let : BinaryExpression
            {
                public const string Token = "let";

                // TODO: Update Type to actual
                public override string Type { get; set; } = Types.Any;

                public Let() : base(Token)
                {
                }

                public override IExpression Call(Environment env, string op, IExpression left, IExpression right)
                {
                    if (!(left is Symbol symbol))
                    {
                        throw new RuntimeException($"Expect key, got {left.GetType()}.");
                    }

                    env[symbol.ToString()] = right.Evaluate(env);

                    return right;
                }
            }
        }
    }
}
