using System;

namespace Crayons {
    public class Crayon {
     

        public static void Write(string str)
        {
            new CrayonString(str).WriteToConsole();
        }
    }
}