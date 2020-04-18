using LamiaSharp.Expressions;

namespace LamiaSharp.Values
{
    public class Value<T> : IValue
    {
        public virtual string Type { get; set; } = Types.Any;

        public readonly T Source;

        public object Boxed => Source;

        public Value(T value)
        {
            Source = value;
        }

        public virtual IExpression Evaluate(Environment env)
        {
            return this;
        }

        public override string ToString()
        {
            return Source.ToString();
        }
    }
}
