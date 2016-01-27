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
            Crayon.Write(":white:I'm :red:BAD :white:and I'm :green:Good");
            Crayon.Write("And I'm not colored!");

            Console.Read();
        }
    }
}
