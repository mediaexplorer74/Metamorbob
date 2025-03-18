// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxAnimationFrame
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using System.Xml.Linq;

#nullable disable
namespace TiledSharp
{
  public class TmxAnimationFrame
  {
    public int Id { get; private set; }

    public int Duration { get; private set; }

    public TmxAnimationFrame(XElement xFrame)
    {
      this.Id = (int) xFrame.Attribute((XName) "tileid");
      this.Duration = (int) xFrame.Attribute((XName) "duration");
    }
  }
}
