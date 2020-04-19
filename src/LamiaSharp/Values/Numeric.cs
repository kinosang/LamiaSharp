using System;

namespace LamiaSharp.Values
{
    public class Numeric<T> : Value<T>, INumeric, IComparable<Numeric<T>>, IEquatable<Numeric<T>> where T : IComparable
    {
        public override string Type { get; set; } = Types.Numeric;

        public Numeric(T value) : base(value)
        {
        }

        public int CompareTo(Numeric<T> other)
        {
            return Source.CompareTo(other.Source);
        }

        public bool Equals(Numeric<T> other)
        {
            return other != null && Source.Equals(other.Source);
        }
    }
}
