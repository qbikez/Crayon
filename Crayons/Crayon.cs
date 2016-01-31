using System;

namespace Crayons {
    public static class Crayon {
     
        public static void Write(string str)
        {
            new CrayonString(str).WriteToConsole();
        }
        
        public static void Write(CrayonString str) {
            str.WriteToConsole();
        }

      
        
        public static void Configure(Action<string, ConsoleColor> write, Action<string> writeline) {
            CrayonString.writer = new CustomConsoleWriter(write, writeline);
        }
        
       
    }
}