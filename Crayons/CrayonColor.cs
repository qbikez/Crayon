using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crayons
{
    public class CrayonColor
    {
        public string OriginalName {get; private set;}
        public string ANSIColor;
        
        private ConsoleColor? _consoleColor;
        public ConsoleColor ConsoleColor => _consoleColor ?? DefaultColor;
        public string Name => _consoleColor?.ToString()?.ToLower() ?? "default";       
        
        private static ConsoleColor DefaultColor = ConsoleColor.Gray;
        public static CrayonColor Default = new CrayonColor()
        {
            _consoleColor = null,
            OriginalName = "default"
        };

        public CrayonColor()
        {

        }
        
        public CrayonColor(ConsoleColor color) {
            this.OriginalName = color.ToString();
            this._consoleColor = color;
        }

        public CrayonColor(string color)
        {
            this.OriginalName = color;
            color = color.ToLower();
            ConsoleColor clr;
            if (color == "d" || color == "default")
            {
                this._consoleColor = null;
            }
            else if (Enum.TryParse<ConsoleColor>(color, true, out clr))
            {
                this._consoleColor = clr;
            }
            else
            {
                throw new Exception($"unrecognized color '{color}'");
            }
        }

        public string Format(string value, bool resetColor = true)
        {
            var colored = $"{CrayonString.escapeStart}{this.Name}{CrayonString.escapeEnd}{value}";
            if (resetColor && this != CrayonColor.Default) colored += CrayonColor.Default.Format("", resetColor: false);
            return colored;
        }

        public override string ToString()
        {
            return this.Name;
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
            var other = (CrayonColor)obj;
            return this._consoleColor == other._consoleColor;
        }

        public static bool operator==(CrayonColor c1, CrayonColor c2)
        {
            return c1?.Equals(c2) ?? c1 == c2;
        }
        public static bool operator !=(CrayonColor c1, CrayonColor c2)
        {
            return !(c1 == c2);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return base.GetHashCode();
        }

    }
}
