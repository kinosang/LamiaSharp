using System.Collections.Generic;
using System.Linq;
using LamiaSharp.Exceptions;

namespace LamiaSharp.Expressions
{
    public abstract class UnaryExpression : ExpressionList
    {
        protected virtual IEnumerable<string> AllowedTypes => Types.AnyTypes;

        protected UnaryExpression(string op) : base(op)
        {
        }

        public override IExpression Evaluate(Environment env)
        {
            var operand = Values[1];

            if (operand.Type != Types.Any && !AllowedTypes.Contains(Types.Any) && !AllowedTypes.Contains(operand.Type))
            {
                throw new RuntimeException($"Unexpected type, expect {string.Join(", ", AllowedTypes)}, got {operand.Type}");
            }

            return Call(env, Op, operand);
        }

        public abstract IExpression Call(Environment env, string op, IExpression operand);
    }
}
