using Elements;
using FluentAssertions;

namespace ElementTests
{
    public class NetworkTests
    {

        [Fact]
        public void ShouldGenerateElementsInConstructor()
        {
            var numberOfElements = 3;

            var network = new Network(numberOfElements);

            network.Elements.Count().Should().Be(numberOfElements);
            network.Elements.Should().Contain(x => x.Id == 1);
            network.Elements.Should().Contain(x => x.Id == 2);
            network.Elements.Should().Contain(x => x.Id == 3);
        }

        [Fact]
        public void ShouldThrowExceptionWhenTryToGenerateInvalidNumberOfElements()
        {
            int invalidNumberOfElements = -1;

            Action action = () => new Network(invalidNumberOfElements);

            action.Should().Throw<Exception>()
                .WithMessage("Number of elements should not be negative.");
        }

        [Fact]
        public void ShouldCreateAConection()
        {
            var network = new Network(2);

            network.Connect(1, 2);

            network.Connections.Count().Should().Be(1);
            network.Connections.Should()
                .Contain(x => x.Elements.Any(y => y.Id == 1) && x.Elements.Any(y => y.Id == 2));
        }

        [Fact]
        public void ShouldThrowExceptionConnectingInvalidElement()
        {
            Action action = () =>
            {
                var network = new Network(1);
                network.Connect(1, 2);
            };

            action.Should().Throw<Exception>()
                .WithMessage("One or more of the indicated elements does not exist.");
        }

        [Fact]
        public void ShouldAppendOriginElementToAnExistingConnection()
        {
            var network = new Network(3);
            network.Connect(1, 2);

            network.Connect(3, 2);

            network.Connections.Count().Should().Be(1);
            network.Connections.Should()
                .Contain(x => x.Elements.Any(y => y.Id == 1) 
                           && x.Elements.Any(y => y.Id == 2)
                           && x.Elements.Any(y => y.Id == 3));
        }

        [Fact]
        public void ShouldAppendDestinyElementToAnExistingConnection()
        {
            var network = new Network(3);
            network.Connect(1, 2);

            network.Connect(2, 3);

            network.Connections.Count().Should().Be(1);
            network.Connections.Should()
                .Contain(x => x.Elements.Any(y => y.Id == 1)
                           && x.Elements.Any(y => y.Id == 2)
                           && x.Elements.Any(y => y.Id == 3));
        }

        [Fact]
        public void ShouldConcatTwoConnectionsWhenConnectingAnElementToEachOther()
        {
            var network = new Network(4);
            network.Connect(1, 2);
            network.Connect(3, 4);

            network.Connect(2, 3);

            network.Connections.Count().Should().Be(1);
            network.Connections.Should()
                .Contain(x => x.Elements.Any(y => y.Id == 1)
                           && x.Elements.Any(y => y.Id == 2)
                           && x.Elements.Any(y => y.Id == 3)
                           && x.Elements.Any(y => y.Id == 4));
        }

        [Fact]
        public void ShouldNotConnectElementsAlreadyConnectedToEachOther()
        {
            var network = new Network(2);

            network.Connect(1, 2);
            network.Connect(2, 1);

            network.Connections.Count().Should().Be(1);
            network.Connections.Should()
                .Contain(x => x.Elements.Any(y => y.Id == 1) && x.Elements.Any(y => y.Id == 2));
        }

        [Fact]
        public void ShouldReturnTrueOnQueryWhenElementsAreConnected()
        {
            var network = new Network(2);
            network.Connect(1, 2);

            var result = network.Query(1, 2);

            result.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnFalseOnQueryWhenElementsAreNotConnected()
        {
            var network = new Network(3);
            network.Connect(1, 2);

            var result = network.Query(2, 3);

            result.Should().BeFalse();
        }

        [Fact]
        public void ShouldReturnFalseOnQueryWhenElementsDoesNotExist()
        {
            var network = new Network(2);
            network.Connect(1, 2);

            var result = network.Query(1, 10);

            result.Should().BeFalse();
        }
    }
}
