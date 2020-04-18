namespace LamiaSharp.Values
{
    public class Boolean : Value<bool>
    {
        public static Boolean True = new Boolean(true);

        public static Boolean False = new Boolean(false);

        public override string Type { get; set; } = Types.Boolean;

        public Boolean(bool value) : base(value)
        {
        }

        public override string ToString()
        {
            return Source ? "true" : "false";
        }
    }
}
