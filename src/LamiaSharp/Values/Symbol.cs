using LamiaSharp.Expressions;

namespace LamiaSharp.Values
{
    public class Symbol : Value<string>
    {
        // TODO: Update Type to actual
        public override string Type { get; set; } = Types.Any;

        public Symbol(string name) : base(name)
        {
        }

        public override IExpression Evaluate(Environment env)
        {
            return env[Source].Evaluate(env);
        }
    }
}
