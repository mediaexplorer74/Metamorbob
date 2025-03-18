// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxTileset
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Linq;

#nullable disable
namespace TiledSharp
{
  public class TmxTileset : TmxDocument, ITmxElement
  {
    public int FirstGid { get; private set; }

    public string Name { get; private set; }

    public int TileWidth { get; private set; }

    public int TileHeight { get; private set; }

    public int Spacing { get; private set; }

    public int Margin { get; private set; }

    public int? Columns { get; private set; }

    public int? TileCount { get; private set; }

    public Collection<TmxTilesetTile> Tiles { get; private set; }

    public TmxTileOffset TileOffset { get; private set; }

    public PropertyDict Properties { get; private set; }

    public TmxImage Image { get; private set; }

    public TmxList<TmxTerrain> Terrains { get; private set; }

    public TmxTileset(XContainer xDoc, string tmxDir)
      : this(xDoc.Element((XName) "tileset"), tmxDir)
    {
    }

    public TmxTileset(XElement xTileset, string tmxDir = "")
    {
      XAttribute xattribute = xTileset.Attribute((XName) "firstgid");
      string path2 = (string) xTileset.Attribute((XName) "source");
      if (path2 != null)
      {
        string filepath = Path.Combine(tmxDir, path2);
        this.FirstGid = (int) xattribute;
        TmxTileset tmxTileset = new TmxTileset((XContainer) this.ReadXml(filepath), this.TmxDirectory);
        this.Name = tmxTileset.Name;
        this.TileWidth = tmxTileset.TileWidth;
        this.TileHeight = tmxTileset.TileHeight;
        this.Spacing = tmxTileset.Spacing;
        this.Margin = tmxTileset.Margin;
        this.Columns = tmxTileset.Columns;
        this.TileCount = tmxTileset.TileCount;
        this.TileOffset = tmxTileset.TileOffset;
        this.Image = tmxTileset.Image;
        this.Terrains = tmxTileset.Terrains;
        this.Tiles = tmxTileset.Tiles;
        this.Properties = tmxTileset.Properties;
      }
      else
      {
        if (xattribute != null)
          this.FirstGid = (int) xattribute;
        this.Name = (string) xTileset.Attribute((XName) "name");
        this.TileWidth = (int) xTileset.Attribute((XName) "tilewidth");
        this.TileHeight = (int) xTileset.Attribute((XName) "tileheight");
        this.Spacing = (int?) xTileset.Attribute((XName) "spacing") ?? 0;
        this.Margin = (int?) xTileset.Attribute((XName) "margin") ?? 0;
        this.Columns = (int?) xTileset.Attribute((XName) "columns");
        this.TileCount = (int?) xTileset.Attribute((XName) "tilecount");
        this.TileOffset = new TmxTileOffset(xTileset.Element((XName) "tileoffset"));
        this.Image = new TmxImage(xTileset.Element((XName) "image"), tmxDir);
        this.Terrains = new TmxList<TmxTerrain>();
        XElement xelement = xTileset.Element((XName) "terraintypes");
        if (xelement != null)
        {
          foreach (XElement element in xelement.Elements((XName) "terrain"))
            this.Terrains.Add(new TmxTerrain(element));
        }
        this.Tiles = new Collection<TmxTilesetTile>();
        foreach (XElement element in xTileset.Elements((XName) "tile"))
          this.Tiles.Add(new TmxTilesetTile(element, this.Terrains, tmxDir));
        this.Properties = new PropertyDict((XContainer) xTileset.Element((XName) "properties"));
      }
    }
  }
}
