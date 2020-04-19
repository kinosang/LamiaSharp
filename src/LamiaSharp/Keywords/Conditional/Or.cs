using System.Collections.Generic;
using System.Linq;
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
            public class Or : DynamicExpression
            {
                public const string Token = "||";

                public override string Type { get; set; } = Types.Integer;

                public Or() : base(Token)
                {
                }

                public override IExpression Call(Environment env, string op, IEnumerable<IExpression> arguments)
                {
                    var values = arguments.ToArray();

                    var head = values.First();
                    var tails = values.Skip(1).ToArray();

                    return tails.Aggregate(If.EvaluateCondition(env, head), (acc, v) => acc || If.EvaluateCondition(env, v)) ? Boolean.True : Boolean.False;
                }
            }
        }
    }
}
