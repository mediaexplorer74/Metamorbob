
// Type: TiledSharp.PropertyDict




using System;
using System.Collections.Generic;
using System.Xml.Linq;


namespace TiledSharp
{
  [Serializable]
  public class PropertyDict : Dictionary<string, string>
  {
    public PropertyDict(XContainer xmlProp)
    {
      if (xmlProp == null)
        return;
      foreach (XElement element in xmlProp.Elements((XName) "property"))
        this.Add(element.Attribute((XName) "name").Value, element.Attribute((XName) "value").Value);
    }
  }
}
