using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crayons.Patterns
{
    public class Pattern
    {
        string[] patterns = null;
        public Pattern(params string[] patterns)
        {
            this.patterns = new string[patterns.Length];

            for (int i = 0; i < patterns.Length; i++)
            {
                var pat = patterns[i];
                this.patterns[i] = pat;
            }
        }

        public CrayonString Colorize(string str)
        {
            var colorized = new CrayonString(str);
            foreach (var pattern in patterns)
            {
                colorized = Colorize(colorized, pattern);
            }

            return colorized;
        }

        public CrayonString Colorize(CrayonString str, string pattern)
        {
            var result = str.Text;
            var regex = new Regex(pattern);
            var matches = regex.Matches(str.Text);
            if (matches.Count > 0)
            {
                var names = regex.GetGroupNames();
                foreach(Match m in matches)
                {
                    foreach(var name in names)
                    {
                        var i = 0;
                        /// ignore groups without names
                        if (int.TryParse(name, out i)) continue;
                        var group = m.Groups[name];
                        var color = GetColor(name);
                        var coloredString = color.Format(group.Value);
                        result = regex.ReplaceGroup(result, name, coloredString);
                    }
                }
            }

            return new CrayonString(result);
        }

        private Crayon GetColor(string name)
        {
            var dash = name.IndexOf("-");
            if (dash >= 0) name = name.Substring(0, dash);
            return new Crayon(name);
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
