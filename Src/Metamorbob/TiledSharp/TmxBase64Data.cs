
// Type: TiledSharp.TmxBase64Data




using Ionic.Zlib;
using System;
using System.IO;
using System.Xml.Linq;


namespace TiledSharp
{
  public class TmxBase64Data
  {
    public Stream Data { get; private set; }

    public TmxBase64Data(XElement xData)
    {
      this.Data = !((string) xData.Attribute((XName) "encoding") != "base64") ? (Stream) new MemoryStream(Convert.FromBase64String(xData.Value), false) : throw new Exception("TmxBase64Data: Only Base64-encoded data is supported.");
      switch ((string) xData.Attribute((XName) "compression"))
      {
        case "gzip":
          this.Data = (Stream) new GZipStream(this.Data, CompressionMode.Decompress, false);
          break;
        case "zlib":
          this.Data = (Stream) new ZlibStream(this.Data, CompressionMode.Decompress, false);
          break;
        case null:
          break;
        default:
          throw new Exception("TmxBase64Data: Unknown compression.");
      }
    }
  }
}
