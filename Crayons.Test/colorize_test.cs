using System.Text;
using Xunit;
using Should;

namespace Crayons.Test
{
    public class colorize_test
    {
        [Theory]
        [InlineData("[abc123] cde]", ":red:[abc123]:default: cde]")]
        [InlineData("[abc] xxx [def]", ":red:[abc]:default: xxx :red:[def]:default:")]
        [InlineData("[abc] xxx", ":red:[abc]:default: xxx")]
        public void colorize_multiple_instances(string original, string expected)
        {
            var pattern = new Crayons.Patterns.Pattern();
            pattern.Add(@"(?<red>\[.+?\])");

            var str = pattern.Colorize(original);
            str.ToString().ShouldEqual(expected);
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
            pattern.Add(@"(?<red>\[.+?\])");
            pattern.Add(@"(?<yellow>'.+?')");

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
            pattern.Add(@"(?<yellow>'.+?')");
            pattern.Add(@"(?<red>\[.+?\])");

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