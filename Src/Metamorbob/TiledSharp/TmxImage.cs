
// Type: TiledSharp.TmxImage




using System.IO;
using System.Xml.Linq;


namespace TiledSharp
{
  public class TmxImage
  {
    public string Source { get; private set; }

    public string Format { get; private set; }

    public Stream Data { get; private set; }

    public TmxColor Trans { get; private set; }

    public int? Width { get; private set; }

    public int? Height { get; private set; }

    public TmxImage(XElement xImage, string tmxDir = "")
    {
      if (xImage == null)
        return;
      XAttribute path2 = xImage.Attribute((XName) "source");
      if (path2 != null)
      {
        this.Source = Path.Combine(tmxDir, (string) path2);
      }
      else
      {
        this.Format = (string) xImage.Attribute((XName) "format");
        this.Data = new TmxBase64Data(xImage.Element((XName) "data")).Data;
      }
      this.Trans = new TmxColor(xImage.Attribute((XName) "trans"));
      this.Width = (int?) xImage.Attribute((XName) "width");
      this.Height = (int?) xImage.Attribute((XName) "height");
    }
  }
}
