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
            _ when long.TryParse(token, out var l) => new Integer(l),
            _ when double.TryParse(token, out var d) => new Double(d),
            _ when token.EndsWith('m') && decimal.TryParse(token.Remove(token.Length - 1), out var m) => new Real(m),
            _ => new Symbol(token)
        };
    }
}
