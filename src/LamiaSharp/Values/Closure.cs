using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LamiaSharp.Expressions;

namespace LamiaSharp.Values
{
    public class Closure : Value<object>, IEnumerable<ExpressionList>
    {
        // TODO: Update Type to actual
        public override string Type => "any -> any";

        public Environment Environment;

        public readonly IEnumerable<Symbol> Parameters;

        public readonly IEnumerable<ExpressionList> Body;

        public Closure(IEnumerable<Symbol> parameters, IEnumerable<ExpressionList> body) : base(null)
        {
            Parameters = parameters;
            Body = body;
        }

        public IEnumerator<ExpressionList> GetEnumerator()
        {
            return Body.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IExpression Run(IEnumerable<IValue> runtime)
        {
            Environment = new Environment();

            return Call(runtime);
        }

        public IExpression Call(IEnumerable<IValue> runtime)
        {
            var env = new Environment(Environment);

            foreach (var (p, arg) in Parameters.Zip(runtime, (p, arg) => (p, arg)))
            {
                env[p.ToString()] = arg;
            }

            env["self"] = this;

            IExpression result = Nil.Default;
            foreach (var expression in Body)
            {
                result = expression.Evaluate(env);
            }
            return result;
        }

        public override IExpression Evaluate(Environment env)
        {
            Environment = env;

            return this;
        }

        public override string ToString()
        {
            return "closure";
        }
    }
}
