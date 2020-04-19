using System.Collections.Generic;
using System.Linq;
using LamiaSharp.Expressions;
using LamiaSharp.Values;

// ReSharper disable once CheckNamespace
namespace LamiaSharp.Keywords
{
    internal static partial class InternalKeywords
    {
        public static partial class Bitwise
        {
            [Alias(Token)]
            public class BitAnd : DynamicExpression
            {
                public const string Token = "&";

                public override string Type { get; set; } = Types.Integer;

                public BitAnd() : base(Token)
                {
                }

                public override IExpression Call(Environment env, string op, IEnumerable<IExpression> arguments)
                {
                    var values = arguments.Select(a => a.Evaluate(env)).OfType<Integer>().ToArray();

                    var head = values.First();
                    var tails = values.Skip(1).ToArray();

                    return new Integer(tails.Aggregate((long)head.Boxed, (acc, v) => acc & (long)v.Boxed));
                }
            }
        }
    }
}
