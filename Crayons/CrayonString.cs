using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using System.Text;

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
        internal static IConsoleWriter defaultWriter = new ConsoleWriter();
        internal static string escapeStart => EscapeChar;
        internal static string escapeEnd => EscapeChar;

        private string text;
        public string Text => text;
        private List<CrayonToken> tokens;
        private string text1;

        private List<CrayonToken> Tokens
        {
            get
            {
                if (tokens == null)
                {
                    tokens = this.Tokenize();
                }
                return tokens;
            }
        }

        //TODO: use non-static escape char
        public CrayonString() : this("")
        { }

        public CrayonString(string text)
        {
            this.text = text;
        }

        internal CrayonString(List<CrayonToken> tokens)
        {
            this.tokens = tokens;
            this.text = Join(tokens);
        }
        internal CrayonString(CrayonToken token) : this(new List<CrayonToken>() { token })
        {
        }
        public CrayonString(CrayonColor color, string text) : this(new CrayonToken()
        {
            Color = color,
            Text = text
        })
        {
        }

        internal List<CrayonToken> Tokenize()
        {
            return Parse(this.text);
        }

        internal static string Join(List<CrayonToken> tokens)
        {
            var sb = new StringBuilder();
            foreach (var token in tokens)
            {
                sb.Append(escapeStart);
                sb.Append(token.Color.OriginalName.ToLower());
                sb.Append(escapeEnd);
                sb.Append(token.Text);
            }
            return sb.ToString();
        }

        internal static List<CrayonToken> Parse(string text)
        {
            //escape escape chars
            //text = text.Replace(escapeStart, $"{escapeStart}{escapeEnd}");
            // make sure to start with a default color, otherwise, regex won't capture text until some color definition
            var defColor = $"{escapeStart}d{escapeEnd}";
            if (!text.StartsWith(defColor)) text = defColor + text;
            if (!text.EndsWith(defColor)) text = text + defColor;
            var pattern = new Regex($"{escapeStart}(?<color>[a-zA-Z]*?){escapeEnd}");
            var matches = pattern.Matches(text);

            var result = new List<CrayonToken>(matches.Count > 0 ? matches.Count - 1 : 0);
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
            WriteToConsole(defaultWriter);
        }

        public void WriteToConsole(IConsoleWriter writer)
        {
            writer.WriteString(this);
        }
        
      

        public override string ToString()
        {
            return this.Text;
        }

        public override bool Equals (object obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            // TODO: write your implementation of Equals() here
            var other = obj as CrayonString;
            
            return other.Text == this.Text;
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            throw new System.NotImplementedException();
            return base.GetHashCode();
        }

        public static implicit operator CrayonString(string str)
        {
            return new CrayonString(str);
        }
        public static implicit operator string (CrayonString str)
        {
            return str.ToString();
        }

        public static CrayonString operator +(CrayonString s1, CrayonString s2)
        {
            CrayonString s3 = new CrayonString(s1.Tokens);
            s3.Tokens.AddRange(s2.Tokens);
            s3.text = Join(s3.Tokens);

            return s3;
        }
    }
}