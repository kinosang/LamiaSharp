using LamiaSharp.Expressions;

namespace LamiaSharp.Values
{
    public class Nil : Value
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
