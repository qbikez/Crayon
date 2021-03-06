﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crayons.Patterns
{
    public class Pattern
    {
        public class RegexWrapper
        {
            public RegexWrapper(Regex regex, string comment)
            {
                this.Regex = regex;
                this.Comment = comment;
            }

            public Regex Regex { get; set; }
            public string Comment { get; set; }

            public override string ToString()
            {
                return $"{Comment}: {Regex}";
            }
        }
        List<RegexWrapper> patterns = new List<RegexWrapper>();

        public List<RegexWrapper> Patterns => patterns;
        public Pattern()
        {

        }
        public Pattern(params string[] patterns)
        {
            for (int i = 0; i < patterns.Length; i++)
            {
                this.Add(patterns[i]);
            }
        }

        public void Add(string pattern, string name = null)
        {
            var uniquePat = pattern;
            //Regex.Replace(pat, @"\(\?\<([a-z][A-Z]+)\>\)",
            //    $"(?<$1-{Guid.NewGuid().ToString("N").Substring(0, 3)}>)");
            this.Add(new Regex(uniquePat), name);

        }

        public void Add(Regex regex, string name = null)
        {
            this.patterns.Add(new RegexWrapper(regex, name));
        }

        public CrayonString Colorize(string str)
        {
            var colorized = new CrayonString(str);
            foreach (var pattern in patterns)
            {
                colorized = Colorize(colorized, pattern.Regex);
            }

            return colorized;
        }

        public CrayonString Colorize(CrayonString str, Regex regex)
        {
            return ColorizeTokens(str, regex);
        }
        private CrayonString ColorizeTokens(CrayonString str, Regex regex)
        {
            var tokens = str.Tokens;
            var colorized = new List<CrayonString.CrayonToken>();
            foreach (var token in tokens)
            {
                var ct = ColorizeToken(token, regex);
                colorized.AddRange(ct);
            }
            if (colorized.Last().Color != CrayonColor.Default)
            {
                colorized.Add(new CrayonString.CrayonToken(""));
            }
            return new CrayonString(colorized);
        }
        
        private List<CrayonString.CrayonToken> ColorizeToken(CrayonString.CrayonToken token, Regex regex) {
            
            if (token.Color != CrayonColor.Default) {
                // do not process colorized tokens
                return new List<CrayonString.CrayonToken>() { token };
            }
            
            var result = new StringBuilder();            
            var text = token.Text;
            var matches = regex.Matches(text);
            var idx = 0;
            
            if (matches.Count > 0)
            {
                var names = regex.GetGroupNames();
                foreach (Match m in matches)
                {
                    var replaced = m.Captures[0].Value;
                    foreach (var name in names)
                    {
                        var i = 0;
                        /// ignore groups without names
                        if (int.TryParse(name, out i)) continue;
                        var group = m.Groups[name];
                        var color = GetColor(name);
                        var coloredString = color.Format(group.Value);
                        replaced = regex.ReplaceGroup(replaced, name, coloredString);
                    }
                    var before = text.Substring(idx, m.Index - idx);
                    result.Append(before);
                    result.Append(replaced);
                    idx = m.Index + m.Length;
                }
            }
            result.Append(text.Substring(idx, text.Length - idx));
            return new CrayonString(result.ToString()).Tokenize();
        }
        private CrayonString ColorizeString(CrayonString str, Regex regex)
        {
            var result = new StringBuilder();
            var matches = regex.Matches(str.Text);
            var idx = 0;
            if (matches.Count > 0)
            {
                var names = regex.GetGroupNames();
                foreach (Match m in matches)
                {
                    var replaced = m.Captures[0].Value;
                    foreach (var name in names)
                    {
                        var i = 0;
                        /// ignore groups without names
                        if (int.TryParse(name, out i)) continue;
                        var group = m.Groups[name];
                        var color = GetColor(name);
                        var coloredString = color.Format(group.Value);
                        replaced = regex.ReplaceGroup(replaced, name, coloredString);
                    }
                    var before = str.Text.Substring(idx, m.Index - idx);
                    result.Append(before);
                    result.Append(replaced);
                    idx = m.Index + m.Length;
                }
            }
            result.Append(str.Text.Substring(idx, str.Text.Length - idx));
            return new CrayonString(result.ToString());
        }

        private CrayonColor GetColor(string name)
        {
            var dash = name.IndexOf("-");
            if (dash >= 0) name = name.Substring(0, dash);
            return new CrayonColor(name);
        }

        public override string ToString()
        {
            return String.Join("\r\n", patterns);
        }
    }

    public static class RegexExtensions
    {
        public static string ReplaceGroup(
            this Regex regex, string input, string groupName, string replacement)
        {
            return regex.Replace(
                input,
                m =>
                {
                    var group = m.Groups[groupName];
                    var sb = new StringBuilder();
                    var previousCaptureEnd = 0;
                    foreach (var capture in group.Captures.Cast<Capture>())
                    {
                        var currentCaptureEnd =
                            capture.Index + capture.Length - m.Index;
                        var currentCaptureLength =
                            capture.Index - m.Index - previousCaptureEnd;
                        sb.Append(
                            m.Value.Substring(
                                previousCaptureEnd, currentCaptureLength));
                        sb.Append(replacement);
                        previousCaptureEnd = currentCaptureEnd;
                    }
                    sb.Append(m.Value.Substring(previousCaptureEnd));

                    return sb.ToString();
                });
        }
    }
}
