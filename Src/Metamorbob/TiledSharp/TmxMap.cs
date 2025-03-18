
// Type: TiledSharp.TmxMap




using System.Collections.Generic;
using System.Xml.Linq;


namespace TiledSharp
{
  public class TmxMap : TmxDocument
  {
    public string Version { get; private set; }

    public int Width { get; private set; }

    public int Height { get; private set; }

    public int TileWidth { get; private set; }

    public int TileHeight { get; private set; }

    public int? HexSideLength { get; private set; }

    public OrientationType Orientation { get; private set; }

    public StaggerAxisType StaggerAxis { get; private set; }

    public StaggerIndexType StaggerIndex { get; private set; }

    public RenderOrderType RenderOrder { get; private set; }

    public TmxColor BackgroundColor { get; private set; }

    public int? NextObjectID { get; private set; }

    public TmxList<TmxTileset> Tilesets { get; private set; }

    public TmxList<TmxLayer> Layers { get; private set; }

    public TmxList<TmxObjectGroup> ObjectGroups { get; private set; }

    public TmxList<TmxImageLayer> ImageLayers { get; private set; }

    public PropertyDict Properties { get; private set; }

    public TmxMap(string filename)
    {
      XElement xelement = this.ReadXml(filename).Element((XName) "map");
      this.Version = (string) xelement.Attribute((XName) "version");
      this.Width = (int) xelement.Attribute((XName) "width");
      this.Height = (int) xelement.Attribute((XName) "height");
      this.TileWidth = (int) xelement.Attribute((XName) "tilewidth");
      this.TileHeight = (int) xelement.Attribute((XName) "tileheight");
      this.HexSideLength = (int?) xelement.Attribute((XName) "hexsidelength");
      Dictionary<string, OrientationType> dictionary1 = new Dictionary<string, OrientationType>()
      {
        {
          "unknown",
          OrientationType.Unknown
        },
        {
          "orthogonal",
          OrientationType.Orthogonal
        },
        {
          "isometric",
          OrientationType.Isometric
        },
        {
          "staggered",
          OrientationType.Staggered
        },
        {
          "hexagonal",
          OrientationType.Hexagonal
        }
      };
      string key1 = (string) xelement.Attribute((XName) "orientation");
      if (key1 != null)
        this.Orientation = dictionary1[key1];
      Dictionary<string, StaggerAxisType> dictionary2 = new Dictionary<string, StaggerAxisType>()
      {
        {
          "x",
          StaggerAxisType.X
        },
        {
          "y",
          StaggerAxisType.Y
        }
      };
      string key2 = (string) xelement.Attribute((XName) "staggeraxis");
      if (key2 != null)
        this.StaggerAxis = dictionary2[key2];
      Dictionary<string, StaggerIndexType> dictionary3 = new Dictionary<string, StaggerIndexType>()
      {
        {
          "odd",
          StaggerIndexType.Odd
        },
        {
          "even",
          StaggerIndexType.Even
        }
      };
      string key3 = (string) xelement.Attribute((XName) "staggerindex");
      if (key3 != null)
        this.StaggerIndex = dictionary3[key3];
      Dictionary<string, RenderOrderType> dictionary4 = new Dictionary<string, RenderOrderType>()
      {
        {
          "right-down",
          RenderOrderType.RightDown
        },
        {
          "right-up",
          RenderOrderType.RightUp
        },
        {
          "left-down",
          RenderOrderType.LeftDown
        },
        {
          "left-up",
          RenderOrderType.LeftUp
        }
      };
      string key4 = (string) xelement.Attribute((XName) "renderorder");
      if (key4 != null)
        this.RenderOrder = dictionary4[key4];
      this.NextObjectID = (int?) xelement.Attribute((XName) "nextobjectid");
      this.BackgroundColor = new TmxColor(xelement.Attribute((XName) "backgroundcolor"));
      this.Properties = new PropertyDict((XContainer) xelement.Element((XName) "properties"));
      this.Tilesets = new TmxList<TmxTileset>();
      foreach (XElement element in xelement.Elements((XName) "tileset"))
        this.Tilesets.Add(new TmxTileset(element, this.TmxDirectory));
      this.Layers = new TmxList<TmxLayer>();
      foreach (XElement element in xelement.Elements((XName) "layer"))
        this.Layers.Add(new TmxLayer(element, this.Width, this.Height));
      this.ObjectGroups = new TmxList<TmxObjectGroup>();
      foreach (XElement element in xelement.Elements((XName) "objectgroup"))
        this.ObjectGroups.Add(new TmxObjectGroup(element));
      this.ImageLayers = new TmxList<TmxImageLayer>();
      foreach (XElement element in xelement.Elements((XName) "imagelayer"))
        this.ImageLayers.Add(new TmxImageLayer(element, this.TmxDirectory));
    }
  }
}
