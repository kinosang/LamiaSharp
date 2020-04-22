using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LamiaSharp.Exceptions;
using LamiaSharp.Values;

namespace LamiaSharp.Expressions
{
    public class ExpressionList : Expression, IEnumerable<IExpression>
    {
        protected readonly IList<IExpression> Values = new List<IExpression>();

        public readonly string Op;

        // TODO: Update Type to actual
        public override string Type { get; set; } = Types.Any;

        public int Count { get; private set; }

        public int Tokens { get; private set; }

        public ExpressionList(string op)
        {
            Op = op;
        }

        public ExpressionList Add(IExpression node)
        {
            Values.Add(node);

            Count++;

            if (node is ExpressionList sub)
            {
                Tokens += sub.Tokens;
            }
            else
            {
                Tokens++;
            }

            return this;
        }

        public ExpressionList Elongate()
        {
            Tokens++;

            return this;
        }

        public IEnumerator<IExpression> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override IExpression Evaluate(Environment env)
        {
            if (!env.TryGetValue(Op, out var symbol))
            {
                IExpression result = Nil.Default;

                foreach (var action in Values)
                {
                    result = action.Evaluate(env);
                }

                return result;
            }

            var expression = symbol.Evaluate(env);

            if (!(expression is IValue value))
            {
                throw new RuntimeException($"Except value, got '{expression}'");
            }

            if (!(value is Closure closure))
            {
                return value;
            }

            var arguments = Values.Skip(1).Select(p => p.Evaluate(env)).OfType<IValue>().ToArray();
            var count = arguments.Length;

            if (count == 0)
            {
                return closure;
            }

            var parameters = closure.Parameters.Count();
            if (count != parameters)
            {
                throw new RuntimeException($"Except {parameters} arguments, only {count} provided");
            }

            return closure.Call(arguments);
        }

        public override string ToString()
        {
            var buf = Parser.Boc;

            foreach (var value in Values)
            {
                buf += value.ToString();
                buf += " ";
            }

            buf = buf.TrimEnd();

            buf += Parser.Eoc;

            return buf;
        }
    }
}
