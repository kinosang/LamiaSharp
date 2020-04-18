using System.Collections.Generic;
using LamiaSharp.Expressions;

namespace LamiaSharp
{
    public class Environment : Dictionary<string, IExpression>
    {
        public Environment()
        {
        }

        public Environment(Environment env) : base(env)
        {
        }
    }
}
