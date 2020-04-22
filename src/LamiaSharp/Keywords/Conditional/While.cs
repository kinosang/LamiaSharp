using System;
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
            public class While : DynamicExpression
            {
                public const string Token = "while";

                protected override Range Range => new Range(2, int.MaxValue);

                // TODO: Update Type to actual
                public override string Type { get; set; } = Types.Any;

                public While() : base(Token)
                {
                }

                public override IExpression Call(Environment env, string op, IEnumerable<IExpression> arguments)
                {
                    var expressions = arguments.ToArray();

                    var condition = expressions.First();

                    IExpression result = Nil.Default;

                    while (If.EvaluateCondition(env, condition))
                    {
                        foreach (var action in expressions.Skip(1))
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
