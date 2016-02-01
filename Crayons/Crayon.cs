using System;

namespace Crayons
{
    public static class Crayon
    {

        public static void Write(string str)
        {
            new CrayonString(str).WriteToConsole();
        }

        public static void Write(CrayonString str)
        {
            str.WriteToConsole();
        }



        public static void Configure(Action<string, ConsoleColor> write, Action<string> writeline)
        {
            CrayonString.writer = new CustomConsoleWriter(write, writeline);
        }

        public static CrayonString Red(string text)
        {
            return CrayonStringExtensions.AutoColor(new CrayonString(), text);
        }
        public static CrayonString Green(string text)
        {
            return CrayonStringExtensions.AutoColor(new CrayonString(), text);
        }

        public static CrayonString Blue(string text)
        {
            return CrayonStringExtensions.AutoColor(new CrayonString(), text);
        }

        public static CrayonString Yellow(string text)
        {
            return CrayonStringExtensions.AutoColor(new CrayonString(), text);
        }

        public static CrayonString Cyan(string text)
        {
            return CrayonStringExtensions.AutoColor(new CrayonString(), text);
        }

        public static CrayonString Magenta(string text)
        {
            return CrayonStringExtensions.AutoColor(new CrayonString(), text);
        }

        public static CrayonString Gray(string text)
        {
            return CrayonStringExtensions.AutoColor(new CrayonString(), text);
        }

        public static CrayonString White(string text)
        {
            return CrayonStringExtensions.AutoColor(new CrayonString(), text);
        }
        
        public static CrayonString Default(string text)
        {
            return CrayonStringExtensions.AutoColor(new CrayonString(), text);
        }
    }
}