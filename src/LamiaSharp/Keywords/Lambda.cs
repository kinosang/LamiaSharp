using System.Collections.Generic;
using System.Linq;
using LamiaSharp.Exceptions;
using LamiaSharp.Expressions;
using LamiaSharp.Values;

namespace LamiaSharp.Keywords
{
    public class Lambda : DynamicExpression
    {
        public const string Token = "λ";

        // TODO: Update Type to actual
        public override string Type { get; set; } = "any -> any";

        public Lambda() : base(Token)
        {
        }

        public override IExpression Call(Environment env, string op, IEnumerable<IExpression> arguments)
        {
            var expressions = arguments.ToArray();

            if (expressions.Length < 2)
            {
                throw new RuntimeException("Expect lambda definition");
            }

            if (!(expressions[0] is ExpressionList list))
            {
                throw new RuntimeException("Expect parameters definition");
            }

            if (list.Any(p => !(p is Symbol)))
            {
                throw new RuntimeException("Expect parameters definition");
            }

            var body = expressions.Skip(1).ToArray();
            if (body.Any(e => !(e is ExpressionList)))
            {
                throw new RuntimeException("Expect parameters definition");
            }

            return new Closure(list.OfType<Symbol>(), body.OfType<ExpressionList>());
        }
    }
}
