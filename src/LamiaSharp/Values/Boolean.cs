using LamiaSharp.Expressions;

namespace LamiaSharp.Values
{
    public class Boolean : Value
    {
        public Boolean(bool value) : base(value)
        {
        }

        public override string ToString()
        {
            return (bool)Boxed ? "true" : "false";
        }
    }
}
