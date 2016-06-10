using Murmansk.ComponentModel.DataAnnotations;
using Xunit;

namespace Murmansk.ComponentModel.Tests.DataAnnotations
{
    public class StringLengthTest
    {
        private StringLengthAttribute sut;

        [Fact]
        public void Test1()
        {
            sut = new StringLengthAttribute(0, 5);
        }
    }
}
