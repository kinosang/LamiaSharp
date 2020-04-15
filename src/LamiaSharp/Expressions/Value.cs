namespace LamiaSharp.Expressions
{
    public class Value : Expression
    {
        protected object Boxed { get; private set; }

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
