using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System;

namespace Crayons
{    
    public class CrayonString
    {
        internal class CrayonToken
        {
            public string Text;
            public Crayon Color;
        }

        private const string escapeChar = ":";
        private ConsoleWriter writer = new ConsoleWriter;
        internal const string escapeStart = escapeChar;
        internal const string escapeEnd = escapeChar;

        private string text;
        public string Text => text;

        public CrayonString(string text)
        {
            this.text = text;
        }
        
        internal List<CrayonToken> Tokenize() {
            return Parse(this.text);
        }

        private static List<CrayonToken> Parse(string text)
        {
            // make sure to start with a default color, otherwise, regex won't capture text until some color definition
            text = ":d:" + text;
            var pattern = $"{escapeStart}(?<color>.*?){escapeEnd}(?<text>[^{escapeStart}]*)";
            var matches = Regex.Matches(text, pattern);

            var result = new List<CrayonToken>();

            foreach(Match m in matches)
            {
                result.Add(new CrayonToken()
                {
                    Color = new Crayon(m.Groups["color"].Value),
                    Text = m.Groups["text"].Value
                });
            }


            return result;
        }
        public void WriteToConsole()
        {
            writer.WriteString(this);
        }

        
        
    }
}