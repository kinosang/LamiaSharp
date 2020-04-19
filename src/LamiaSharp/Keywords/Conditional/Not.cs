using System.Collections.Generic;
using LamiaSharp.Expressions;
using LamiaSharp.Values;

// ReSharper disable once CheckNamespace
namespace LamiaSharp.Keywords
{
    internal static partial class InternalKeywords
    {
        public static partial class Conditional
        {
            [Alias(Token)]
            public class Not : UnaryExpression
            {
                public const string Token = "!";

                protected override IEnumerable<string> AllowedTypes => Types.AnyTypes;

                public override string Type { get; set; } = Types.Boolean;

                public Not() : base(Token)
                {
                }

                public override IExpression Call(Environment env, string op, IExpression operand)
                {
                    return If.EvaluateCondition(env, operand) ? Boolean.False : Boolean.True;
                }
            }
        }
    }
}
