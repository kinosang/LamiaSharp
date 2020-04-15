namespace LamiaSharp.Expressions
{
    public class Symbol : Expression
    {
        protected readonly string Name;

        public Symbol(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
