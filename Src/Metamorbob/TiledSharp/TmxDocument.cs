
// Type: TiledSharp.TmxDocument




using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;


namespace TiledSharp
{
  public class TmxDocument
  {
    public string TmxDirectory { get; private set; }

    protected XDocument ReadXml(string filepath)
    {
      Assembly entryAssembly = null;//Assembly.GetEntryAssembly();
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
