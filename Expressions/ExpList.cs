using System.Collections;
using System.Collections.Generic;

namespace LamiaSharp.Expression
{
    public class ExpList : IExpression, IEnumerable<IExpression>
    {
        private IList<IExpression> values = new List<IExpression>();

        public ExpListNode First { get; set; }
        public ExpListNode Last { get; set; }

        public int Count { get; private set; } = 0;

        public int Total { get; private set; } = 0;

        public ExpList AddFirst(ExpListNode node)
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

            if (node.Value is ExpList sub)
            {

                Total += sub.Total;
            }
            else
            {
                Total++;
            }

            return this;
        }

        public ExpList AddFirst(IExpression value)
        {
            return AddFirst(new ExpListNode(value));
        }

        public ExpList AddLast(ExpListNode node)
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

            if (node.Value is ExpList sub)
            {

                Total += sub.Total;
            }
            else
            {
                Total++;
            }

            return this;
        }

        public ExpList AddLast(IExpression value)
        {
            return AddLast(new ExpListNode(value));
        }

        public ExpList Enter()
        {
            Total++;

            return this;
        }

        public ExpList Return()
        {
            Total++;

            return this;
        }

        public IEnumerator<IExpression> GetEnumerator()
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
