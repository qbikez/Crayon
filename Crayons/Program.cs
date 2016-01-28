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
            Crayon.Write("And I'm not colored!");

            var pattern = new Patterns.Pattern(
                @"\[(?<green>OK)|(?<red>Err.*)\]",
                @"(?<green>good)"
            );
            pattern.Colorize("[OK] I'm a good message").WriteToConsole();
            pattern.Colorize("[Error] I'm a bad message").WriteToConsole();
            Console.Read();
        }
    }
}
