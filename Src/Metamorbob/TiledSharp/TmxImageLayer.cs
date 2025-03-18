
// Type: TiledSharp.TmxImageLayer




using System.Xml.Linq;


namespace TiledSharp
{
  public class TmxImageLayer : ITmxElement
  {
    public string Name { get; private set; }

    public int? Width { get; private set; }

    public int? Height { get; private set; }

    public bool Visible { get; private set; }

    public double Opacity { get; private set; }

    public double OffsetX { get; private set; }

    public double OffsetY { get; private set; }

    public TmxImage Image { get; private set; }

    public PropertyDict Properties { get; private set; }

    public TmxImageLayer(XElement xImageLayer, string tmxDir = "")
    {
      this.Name = (string) xImageLayer.Attribute((XName) "name");
      this.Width = (int?) xImageLayer.Attribute((XName) "width");
      this.Height = (int?) xImageLayer.Attribute((XName) "height");
      this.Visible = ((bool?)xImageLayer.Attribute((XName)"visible") ?? true) == true;
      double? nullable = (double?) xImageLayer.Attribute((XName) "opacity");
      this.Opacity = nullable ?? 1.0;
      nullable = (double?) xImageLayer.Attribute((XName) "offsetx");
      this.OffsetX = nullable ?? 0.0;
      nullable = (double?) xImageLayer.Attribute((XName) "offsety");
      this.OffsetY = nullable ?? 0.0;
      this.Image = new TmxImage(xImageLayer.Element((XName) "image"), tmxDir);
      this.Properties = new PropertyDict((XContainer) xImageLayer.Element((XName) "properties"));
    }
  }
}
