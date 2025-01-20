using Elements;
using FluentAssertions;

namespace ElementTests
{
    public class ElementTests
    {
        [Fact]
        public void ShouldSetIdInConstructor()
        {
            var expectedId = 1;

            var element = new Element(expectedId);
            element.Id.Should().Be(expectedId);
        }
    }
}
