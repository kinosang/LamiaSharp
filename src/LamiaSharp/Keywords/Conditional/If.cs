using LamiaSharp.Expressions;
using LamiaSharp.Values;

// ReSharper disable once CheckNamespace
namespace LamiaSharp.Keywords
{
    internal static partial class InternalKeywords
    {
        public static partial class Conditional
        {
            public class If : ExpressionList
            {
                public const string Token = "if";

                // TODO: Update Type to actual
                public override string Type { get; set; } = Types.Any;

                public If() : base(Token)
                {
                }

                public static bool EvaluateCondition(Environment env, IExpression condition)
                {
                    var result = condition.Evaluate(env);

                    if (!(result is IValue value)) return false;

                    switch (value.Type)
                    {
                        case Types.Boolean when (bool)value.Boxed:
                        case Types.String when !string.IsNullOrWhiteSpace((string)value.Boxed):
                        case Types.Integer when (long)value.Boxed > 0:
                        case Types.Double when (double)value.Boxed > 0:
                        case Types.Real when (decimal)value.Boxed > 0:
                            return true;
                    }

                    return false;
                }

                public override IExpression Evaluate(Environment env)
                {
                    var condition = Values[1];
                    var action1 = Values[2];
                    var action2 = Values[3];

                    if (EvaluateCondition(env, condition))
                    {
                        return action1.Evaluate(env);
                    }

                    return action2 == null ? Nil.Default : action2.Evaluate(env);
                }
            }
        }
    }
}
