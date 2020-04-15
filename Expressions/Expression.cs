namespace LamiaSharp.Expressions
{
    public abstract class Expression
    {
        public static Expression From(string token) => token switch
        {
            _ => new Symbol(token)
        };
    }
}
