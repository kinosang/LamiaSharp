using System;

namespace LamiaSharp.Keywords
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class AliasAttribute : Attribute
    {
        public string Name { get; }

        public AliasAttribute(string name)
        {
            Name = name;
        }
    }
}
