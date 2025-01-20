using Elements;
using FluentAssertions;

namespace ElementTests
{
    public class ConnectionTests
    {
        [Fact]
        public void ShouldCreateAConnection()
        {
            var elementOrigin = new Element(1);
            var elementDestiny = new Element(2);

            var connection = new Connection(elementOrigin, elementDestiny);

            connection.Elements.Count().Should().Be(2);
            connection.Elements.Should().Contain(elementOrigin);
            connection.Elements.Should().Contain(elementDestiny);
        }

        [Fact]
        public void ShouldAddElement()
        {
            var oldElement1 = new Element(1);
            var oldElement2 = new Element(2);

            var connection = new Connection(oldElement1, oldElement2);

            var newElement = new Element(3);

            connection.AddElement(newElement);

            connection.Elements.Count().Should().Be(3);
            connection.Elements.Should().Contain(oldElement1);
            connection.Elements.Should().Contain(oldElement2);
            connection.Elements.Should().Contain(newElement);
        }

        [Fact]
        public void ShouldConcatElement()
        {
            var element1 = new Element(1);
            var element2 = new Element(2);

            var connection1 = new Connection(element1, element2);

            var element3 = new Element(3);
            var element4 = new Element(4);

            var connection2 = new Connection(element3, element4);

            connection1.ConcatElements(connection2.Elements.ToList());

            connection1.Elements.Count().Should().Be(4);
            connection1.Elements.Should().Contain(element1);
            connection1.Elements.Should().Contain(element2);
            connection1.Elements.Should().Contain(element3);
            connection1.Elements.Should().Contain(element4);
        }
    }
}
