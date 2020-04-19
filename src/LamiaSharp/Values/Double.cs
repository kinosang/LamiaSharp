namespace LamiaSharp.Values
{
    public class Double : Numeric<double>
    {
        public override string Type { get; set; } = Types.Double;

        public Double(double value) : base(value)
        {
        }
    }
}
