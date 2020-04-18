namespace LamiaSharp.Values
{
    public class Nil : Value<object>
    {
        public static Nil Default = new Nil();

        public override string Type { get; set; } = Types.Nil;

        public Nil() : base(null)
        {
        }

        public override string ToString()
        {
            return "nil";
        }
    }
}
