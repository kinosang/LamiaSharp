namespace LamiaSharp.Expressions
{
    public class ExpressionListNode
    {
        public IExpression Value { get; set; }

        public ExpressionListNode Next { get; set; }

        public ExpressionListNode Previous { get; set; }

        public ExpressionListNode(IExpression value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
