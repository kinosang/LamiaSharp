namespace LamiaSharp.Values
{
    public class Boolean : Value<bool>
    {
        public Boolean(bool value) : base(value)
        {
        }

        public override string ToString()
        {
            return Boxed ? "true" : "false";
        }
    }
}
