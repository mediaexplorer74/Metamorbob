
// Type: TiledSharp.TmxColor




using System.Globalization;
using System.Xml.Linq;


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
