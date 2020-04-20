using System.Collections.Generic;

namespace LamiaSharp
{
    public static class Types
    {
        public const string Any = "any";

        public const string Boolean = "boolean";

        public const string Char = "char";

        public const string Double = "double";

        public const string Integer = "integer";

        public const string Nil = "nil";

        public const string Numeric = "numeric";

        public const string Real = "real";

        public const string String = "string";

        public static IEnumerable<string> AnyTypes = new[] { Any };

        public static IEnumerable<string> NumericTypes = new[] { Double, Integer, Real };
    }
}
