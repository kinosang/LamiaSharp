using System.Collections;
using System.Collections.Generic;

namespace LamiaSharp.Expressions
{
    public class ExpressionList : Expression, IEnumerable<IExpression>
    {
        private readonly IList<IExpression> _values = new List<IExpression>();

        public readonly string Op;

        public ExpressionListNode First { get; set; }

        public ExpressionListNode Last { get; set; }

        public int Count { get; private set; }

        public int Tokens { get; private set; } = 1;

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

            _values.Insert(0, node.Value);

            Count++;

            if (node.Value is ExpressionList sub)
            {
                Tokens += sub.Tokens;
            }
            else
            {
                Tokens++;
            }

            return this;
        }

        public ExpressionList AddFirst(IExpression value)
        {
            return AddFirst(new ExpressionListNode(value));
        }

        public ExpressionList AddLast(ExpressionListNode node)
        {
            if (First == null)
            {
                AddFirst(node);

                return this;
            }

            Last.Next = node;
            node.Previous = Last;

            Last = node;

            _values.Add(node.Value);

            Count++;

            if (node.Value is ExpressionList sub)
            {
                Tokens += sub.Tokens;
            }
            else
            {
                Tokens++;
            }

            return this;
        }

        public ExpressionList AddLast(IExpression value)
        {
            return AddLast(new ExpressionListNode(value));
        }

        public ExpressionList Enter()
        {
            Tokens++;

            return this;
        }

        public ExpressionList Return()
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

        public override string ToString()
        {
            var buf = Parser.Boc;

            buf += Op;

            foreach (var value in _values)
            {
                buf += " ";
                buf += value.ToString();
            }

            buf = buf.TrimEnd();

            buf += Parser.Eoc;

            return buf;
        }
    }
}
