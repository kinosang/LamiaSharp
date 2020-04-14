using System;
using System.Linq;
using LamiaSharp.Expression;

namespace LamiaSharp
{
    public class Parser
    {
        public const string BOC = "(";
        public const string EOC = ")";
        public const string COMMENT = ";;";

        public static string[] Tokenize(string input)
        {
            var lines = input
                .Replace(BOC, $" {BOC} ")
                .Replace(EOC, $" {EOC} ")
                .Split('\n');

            return lines
                .Select(l =>
                {
                    var i = l.IndexOf(COMMENT);

                    return i > 0 ? l.Substring(0, i) : l;
                })
                .SelectMany(l => l.Split())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
        }

        public static ExpList Listize(string[] tokens)
        {
            var list = new ExpList();
            list.Enter();

            for (var i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];

                if (token == BOC)
                {
                    var node = Listize(tokens.Skip(i + 1).ToArray());
                    list.AddLast(node);
                    i += node.Total - 1;
                }
                else if (token == EOC)
                {
                    list.Return();
                    return list;
                }
                else
                {
                    list.AddLast(new Symbol(token));
                }
            }

            throw new Exception($"Expect '{EOC}'");
        }

        public static ExpList Parse(string input)
        {
            var tokens = Tokenize(input);

            if (tokens.First() == BOC)
            {
                return Listize(tokens.Skip(1).ToArray());
            }

            throw new Exception($"Expect '{BOC}'");
        }
    }
}
