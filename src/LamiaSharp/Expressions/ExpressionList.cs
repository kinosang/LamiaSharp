using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LamiaSharp.Exceptions;
using LamiaSharp.Values;

namespace LamiaSharp.Expressions
{
    public class ExpressionList : Expression, IEnumerable<IExpression>
    {
        private readonly IList<IExpression> _values = new List<IExpression>();

        public readonly string Op;

        // TODO: Update Type to actual
        public override string Type { get; set; } = Types.Any;

        public ExpressionListNode First { get; set; }

        public ExpressionListNode Last { get; set; }

        public int Count { get; private set; }

        public int Tokens { get; private set; }

        public ExpressionList()
        {
        }

        public ExpressionList(string op)
        {
            Op = op;
        }

        public ExpressionList AddFirst(ExpressionListNode node)
        {
            if (First == null)
            {
                First = node;
                Last = First;
            }
            else
            {
                First.Previous = node;
                node.Next = First;

                First = node;
            }

            UpdateEnumerable(node, true);

            return this;
        }

        public ExpressionList AddFirst(IExpression value)
        {
            return AddFirst(new ExpressionListNode(value));
        }

        public ExpressionList AddLast(ExpressionListNode node)
        {
            if (Last == null)
            {
                First = node;
                Last = First;
            }
            else
            {
                Last.Next = node;
                node.Previous = Last;

                Last = node;
            }

            UpdateEnumerable(node);

            return this;
        }

        public ExpressionList AddLast(IExpression value)
        {
            return AddLast(new ExpressionListNode(value));
        }

        public ExpressionList Elongate()
        {
            Tokens++;

            return this;
        }

        public IEnumerator<IExpression> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override IExpression Evaluate(Environment env)
        {
            if (string.IsNullOrEmpty(Op))
            {
                IExpression result = Nil.Default;

                foreach (var action in _values)
                {
                    result = action.Evaluate(env);
                }

                return result;
            }

            if (!env.TryGetValue(Op, out var symbol))
            {
                throw new RuntimeException($"Call to undefined symbol '{Op}'");
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

            var arguments = _values.Skip(1).Select(p => p.Evaluate(env)).OfType<IValue>().ToArray();
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

            foreach (var value in _values)
            {
                buf += value.ToString();
                buf += " ";
            }

            buf = buf.TrimEnd();

            buf += Parser.Eoc;

            return buf;
        }

        private void UpdateEnumerable(ExpressionListNode node, bool prepend = false)
        {
            if (prepend)
            {
                _values.Insert(0, node.Value);
            }
            else
            {
                _values.Add(node.Value);
            }

            Count++;

            if (node.Value is ExpressionList sub)
            {
                Tokens += sub.Tokens;
            }
            else
            {
                Tokens++;
            }
        }
    }
}
