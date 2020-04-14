namespace LamiaSharp.Expression
{
    public class ExpListNode
    {
        public IExpression Value { get; set; }

        public ExpListNode Next { get; set; }

        public ExpListNode Previous { get; set; }

        public ExpListNode(IExpression value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
