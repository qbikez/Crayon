using System;
namespace Crayons
{
    class ConsoleWriter
    {
        public void WriteString(CrayonString str)
        {
            var tokens = str.Tokenize();
            var lastColor = Console.ForegroundColor;

            try
            {
                if (tokens.Count == 0)
                {
                    /// text is not colored
                    Console.WriteLine(str.Text);
                    return;
                }
                foreach (var token in tokens)
                {
                    WriteToken(token);
                }
                Console.WriteLine();
            }
            finally
            {
                Console.ForegroundColor = lastColor;
            }
        }

        public void WriteToken(CrayonString.CrayonToken token)
        {
            Write(token.Text, token.Color.ConsoleColor);
        }
        
        private void WriteLine(string str = null) {
            if (str != null) System.Console.WriteLine(str);
            else System.Console.WriteLine();
        }
        
        private void Write(string text, ConsoleColor color) {
            Console.ForegroundColor = color;
            Console.Write(text);
        }
    }
}