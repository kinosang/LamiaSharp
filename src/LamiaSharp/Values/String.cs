namespace LamiaSharp.Values
{
    public class String : Value<string>
    {
        public String(string value) : base(value)
        {
        }

        public override string ToString()
        {
            return $"\"{Boxed}\"";
        }
    }
}
