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
            public class And : BinaryExpression
            {
                public const string Token = "&&";

                protected override IEnumerable<string> LeftAllowedTypes => Types.AnyTypes;
                protected override IEnumerable<string> RightAllowedTypes => Types.AnyTypes;

                public override string Type { get; set; } = Types.Boolean;

                public And() : base(Token)
                {
                }

                public override IExpression Call(Environment env, string op, IExpression left, IExpression right)
                {
                    return If.EvaluateCondition(env, left) && If.EvaluateCondition(env, right) ? Boolean.True : Boolean.False;
                }
            }
        }
    }
}
