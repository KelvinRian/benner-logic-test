namespace Elements
{
    public class Connection
    {
        public ICollection<Element> Elements { get; private set; } = new List<Element>();

        public Connection(Element elementOrigin, Element elementDestiny)
        {
            Elements.Add(elementOrigin);
            Elements.Add(elementDestiny);
        }

        public void AddElement(Element element)
        {
            Elements.Add(element);
        }

        public void ConcatElements(List<Element> elements)
        {
            Elements = Elements.Concat(elements).ToList();
        }
    }
}
