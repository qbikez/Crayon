using System;

namespace Crayons
{
    internal class CustomConsoleWriter : ConsoleWriter
    {
        private Action<string, ConsoleColor> write;
        private Action<string> writeline;
        public CustomConsoleWriter(Action<string, ConsoleColor> write, Action<string> writeline)
        {
            Configure(write, writeline);
        }
        
        internal void Configure(Action<string, ConsoleColor> write, Action<string> writeline)
        {
            this.write = write;
            this.writeline = writeline;
        }   
        
        protected override void Write(string text, ConsoleColor color) {
            this.write(text, color);
        }
        
        protected override void WriteLine(string text){
            this.writeline(text);
        }
    }
}