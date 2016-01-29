using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System;

namespace Crayons
{    
    class CrayonString
    {
        class CrayonToken
        {
            public string Text;
            public CrayonColor Color;
        }

        private const string escapeChar = ":";
        private const string escapeStart = escapeChar;
        private const string escapeEnd = escapeChar;

        private string text;

        public CrayonString(string text)
        {
            this.text = text;
        }

        private static List<CrayonToken> Parse(string text)
        {
            var pattern = $"{escapeStart}(?<color>.*?){escapeEnd}(?<text>[^{escapeStart}]*)";
            var matches = Regex.Matches(text, pattern);

            var result = new List<CrayonToken>();

            foreach(Match m in matches)
            {
                result.Add(new CrayonToken()
                {
                    Color = new CrayonColor(m.Groups["color"].Value),
                    Text = m.Groups["text"].Value
                });
            }


            return result;
        }
        public void WriteToConsole()
        {
            WriteString(this);
        }

        
        private static void WriteString(CrayonString str)
        {
            var tokens = Parse(str.text);
            var lastColor = Console.ForegroundColor;

            try
            {
                if (tokens.Count == 0)
                {
                    /// text is not colored
                    Console.WriteLine(str.text);
                    return;
                }
                foreach (var token in tokens)
                {
                    WriteToken(token);
                }
                Console.WriteLine();
            }
            finally
            {
                Console.ForegroundColor = lastColor;
            }
        }

        private static void WriteToken(CrayonToken token)
        {
            Console.ForegroundColor = token.Color.ConsoleColor;
            Console.Write(token.Text);
        }
    }
}