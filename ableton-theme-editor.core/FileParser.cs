using System.Xml;

namespace ableton_theme_editor.core
{
    public class FileParser
    {
        public IEnumerable<ThemeProperty> Open(string path)
        {
            XmlDocument doc = new XmlDocument();

            if (string.IsNullOrEmpty(path) | !File.Exists(path))
            {
                var error = $"File not found: {path}";
                throw new ArgumentException(error);
            }
            
            doc.Load(path);
            XmlNodeList? nodes = doc.GetElementsByTagName("Theme");
            var theme = nodes.Item(0);

            foreach (XmlNode node in theme.ChildNodes)
            {
                yield return new ThemeProperty
                {
                    Name = node.Name,
                    Colour = node.Attributes["Value"]?.Value
                };
            }
        }
    }

    public class ThemeProperty
    {
        public string Name { get; set; }
        public string Colour { get; set; }
    }
}