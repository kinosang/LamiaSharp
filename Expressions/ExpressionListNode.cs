namespace LamiaSharp.Expressions
{
    public class ExpressionListNode
    {
        public Expression Value { get; set; }

        public ExpressionListNode Next { get; set; }

        public ExpressionListNode Previous { get; set; }

        public ExpressionListNode(Expression value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
