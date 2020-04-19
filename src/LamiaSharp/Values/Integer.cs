namespace LamiaSharp.Values
{
    public class Integer : Numeric<long>
    {
        public override string Type { get; set; } = Types.Integer;

        public Integer(long value) : base(value)
        {
        }
    }
}
