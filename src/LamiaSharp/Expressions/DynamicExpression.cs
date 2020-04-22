using System;
using System.Collections.Generic;
using System.Linq;
using LamiaSharp.Exceptions;

namespace LamiaSharp.Expressions
{
    public abstract class DynamicExpression : ExpressionList
    {
        protected virtual Range Range { get; } = new Range(1, int.MaxValue);

        protected DynamicExpression(string op) : base(op)
        {
        }

        public override IExpression Evaluate(Environment env)
        {
            var count = Values.Count - 1;
            if (count < Range.Start.Value || count > Range.End.Value)
            {
                throw new RuntimeException($"Expect {Range.Start}..{Range.End} arguments, got {count}");
            }

            return Call(env, Op, Values.Skip(1));
        }

        public abstract IExpression Call(Environment env, string op, IEnumerable<IExpression> arguments);
    }
}
