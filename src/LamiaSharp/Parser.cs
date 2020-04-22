using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using LamiaSharp.Exceptions;
using LamiaSharp.Expressions;
using LamiaSharp.Keywords;

// ReSharper disable once CheckNamespace
namespace LamiaSharp
{
    public class Parser
    {
        private static Dictionary<string, System.Type> _keywords;

        public const string Boc = "(";
        public const string Eoc = ")";
        public const string Comment = ";;";

        public static IList<string> Tokenize(string input)
        {
            var lines = input
                .Replace(Boc, $" {Boc} ")
                .Replace(Eoc, $" {Eoc} ")
                .Split('\n');

            var tokens = lines
                .Select(l =>
                {
                    var i = l.IndexOf(Comment, System.StringComparison.Ordinal);

                    return i > 0 ? l.Substring(0, i) : l;
                })
                .SelectMany(l => l.Split())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            if (tokens.First() != Boc)
            {
                tokens.Insert(0, Boc);
                tokens.Add(Eoc);
            }

            return tokens;
        }

        public static void Boot()
        {
            _keywords = new Dictionary<string, System.Type>();

            foreach (var cls in typeof(InternalKeywords).GetNestedTypes())
            {
                foreach (var keyword in cls.GetNestedTypes())
                {
                    _keywords[keyword.Name.ToLower()] = keyword;

                    foreach (var alias in keyword.GetCustomAttributes<AliasAttribute>())
                    {
                        _keywords[alias.Name.ToLower()] = keyword;
                    }
                }
            }
        }

        public static ExpressionList Evaluatize(IList<string> tokens)
        {
            if (_keywords == null)
            {
                Boot();
            }
            
            Debug.Assert(_keywords != null, nameof(_keywords) + " != null");

            var stack = new Stack<ExpressionList>();

            for (var i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];

                switch (token)
                {
                    case Boc:
                        var op = tokens[i + 1];
                        var node = _keywords.TryGetValue(op, out var keyword)
                                        ? System.Activator.CreateInstance(keyword) as ExpressionList
                                        : new ExpressionList(op);

                        Debug.Assert(node != null, nameof(node) + " != null");

                        node.Elongate();
                        stack.Push(node);
                        break;
                    case Eoc:
                        var list = stack.Pop();
                        list.Elongate();

                        if (stack.Count == 0) return list;

                        stack.Peek().Add(list);
                        break;
                    default:
                        stack.Peek().Add(Expression.From(token));
                        break;
                }
            }

            throw new RuntimeException($"Expect '{Eoc}'");
        }

        public static ExpressionList Parse(string input)
        {
            var tokens = Tokenize(input);

            return Evaluatize(tokens);
        }
    }
}
