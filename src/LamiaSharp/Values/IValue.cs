using LamiaSharp.Expressions;

namespace LamiaSharp.Values
{
    public interface IValue : IExpression
    {
        public object Boxed { get; }
    }
}
