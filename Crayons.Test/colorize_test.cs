using System.Text;
using Xunit;
using Should;

namespace Crayons.Test
{
    public class colorize_test
    {
        [Fact]
        public void colorize_multiple_instances()
        {
            var pattern = new Crayons.Patterns.Pattern();
            pattern.Add(@"(?<red>\[[a-z]+\])");

            var str = pattern.Colorize("[abc]");
            str.ToString().ShouldEqual(":red:[abc]:default:");

            str = pattern.Colorize("[abc] xxx [def]");
            str.ToString().ShouldEqual(":red:[abc]:default: xxx :red:[def]:default:");

            str = pattern.Colorize("[abc] xxx");
            str.ToString().ShouldEqual(":red:[abc]:default: xxx");

        }

        [Theory]
        [InlineData("[abc]", ":red:[abc]:default:")]
        [InlineData("a'bc'd", "a:yellow:'bc':default:d")]
        // nested version:          
        //[InlineData("[a'bc'd]", ":red:[a:yellow:'bc':red:d]:default:")]
        // "stop on match" version:            
        [InlineData("[a'bc'd]", ":red:[a'bc'd]:default:")]
        public void colorize_multiple_closed_pattern_matches(string original, string expected)
        {
            var pattern = new Crayons.Patterns.Pattern();
            pattern.Add(@"(?<red>\[[a-z]+\])");
            pattern.Add(@"(?<yellow>'[a-z]+')");

            var str = pattern.Colorize(original);
            str.ToString().ShouldEqual(expected);

        }

        [Theory]
        [InlineData("[abc]", ":red:[abc]:default:")]
        [InlineData("a'bc'd", "a:yellow:'bc':default:d")]
        [InlineData("[a'bc'd]", "[a:yellow:'bc':default:d]")]
        public void colorize_multiple_pattern_matches_order(string original, string expected)
        {
            var pattern = new Crayons.Patterns.Pattern();
            pattern.Add(@"(?<yellow>'[a-z]+')");
            pattern.Add(@"(?<red>\[[a-z]+\])");

            var str = pattern.Colorize(original);
            str.ToString().ShouldEqual(expected);
        }

        [Theory]
        [InlineData("this is a 'nested match 08:20:00'", "this is a :magenta:'nested match 08:20:00':default:")]
        public void colorize_multiple_open_pattern_matches(string original, string expected)
        {
            var p = new Patterns.Pattern();
            p.Add("(?<magenta>'.*?')", "quoted names");
            p.Add(@":(?<cyan>[^\s.]+)", "debug values");

            var str = p.Colorize(original);
            str.ToString().ShouldEqual(expected);
        }

    }
}