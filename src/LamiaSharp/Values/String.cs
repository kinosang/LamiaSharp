namespace LamiaSharp.Values
{
    public class String : Value<string>
    {
        public override string Type => Types.String;

        public String(string value) : base(value)
        {
        }

        public override string ToString()
        {
            return $"\"{Source}\"";
        }
    }
}
