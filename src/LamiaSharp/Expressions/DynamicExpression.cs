using System.Collections.Generic;
using System.Linq;

namespace LamiaSharp.Expressions
{
    public abstract class DynamicExpression : ExpressionList
    {
        protected DynamicExpression(string op) : base(op)
        {
        }

        public override IExpression Evaluate(Environment env)
        {
            return Call(env, Op, Values.Skip(1));
        }

        public abstract IExpression Call(Environment env, string op, IEnumerable<IExpression> arguments);
    }
}
