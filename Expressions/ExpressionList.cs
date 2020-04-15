using System.Collections;
using System.Collections.Generic;

namespace LamiaSharp.Expressions
{
    public class ExpressionList : Expression, IEnumerable<Expression>
    {
        private IList<Expression> values = new List<Expression>();

        public ExpressionListNode First { get; set; }

        public ExpressionListNode Last { get; set; }

        public int Count { get; private set; } = 0;

        public int Total { get; private set; } = 0;

        public ExpressionList AddFirst(ExpressionListNode node)
        {
            if (First == null)
            {
                First = node;
                Last = node;
            }
            else
            {
                First.Previous = node;
                node.Next = First;

                First = node;
            }

            values.Insert(0, node.Value);

            Count++;

            if (node.Value is ExpressionList sub)
            {

                Total += sub.Total;
            }
            else
            {
                Total++;
            }

            return this;
        }

        public ExpressionList AddFirst(Expression value)
        {
            return AddFirst(new ExpressionListNode(value));
        }

        public ExpressionList AddLast(ExpressionListNode node)
        {
            if (Last == null)
            {
                Last = node;
                First = node;
            }
            else
            {
                Last.Next = node;
                node.Previous = Last;

                Last = node;
            }

            values.Add(node.Value);

            Count++;

            if (node.Value is ExpressionList sub)
            {

                Total += sub.Total;
            }
            else
            {
                Total++;
            }

            return this;
        }

        public ExpressionList AddLast(Expression value)
        {
            return AddLast(new ExpressionListNode(value));
        }

        public ExpressionList Enter()
        {
            Total++;

            return this;
        }

        public ExpressionList Return()
        {
            Total++;

            return this;
        }

        public IEnumerator<Expression> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }

        public override string ToString()
        {
            var buf = Parser.BOC;

            foreach (var value in values)
            {
                buf += value.ToString();
                buf += " ";
            }

            buf = buf.TrimEnd();

            buf += Parser.EOC;

            return buf;
        }
    }
}
