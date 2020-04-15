namespace LamiaSharp.Values
{
    public class Real : Number
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
