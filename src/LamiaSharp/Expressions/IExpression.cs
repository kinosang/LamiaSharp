namespace LamiaSharp.Expressions
{
    public interface IExpression
    {
        public string Type { get; }

        public IExpression Evaluate(Environment env);
    }
}
