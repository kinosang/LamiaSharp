using LamiaSharp.Values;

namespace LamiaSharp.Expressions
{
    public abstract class Expression
    {
        public static Expression From(string token) => token switch
        {
            "nil" => new Nil(),
            "true" => new Boolean(true),
            "false" => new Boolean(false),
            _ when token.StartsWith('"') => new String(token.Trim('"')),
            _ when int.TryParse(token, out var n) => new Integer(n),
            _ when decimal.TryParse(token, out var x) => new Real(x),
            _ => new Symbol(token)
        };
    }
}
