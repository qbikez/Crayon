using System;

namespace Crayons {
    public class Crayon {
     
        public Crayon()
        {

        }

        public Crayon(string color)
        {
            ConsoleColor clr;
            if (Enum.TryParse<ConsoleColor>(color, true, out clr))
            {
                this.ConsoleColor = clr;
            } else
            {
                throw new Exception($"unrecognized color '{color}'");
            }
        }

        public string ANSIColor;
        public ConsoleColor ConsoleColor;

        public static void Write(string str)
        {
            new CrayonString(str).WriteToConsole();
        }
    }
}