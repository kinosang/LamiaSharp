namespace LamiaSharp.Values
{
    public class Double : Value<double>
    {
        public override string Type => Types.Double;

        public Double(double value) : base(value)
        {
        }
    }
}
