namespace Elements
{
    public class Network
    {
        public ICollection<Element> Elements { get; private set; } = new HashSet<Element>();
        public ICollection<Connection> Connections { get; private set; } = new HashSet<Connection>();

        public Network(int numberOfElements)
        {
            if (numberOfElements < 0)
            {
                throw new Exception("Number of elements should not be negative.");
            }

            for (int i = 1; i <= numberOfElements; i++)
            {
                Elements.Add(new Element(i));
            }
        }

        public void Connect(int originElementId, int destinyElementId)
        {
            var originElement = Elements.Where(x => x.Id == originElementId).FirstOrDefault();
            var destinyElement = Elements.Where(x => x.Id == destinyElementId).FirstOrDefault();

            if (originElement is null || destinyElement is null)
            {
                throw new Exception("One or more of the indicated elements does not exist.");
            }

            var existingDestinyConnection = Connections.FirstOrDefault(x => x.Elements.Any(x => x.Id == destinyElementId));
            var existingOriginConnection = Connections.FirstOrDefault(x => x.Elements.Any(x => x.Id == originElementId));

            if (existingOriginConnection != null && existingDestinyConnection != null)
            {
                if (existingDestinyConnection == existingOriginConnection)
                    return;

                existingOriginConnection.ConcatElements(existingDestinyConnection.Elements.ToList());
                Connections.Remove(existingDestinyConnection);
            }

            if (existingDestinyConnection != null)
            {
                existingDestinyConnection.AddElement(originElement);
                return;
            }

            if (existingOriginConnection != null)
            {
                existingOriginConnection.AddElement(destinyElement);
                return;
            }

            var newConnection = new Connection(originElement, destinyElement);
            Connections.Add(newConnection);
        }

        public bool Query(int firstElementId, int secondElementId)
        {
            return Connections.Any(x => x.Elements.Any(x => x.Id == firstElementId)
                                     && x.Elements.Any(x => x.Id == secondElementId));
        }
    }
}
