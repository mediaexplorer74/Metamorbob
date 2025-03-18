// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxDocument
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

#nullable disable
namespace TiledSharp
{
  public class TmxDocument
  {
    public string TmxDirectory { get; private set; }

    protected XDocument ReadXml(string filepath)
    {
      Assembly entryAssembly = Assembly.GetEntryAssembly();
      string[] array = new string[0];
      if (entryAssembly != (Assembly) null)
        array = entryAssembly.GetManifestResourceNames();
      string fileResPath = filepath.Replace(Path.DirectorySeparatorChar.ToString(), ".");
      string name = Array.Find<string>(array, (Predicate<string>) (s => s.EndsWith(fileResPath)));
      XDocument xdocument;
      if (name != null)
      {
        using (Stream manifestResourceStream = entryAssembly.GetManifestResourceStream(name))
        {
          using (XmlReader reader = XmlReader.Create(manifestResourceStream))
            xdocument = XDocument.Load(reader);
        }
        this.TmxDirectory = string.Empty;
      }
      else
      {
        xdocument = XDocument.Load(filepath);
        this.TmxDirectory = Path.GetDirectoryName(filepath);
      }
      return xdocument;
    }
  }
}
