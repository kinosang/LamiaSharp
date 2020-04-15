using System;
using LamiaSharp.Expressions;

namespace LamiaSharp.Values
{
    public abstract class Number : Value
    {
        protected Number(IComparable value) : base(value)
        {
        }
    }
}
