namespace LamiaSharp.Expressions
{
    public interface IExpression
    {
        public string Type { get; set; }

        public IExpression Evaluate(Environment env);
    }
}
