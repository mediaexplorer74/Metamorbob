// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxTerrain
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using System.Xml.Linq;

#nullable disable
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
