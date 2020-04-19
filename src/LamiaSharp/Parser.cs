using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LamiaSharp.Exceptions;
using LamiaSharp.Expressions;
using LamiaSharp.Keywords;
using LamiaSharp.Values;

// ReSharper disable once CheckNamespace
namespace LamiaSharp
{
    public class Parser
    {
        private static Dictionary<string, System.Type> _keywords;

        public const string Boc = "(";
        public const string Eoc = ")";
        public const string Comment = ";;";

        public static string[] Tokenize(string input)
        {
            var lines = input
                .Replace(Boc, $" {Boc} ")
                .Replace(Eoc, $" {Eoc} ")
                .Split('\n');

            return lines
                .Select(l =>
                {
                    var i = l.IndexOf(Comment, System.StringComparison.Ordinal);

                    return i > 0 ? l.Substring(0, i) : l;
                })
                .SelectMany(l => l.Split())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
        }

        public static ExpressionList Listize(string[] tokens)
        {
            if (_keywords == null)
            {
                Boot();
            }

            ExpressionList list;
            var op = tokens[0];

            // ReSharper disable once PossibleNullReferenceException
            if (_keywords.TryGetValue(op, out var keyword))
            {
                list = System.Activator.CreateInstance(keyword) as ExpressionList;
            }
            else
            {
                list = new ExpressionList(tokens[0]);
            }

            // ReSharper disable once PossibleNullReferenceException
            list.Elongate();

            for (var i = 1; i < tokens.Length; i++)
            {
                var token = tokens[i];

                switch (token)
                {
                    case Boc:
                        var node = Listize(tokens.Skip(i + 1).ToArray());
                        list.AddLast(node);
                        i += node.Tokens - 1;
                        break;
                    case Eoc:
                        list.Elongate();
                        return list;
                    default:
                        list.AddLast(Expression.From(token));
                        break;
                }
            }

            throw new RuntimeException($"Expect '{Eoc}'");
        }

        public static Closure Evaluatize(string[] tokens)
        {
            var lines = new List<ExpressionList>();

            for (var i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] != Boc) continue;

                var line = Listize(tokens.Skip(i + 1).ToArray());
                lines.Add(line);
                i += line.Tokens - 1;
            }

            if (lines.Count > 0)
            {
                return new Closure(new Symbol[0], lines);
            }

            throw new RuntimeException($"Expect '{Boc}'");
        }

        public static Closure Parse(string input)
        {
            var tokens = Tokenize(input);

            return Evaluatize(tokens);
        }

        private static void Boot()
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
    }
}
