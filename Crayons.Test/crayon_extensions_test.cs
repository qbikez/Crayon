using System.Text;
using Xunit;
using Should;

namespace Crayons.Test
{
    public class crayon_extensions_test
    {
        [Fact]        
        public void color_extenions_work()
        {
            var str1 = Crayon.Red("I'm red").Blue("I'm blue");
            var str2 = new CrayonString(":red:I'm red:blue:I'm blue");
            
            str2.ShouldEqual(str1);
            
            str1 = Crayon.Red("I'm red").Blue("I'm blue").Default(" what?");
            str2 = new CrayonString(":red:I'm red:blue:I'm blue:default: what?");
            
            str2.ShouldEqual(str1);            
        }

    }
}