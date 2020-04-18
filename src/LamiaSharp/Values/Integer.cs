namespace LamiaSharp.Values
{
    public class Integer : Value<long>
    {
        public override string Type => Types.Integer;

        public Integer(long value) : base(value)
        {
        }
    }
}
