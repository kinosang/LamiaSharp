using LamiaSharp.Expressions;
using LamiaSharp.Values;

// ReSharper disable once CheckNamespace
namespace LamiaSharp.Keywords
{
    internal static partial class InternalKeywords
    {
        public static partial class Control
        {
            public class If : ExpressionList
            {
                public const string Token = "if";

                // TODO: Update Type to actual
                public override string Type { get; set; } = Types.Any;

                public If() : base(Token)
                {
                }

                public override IExpression Evaluate(Environment env)
                {
                    var condition = First.Next;
                    var action1 = condition.Next;
                    var action2 = action1.Next;

                    var result = condition.Value.Evaluate(env);

                    if (!(result is IValue value)) return Nil.Default;

                    switch (value.Type)
                    {
                        case Types.Boolean when (bool)value.Boxed:
                            return action1.Value.Evaluate(env);
                        case Types.String when !string.IsNullOrWhiteSpace((string)value.Boxed):
                            return action1.Value.Evaluate(env);
                        case Types.Integer when (long)value.Boxed > 0:
                        case Types.Double when (double)value.Boxed > 0:
                        case Types.Real when (decimal)value.Boxed > 0:
                            return action1.Value.Evaluate(env);
                    }

                    return action2 == null ? Nil.Default : action2.Value.Evaluate(env);
                }
            }
        }
    }
}
