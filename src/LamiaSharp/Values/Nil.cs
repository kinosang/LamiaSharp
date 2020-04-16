namespace LamiaSharp.Values
{
    public class Nil : Value<object>
    {
        public Nil() : base(null)
        {
        }

        public override string ToString()
        {
            return "nil";
        }
    }
}
