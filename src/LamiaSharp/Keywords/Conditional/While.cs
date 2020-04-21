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
            public class While : ExpressionList
            {
                public const string Token = "while";

                // TODO: Update Type to actual
                public override string Type { get; set; } = Types.Any;

                public While() : base(Token)
                {
                }

                public override IExpression Evaluate(Environment env)
                {
                    var condition = Values[1];

                    IExpression result = Nil.Default;

                    while (If.EvaluateCondition(env, condition))
                    {
                        foreach (var action in Values.Skip(2))
                        {
                            result = action.Evaluate(env);
                        }
                    }

                    return result;
                }
            }
        }
    }
}
