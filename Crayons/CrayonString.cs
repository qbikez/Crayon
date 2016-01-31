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
            public CrayonColor Color;
        }

        public static string EscapeChar = ":";
        internal static ConsoleWriter writer = new ConsoleWriter();
        internal static string escapeStart => EscapeChar;
        internal static string escapeEnd => EscapeChar;

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
            //escape escape chars
            //text = text.Replace(escapeStart, $"{escapeStart}{escapeEnd}");
            // make sure to start with a default color, otherwise, regex won't capture text until some color definition
            var defColor = $"{escapeStart}d{escapeEnd}";
            if (!text.StartsWith(defColor)) text = defColor + text;
            if (!text.EndsWith(defColor)) text = text + defColor;
            var pattern = new Regex($"{escapeStart}(?<color>[a-zA-Z]*?){escapeEnd}");
            var matches = pattern.Matches(text);

            var result = new List<CrayonToken>(matches.Count > 0 ? matches.Count-1 : 0);
            var curColor = new CrayonColor("d");
            for (int i = 1; i < matches.Count; i++)
            {
                var m = matches[i];
                var colorName = matches[i - 1].Groups["color"].Value;
                //empty color name means escaped escape char
                var isEscaped = string.IsNullOrEmpty(colorName);
                var color = !isEscaped ? new CrayonColor(colorName) : curColor;
                curColor = color;
                var start = matches[i - 1].Captures[0].Index + matches[i - 1].Captures[0].Length;
                var end = m.Captures[0].Index;
                var tokenText = text.Substring(start, end - start);
                if (isEscaped) tokenText = EscapeChar + tokenText;
                result.Add(new CrayonToken()
                {
                    Color = color,
                    Text = tokenText
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