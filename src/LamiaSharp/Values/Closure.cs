using System.Collections;
using System.Collections.Generic;
using LamiaSharp.Expressions;

namespace LamiaSharp.Values
{
    public class Closure : Value<object>, IEnumerable<ExpressionList>
    {
        public IEnumerable<Symbol> Parameters { get; }
        public IEnumerable<ExpressionList> Body { get; }

        public Closure(IEnumerable<Symbol> parameters, IEnumerable<ExpressionList> body) : base(null)
        {
            Parameters = parameters;
            Body = body;
        }

        public IEnumerator<ExpressionList> GetEnumerator()
        {
            return Body.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
