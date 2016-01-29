using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crayons
{
    public class CrayonColor
    {
        public string Name => ConsoleColor.ToString().ToLower();

        public CrayonColor()
        {

        }

        public CrayonColor(string color)
        {
            ConsoleColor clr;
            if (color == "d" || color == "default") color = "white";
            if (Enum.TryParse<ConsoleColor>(color, true, out clr))
            {
                this.ConsoleColor = clr;
            }
            else
            {
                throw new Exception($"unrecognized color '{color}'");
            }
        }

        public string Format(string value, bool resetColor = true)
        {
            var colored = $"{CrayonString.escapeStart}{this.Name}{CrayonString.escapeEnd}{value}";
            if (resetColor) colored += $"{CrayonString.escapeStart}default{CrayonString.escapeEnd}";
            return colored;
        }
        public string ANSIColor;
        public ConsoleColor ConsoleColor;
    }
}
