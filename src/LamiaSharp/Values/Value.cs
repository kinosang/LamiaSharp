namespace LamiaSharp.Values
{
    public class Value<T> : IValue
    {
        public readonly T Boxed;

        public Value(T value)
        {
            Boxed = value;
        }

        public override string ToString()
        {
            return Boxed.ToString();
        }
    }
}
