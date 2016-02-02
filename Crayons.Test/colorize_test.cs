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

    }
}