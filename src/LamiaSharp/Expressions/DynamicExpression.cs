using System.Collections.Generic;

namespace LamiaSharp.Expressions
{
    public abstract class DynamicExpression : ExpressionList
    {
        protected DynamicExpression(string op) : base(op)
        {
        }

        public override IExpression Evaluate(Environment env)
        {
            var tail = First.Next;

            var arguments = new List<IExpression>();
            while (tail != null)
            {
                arguments.Add(tail.Value);

                tail = tail.Next;
            }

            return Call(env, Op, arguments);
        }

        public abstract IExpression Call(Environment env, string op, IEnumerable<IExpression> arguments);
    }
}
