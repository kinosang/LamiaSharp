namespace LamiaSharp.Values
{
    public class Char : Value<char>
    {
        public override string Type { get; set; } = Types.Char;

        public Char(char value) : base(value)
        {
        }

        public override string ToString()
        {
            return $"\\{Source}";
        }
    }
}
