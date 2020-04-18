using System.Collections.Generic;
using System.Linq;
using LamiaSharp.Exceptions;

namespace LamiaSharp.Expressions
{
    public abstract class BinaryExpression : ExpressionList
    {
        protected virtual IEnumerable<string> LeftAllowedTypes => Types.AnyTypes;
        protected virtual IEnumerable<string> RightAllowedTypes => Types.AnyTypes;

        protected BinaryExpression(string op) : base(op)
        {
        }

        public override IExpression Evaluate(Environment env)
        {
            var left = First.Next;
            var right = left.Next;

            if (left.Value.Type != Types.Any && !LeftAllowedTypes.Contains(Types.Any) && !LeftAllowedTypes.Contains(left.Value.Type))
            {
                throw new RuntimeException($"Unexpected type, expect {string.Join(", ", LeftAllowedTypes)}, got {left.Value.Type}");
            }

            if (right.Value.Type != Types.Any && !RightAllowedTypes.Contains(Types.Any) && !RightAllowedTypes.Contains(right.Value.Type))
            {
                throw new RuntimeException($"Unexpected type, expect {string.Join(", ", LeftAllowedTypes)}, got {right.Value.Type}");
            }

            return Call(env, Op, left.Value, right.Value);
        }

        public abstract IExpression Call(Environment env, string op, IExpression left, IExpression right);
    }
}
