
namespace LamiaSharp.Expression
{
    public class Symbol : IExpression
    {
        public readonly string Name;

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
