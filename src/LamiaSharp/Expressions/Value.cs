namespace LamiaSharp.Expressions
{
    public class Value : Expression
    {
        protected object Boxed { get; }

        public Value(object value)
        {
            Boxed = value;
        }

        public override string ToString()
        {
            return Boxed.ToString();
        }
    }
}
