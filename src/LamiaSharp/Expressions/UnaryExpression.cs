namespace LamiaSharp.Expressions
{
    public abstract class UnaryExpression : ExpressionList
    {
        protected UnaryExpression(string op) : base(op)
        {
        }

        public override IExpression Evaluate(Environment env)
        {
            var operand = First.Next;

            return Call(env, Op, operand.Value);
        }

        public abstract IExpression Call(Environment env, string op, IExpression operand);
    }
}
