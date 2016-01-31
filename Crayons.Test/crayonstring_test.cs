using System.Text;
using Xunit;
using Should;

namespace Crayons.Test
{
    public class crayonstring_test
    {
        [Theory]
//        [InlineData(":d:I'm :red:BAD :d:and I'm :green:Good:d:")]
        [InlineData("And I'm not colored!")]
        [InlineData("And I have an unescaped colon: not colored!")]
        [InlineData("And I have an escaped colon:: not colored!")]
        //fails: [InlineData("And I have an unescaped :colon: not colored!")]
        [InlineData("And I have an unescaped colon:not colored:!")]
        public void plain_string_is_properly_parsed(string str)
        {
            CrayonString crayon = new CrayonString(str);
            var tokens = crayon.Tokenize();

            var sb = new StringBuilder();
            foreach (var token in tokens)
            {
                sb.Append(token.Text);
            }

            var removedExcapes = str.Replace($"{CrayonString.escapeStart}{CrayonString.escapeStart}", $"{CrayonString.escapeStart}");
            
            sb.ToString().ShouldEqual(removedExcapes);
        }

       // [Theory]
        [InlineData(":d:I'm :red:BAD :d:and I'm :green:Good:d:")]
        public void colors_are_properly_parsed(string str) {
                  CrayonString crayon = new CrayonString(str);
            var tokens = crayon.Tokenize();

            var sb = new StringBuilder();
            foreach (var token in tokens)
            {
                sb.Append(CrayonString.escapeStart)
                    .Append(token.Color.OriginalName)
                    .Append(CrayonString.escapeEnd);
                sb.Append(token.Text);
            }

            //var removedExcapes = str.Replace($"{CrayonString.escapeStart}{CrayonString.escapeStart}", $"{CrayonString.escapeStart}");
            
            sb.ToString().ShouldEqual(str);       
        }
    }
}