
// Type: TiledSharp.TmxTerrain




using System.Xml.Linq;


namespace TiledSharp
{
  public class TmxTerrain : ITmxElement
  {
    public string Name { get; private set; }

    public int Tile { get; private set; }

    public PropertyDict Properties { get; private set; }

    public TmxTerrain(XElement xTerrain)
    {
      this.Name = (string) xTerrain.Attribute((XName) "name");
      this.Tile = (int) xTerrain.Attribute((XName) "tile");
      this.Properties = new PropertyDict((XContainer) xTerrain.Element((XName) "properties"));
    }
  }
}
