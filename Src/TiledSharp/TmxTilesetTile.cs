// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxTilesetTile
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using System.Collections.ObjectModel;
using System.Xml.Linq;

#nullable disable
namespace TiledSharp
{
  public class TmxTilesetTile
  {
    public int Id { get; private set; }

    public Collection<TmxTerrain> TerrainEdges { get; private set; }

    public double Probability { get; private set; }

    public PropertyDict Properties { get; private set; }

    public TmxImage Image { get; private set; }

    public TmxList<TmxObjectGroup> ObjectGroups { get; private set; }

    public Collection<TmxAnimationFrame> AnimationFrames { get; private set; }

    public TmxTerrain TopLeft => this.TerrainEdges[0];

    public TmxTerrain TopRight => this.TerrainEdges[1];

    public TmxTerrain BottomLeft => this.TerrainEdges[2];

    public TmxTerrain BottomRight => this.TerrainEdges[3];

    public TmxTilesetTile(XElement xTile, TmxList<TmxTerrain> Terrains, string tmxDir = "")
    {
      this.Id = (int) xTile.Attribute((XName) "id");
      this.TerrainEdges = new Collection<TmxTerrain>();
      string str = (string) xTile.Attribute((XName) "terrain") ?? ",,,";
      char[] chArray = new char[1]{ ',' };
      foreach (string s in str.Split(chArray))
      {
        int result;
        this.TerrainEdges.Add(!int.TryParse(s, out result) ? (TmxTerrain) null : Terrains[result]);
      }
      this.Probability = (double?) xTile.Attribute((XName) "probability") ?? 1.0;
      this.Image = new TmxImage(xTile.Element((XName) "image"), tmxDir);
      this.ObjectGroups = new TmxList<TmxObjectGroup>();
      foreach (XElement element in xTile.Elements((XName) "objectgroup"))
        this.ObjectGroups.Add(new TmxObjectGroup(element));
      this.AnimationFrames = new Collection<TmxAnimationFrame>();
      if (xTile.Element((XName) "animation") != null)
      {
        foreach (XElement element in xTile.Element((XName) "animation").Elements((XName) "frame"))
          this.AnimationFrames.Add(new TmxAnimationFrame(element));
      }
      this.Properties = new PropertyDict((XContainer) xTile.Element((XName) "properties"));
    }
  }
}
