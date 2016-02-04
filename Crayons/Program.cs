using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crayons
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Crayon.Write(":d:I'm :red:BAD :d:and I'm :green:Good");
            Crayon.Write("here is a string with: colon");
            Crayon.Write("here is a string with::escaped colon");


            Crayon.Write("And I'm not colored!");

            var pattern = new Patterns.Pattern(
                @"\[(?<green>OK)|(?<red>Err.*)\]",
                @"(?<green>good)"
            );
            pattern.Add(@"(?<magenta>'.*')", "highlight quoted names");
            pattern.Add(@"[^\s]+\:\s*(?<cyan>[^\s]+)", "highlight debug values");

            var col = pattern.Colorize("[OK] I'm a good message");
            col.WriteToConsole();
            pattern.Colorize("[Error] I'm a bad message").WriteToConsole();
            pattern.Colorize("test:: colon ").WriteToConsole();
            pattern.Colorize("debug::abc ef").WriteToConsole();
            pattern.Colorize("debug: ced ef").WriteToConsole();

            CrayonString.EscapeChar = "`";

            pattern.Colorize("[OK] I'm a good message").WriteToConsole();
            pattern.Colorize("[Error] I'm a bad message").WriteToConsole();
            pattern.Colorize("test: colon ").WriteToConsole();
            pattern.Colorize("debug:abc ef").WriteToConsole();
            pattern.Colorize("debug: ced ef").WriteToConsole();
            
            var p = new Patterns.Pattern();
            p.Add("(?<magenta>'.*?')", "quoted names");
            p.Add("^(?<green>info):", "info log level");
            p.Add("^(?<yellow>warn):", "warn log level");
            p.Add("^(?<red>err) :", "error log level");
            p.Add("^(?<cyan>verbose):", "verbose log level");
            p.Add("(?<green>done|OK)", "done");
            p.Add("(?<red>Error|Fail|Failed)", "done");
            p.Add("(?<red>Error:.*)", "errors");
            p.Add(@":(?<cyan>[^\s.]+)", "debug values");

            var str = p.Colorize("this is a 'nested match 08:20:00'");
            
            Console.Write(str.ToString());
            str.WriteToConsole();
            
            Console.Read();
        }
    }
}
