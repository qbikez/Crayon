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
        [InlineData("And I have an escaped colon:: not colored!")]
        public void plain_string_is_properly_parsed(string str)
        {
            CrayonString crayon = new CrayonString(str);
            var tokens = crayon.Tokenize();

            var sb = new StringBuilder();
            foreach (var token in tokens)
            {
                // sb.Append(CrayonString.escapeStart)
                //     .Append(token.Color.ToString().ToLower())
                //     .Append(CrayonString.escapeStart);
                sb.Append(token.Text);
            }

            sb.ToString().ShouldEqual(str);
        }

        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }
    }
}