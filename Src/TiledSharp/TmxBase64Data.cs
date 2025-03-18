// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxBase64Data
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Ionic.Zlib;
using System;
using System.IO;
using System.Xml.Linq;

#nullable disable
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
