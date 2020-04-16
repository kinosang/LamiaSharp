namespace LamiaSharp.Values
{
    public class Real : Value<decimal>
    {
        public Real(decimal value) : base(value)
        {
        }

        public override string ToString()
        {
            return $"{Boxed}m";
        }
    }
}
