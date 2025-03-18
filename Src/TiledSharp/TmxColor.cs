// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxColor
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using System.Globalization;
using System.Xml.Linq;

#nullable disable
namespace TiledSharp
{
  public class TmxColor
  {
    public int R { get; private set; }

    public int G { get; private set; }

    public int B { get; private set; }

    public TmxColor(XAttribute xColor)
    {
      if (xColor == null)
        return;
      string str = ((string) xColor).TrimStart("#".ToCharArray());
      this.R = int.Parse(str.Substring(0, 2), NumberStyles.HexNumber);
      this.G = int.Parse(str.Substring(2, 2), NumberStyles.HexNumber);
      this.B = int.Parse(str.Substring(4, 2), NumberStyles.HexNumber);
    }
  }
}
