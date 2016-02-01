using System.Runtime.CompilerServices;

namespace Crayons
{
    public static class CrayonStringExtensions
    {
        public static CrayonString Color(this CrayonString s1, string colorname, string text)
        {
            return s1
                + new CrayonString(new CrayonColor(colorname), text);
        }

        internal static CrayonString AutoColor(this CrayonString s1, string text, [CallerMemberName] string colorname = null)
        {
            return s1
                + new CrayonString(new CrayonColor(colorname), text);
        }


        public static CrayonString Red(this CrayonString s1, string text)
        {
            return s1.AutoColor(text);
        }
        public static CrayonString Green(this CrayonString s1, string text)
        {
            return s1.AutoColor(text);
        }
        public static CrayonString Blue(this CrayonString s1, string text)
        {
            return s1.AutoColor(text);
        }

        public static CrayonString Yellow(this CrayonString s1, string text)
        {
            return s1.AutoColor(text);
        }

        public static CrayonString Cyan(this CrayonString s1, string text)
        {
            return s1.AutoColor(text);
        }
        public static CrayonString Magenta(this CrayonString s1, string text)
        {
            return s1.AutoColor(text);
        }

        public static CrayonString White(this CrayonString s1, string text)
        {
            return s1.AutoColor(text);
        }
        
         public static CrayonString Gray(string text)
        {
            return CrayonStringExtensions.AutoColor(new CrayonString(), text);
        }
        public static CrayonString Default(this CrayonString s1, string text)
        {
            return s1.AutoColor(text);
        }
        


    }
}