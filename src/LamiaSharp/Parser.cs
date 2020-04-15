using System;
using System.Collections.Generic;
using System.Linq;
using LamiaSharp.Expressions;

namespace LamiaSharp
{
    public class Parser
    {
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
                    var i = l.IndexOf(Comment, StringComparison.Ordinal);

                    return i > 0 ? l.Substring(0, i) : l;
                })
                .SelectMany(l => l.Split())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
        }

        public static ExpressionList Listize(string[] tokens)
        {
            var list = new ExpressionList();
            list.Enter();

            for (var i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];

                switch (token)
                {
                    case Boc:
                        var node = Listize(tokens.Skip(i + 1).ToArray());
                        list.AddLast(node);
                        i += node.Total - 1;
                        break;
                    case Eoc:
                        list.Return();
                        return list;
                    default:
                        list.AddLast(Expression.From(token));
                        break;
                }
            }

            throw new Exception($"Expect '{Eoc}'");
        }

        public static ExpressionList[] Evaluatize(string[] tokens)
        {
            var lines = new List<ExpressionList>();

            for (var i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] != Boc) continue;

                var line = Listize(tokens.Skip(i + 1).ToArray());
                lines.Add(line);
                i += line.Total - 1;
            }

            if (lines.Count > 0)
            {
                return lines.ToArray();
            }

            throw new Exception($"Expect '{Boc}'");
        }

        public static ExpressionList[] Parse(string input)
        {
            var tokens = Tokenize(input);

            return Evaluatize(tokens);
        }
    }
}
