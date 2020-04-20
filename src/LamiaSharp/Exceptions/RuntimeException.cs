using System;

namespace LamiaSharp.Exceptions
{
    internal class RuntimeException : Exception
    {
        public RuntimeException(string message) : base(message) { }
    }
}
