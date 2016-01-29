using System;

namespace Crayons {
    public class Crayon {
     
        public Crayon()
        {

        }

        public Crayon(string color)
        {
            ConsoleColor clr;
            if (color == "d" || color == "default") color = "white";
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

        public string Name => ConsoleColor.ToString().ToLower();

        
        public static void Write(string str)
        {
            new CrayonString(str).WriteToConsole();
        }
        
        public static void Write(CrayonString str) {
            str.WriteToConsole();
        }

        public string Format(string value, bool resetColor = true)
        {
            var colored =  $"{CrayonString.escapeStart}{this.Name}{CrayonString.escapeEnd}{value}";
            if (resetColor) colored += $"{CrayonString.escapeStart}default{CrayonString.escapeEnd}";
            return colored;
        }
        
        public static void Configure(Action<string, ConsoleColor> write, Action<string> writeline) {
            CrayonString.writer = new CustomConsoleWriter(write, writeline);
        }
    }
}