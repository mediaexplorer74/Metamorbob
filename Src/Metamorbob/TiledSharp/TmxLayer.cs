
// Type: TiledSharp.TmxLayer




using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Linq;


namespace TiledSharp
{
  public class TmxLayer : ITmxElement
  {
    public string Name { get; private set; }

    public double Opacity { get; private set; }

    public bool Visible { get; private set; }

    public double? OffsetX { get; private set; }

    public double? OffsetY { get; private set; }

    public Collection<TmxLayerTile> Tiles { get; private set; }

    public PropertyDict Properties { get; private set; }

    public TmxLayer(XElement xLayer, int width, int height)
    {
      this.Name = (string) xLayer.Attribute((XName) "name");
      this.Opacity = (double?) xLayer.Attribute((XName) "opacity") ?? 1.0;
      this.Visible = (bool?)xLayer.Attribute((XName)"visible") ?? true;
      double? nullable = (double?) xLayer.Attribute((XName) "offsetx");
      this.OffsetX = new double?(nullable ?? 0.0);
      nullable = (double?) xLayer.Attribute((XName) "offsety");
      this.OffsetY = new double?(nullable ?? 0.0);
      XElement xData = xLayer.Element((XName) "data");
      string str1 = (string) xData.Attribute((XName) "encoding");
      this.Tiles = new Collection<TmxLayerTile>();
      switch (str1)
      {
        case "base64":
          using (BinaryReader binaryReader = new BinaryReader(new TmxBase64Data(xData).Data))
          {
            for (int y = 0; y < height; ++y)
            {
              for (int x = 0; x < width; ++x)
                this.Tiles.Add(new TmxLayerTile(binaryReader.ReadUInt32(), x, y));
            }
            break;
          }
        case "csv":
          string str2 = xData.Value;
          int num1 = 0;
          char[] chArray = new char[1]{ ',' };
          foreach (string str3 in str2.Split(chArray))
          {
            this.Tiles.Add(new TmxLayerTile(uint.Parse(str3.Trim()), num1 % width, num1 / width));
            ++num1;
          }
          break;
        case null:
          int num2 = 0;
          using (IEnumerator<XElement> enumerator = xData.Elements((XName) "tile").GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              this.Tiles.Add(new TmxLayerTile((uint) enumerator.Current.Attribute((XName) "gid"), num2 % width, num2 / width));
              ++num2;
            }
            break;
          }
        default:
          throw new Exception("TmxLayer: Unknown encoding.");
      }
      this.Properties = new PropertyDict((XContainer) xLayer.Element((XName) "properties"));
    }
  }
}
