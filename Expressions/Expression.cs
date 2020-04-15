using LamiaSharp.Keywords;
using LamiaSharp.Values;

namespace LamiaSharp.Expressions
{
    public abstract class Expression
    {
        public static Expression From(string token)
        {
            if (token == "true")
                return new Boolean(true);

            if (token == "false")
                return new Boolean(false);

            if (token == "nil")
                return new Nil();

            if (token.StartsWith('"'))
                return new String(token.Trim('"'));

            if (int.TryParse(token, out var n))
                return new Integer(n);

            if (decimal.TryParse(token, out var x))
                return new Real(x);

            return new Symbol(token);
        }
    }
}
