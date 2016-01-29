using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crayons
{
    public class CrayonColor
    {

        public CrayonColor()
        {

        }

        public CrayonColor(string color)
        {
            ConsoleColor clr;
            if (Enum.TryParse<ConsoleColor>(color, true, out clr))
            {
                this.ConsoleColor = clr;
            }
            else
            {
                throw new Exception($"unrecognized color '{color}'");
            }
        }

        public string ANSIColor;
        public ConsoleColor ConsoleColor;
    }
}
