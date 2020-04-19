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
            public class Dec : UnaryExpression
            {
                public const string Token = "--";

                protected override IEnumerable<string> AllowedTypes => Types.NumericTypes;

                // TODO: Update Type to actual
                public override string Type { get; set; } = Types.Any;

                public Dec() : base(Token)
                {
                }

                public override IExpression Call(Environment env, string op, IExpression operand)
                {
                    var o = operand.Evaluate(env);

                    if (!(o is IValue v))
                    {
                        throw new System.Exception($"Except value, got {o}");
                    }

                    return v.Boxed switch
                    {
                        long integer => new Integer(--integer),
                        decimal @decimal => new Real(--@decimal),
                        double @double => new Double(--@double),
                        _ => throw new System.Exception($"Except numeric, got {v.Boxed}")
                    };
                }
            }
        }
    }
}
