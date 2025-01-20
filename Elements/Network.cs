namespace Elements
{
    public class Network
    {
        public ICollection<Element> Elements { get; private set; } = new List<Element>();
        public ICollection<Connection> Connections { get; private set; } = new List<Connection>();

        public Network(int numberOfElements)
        {
            if (ValidNumberOfElements(numberOfElements))
                CreateElements(numberOfElements);
        }

        private bool ValidNumberOfElements(int numberOfElements)
        {
            if (numberOfElements < 0)
                throw new Exception("Number of elements should not be negative.");

            return true;
        }

        private void CreateElements(int numberOfElements)
        {
            for (int i = 1; i <= numberOfElements; i++)
            {
                Elements.Add(new Element(i));
            }
        }

        public void Connect(int originElementId, int destinyElementId)
        {
            var originElement = Elements.Where(x => x.Id == originElementId).FirstOrDefault();
            var destinyElement = Elements.Where(x => x.Id == destinyElementId).FirstOrDefault();

            var elementIdsAreInvalid = originElement is null || destinyElement is null;
            if (elementIdsAreInvalid)
            {
                throw new Exception("One or more of the indicated elements does not exist.");
            }

            var ConnectionWithDestinyElement = Connections.FirstOrDefault(x => x.Elements.Any(x => x.Id == destinyElementId));
            var ConenctionWithOriginElement = Connections.FirstOrDefault(x => x.Elements.Any(x => x.Id == originElementId));
            var oneElementAlreadyConnected = ConnectionWithDestinyElement != null || ConenctionWithOriginElement != null;

            if (oneElementAlreadyConnected)
            {
                ConcatIfElementsHaveConnections(ConnectionWithDestinyElement, ConenctionWithOriginElement);
                AddElementIntoHisConnectedElementConnection(originElement, destinyElement, ConnectionWithDestinyElement, ConenctionWithOriginElement);
            }
            else
            {
                CreateConnection(originElement, destinyElement);
            }
        }

        private void ConcatIfElementsHaveConnections(Connection? ConnectionWithDestinyElement, Connection? ConenctionWithOriginElement)
        {
            var bothElementsHaveConnections = ConenctionWithOriginElement != null && ConnectionWithDestinyElement != null;
            if (bothElementsHaveConnections)
            {
                var elementsAreInTheSameConnection = ConnectionWithDestinyElement == ConenctionWithOriginElement;
                if (elementsAreInTheSameConnection)
                    return;

                Concat(ConnectionWithDestinyElement, ConenctionWithOriginElement);
            }
        }

        private void Concat(Connection ConnectionWithDestinyElement, Connection ConenctionWithOriginElement)
        {
            ConenctionWithOriginElement.ConcatElements(ConnectionWithDestinyElement.Elements.ToList());
            Connections.Remove(ConnectionWithDestinyElement);
        }

        private static void AddElementIntoHisConnectedElementConnection(Element? originElement, Element? destinyElement, Connection? ConnectionWithDestinyElement, Connection? ConenctionWithOriginElement)
        {
            if (ConnectionWithDestinyElement != null)
            {
                ConnectionWithDestinyElement.AddElement(originElement);
                return;
            }

            if (ConenctionWithOriginElement != null)
            {
                ConenctionWithOriginElement.AddElement(destinyElement);
                return;
            }
        }

        private void CreateConnection(Element? originElement, Element? destinyElement)
        {
            var newConnection = new Connection(originElement, destinyElement);
            Connections.Add(newConnection);
        }

        public bool Query(int firstElementId, int secondElementId)
        {
            return ElementsAreConnected(firstElementId, secondElementId);
        }

        private bool ElementsAreConnected(int firstElementId, int secondElementId)
        {
            return Connections.Any(x => x.Elements.Any(x => x.Id == firstElementId)
                                     && x.Elements.Any(x => x.Id == secondElementId));
        }
    }
}
