// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxTileOffset
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using System.Xml.Linq;

#nullable disable
namespace TiledSharp
{
  public class TmxTileOffset
  {
    public int X { get; private set; }

    public int Y { get; private set; }

    public TmxTileOffset(XElement xTileOffset)
    {
      if (xTileOffset == null)
      {
        this.X = 0;
        this.Y = 0;
      }
      else
      {
        this.X = (int) xTileOffset.Attribute((XName) "x");
        this.Y = (int) xTileOffset.Attribute((XName) "y");
      }
    }
  }
}
