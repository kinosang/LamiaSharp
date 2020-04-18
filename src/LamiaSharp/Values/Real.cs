namespace LamiaSharp.Values
{
    public class Real : Value<decimal>
    {
        public override string Type => Types.Real;

        public Real(decimal value) : base(value)
        {
        }

        public override string ToString()
        {
            return $"{Source}m";
        }
    }
}
