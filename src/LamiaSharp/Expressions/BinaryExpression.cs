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
            if (Values.Count != 3)
            {
                throw new RuntimeException($"Expect 2 arguments, got {Values.Count - 1}");
            }

            var left = Values[1];
            var right = Values[2];

            if (left.Type != Types.Any && !LeftAllowedTypes.Contains(Types.Any) && !LeftAllowedTypes.Contains(left.Type))
            {
                throw new RuntimeException($"Unexpected type, expect {string.Join(", ", LeftAllowedTypes)}, got {left.Type}");
            }

            if (right.Type != Types.Any && !RightAllowedTypes.Contains(Types.Any) && !RightAllowedTypes.Contains(right.Type))
            {
                throw new RuntimeException($"Unexpected type, expect {string.Join(", ", LeftAllowedTypes)}, got {right.Type}");
            }

            return Call(env, Op, left, right);
        }

        public abstract IExpression Call(Environment env, string op, IExpression left, IExpression right);
    }
}
