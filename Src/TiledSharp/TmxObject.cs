// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxObject
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using System;
using System.Collections.ObjectModel;
using System.Xml.Linq;

#nullable disable
namespace TiledSharp
{
  public class TmxObject : ITmxElement
  {
    public string Name { get; private set; }

    public TmxObjectType ObjectType { get; private set; }

    public string Type { get; private set; }

    public double X { get; private set; }

    public double Y { get; private set; }

    public double Width { get; private set; }

    public double Height { get; private set; }

    public double Rotation { get; private set; }

    public TmxLayerTile Tile { get; private set; }

    public bool Visible { get; private set; }

    public Collection<TmxObjectPoint> Points { get; private set; }

    public PropertyDict Properties { get; private set; }

    public TmxObject(XElement xObject)
    {
      this.Name = (string) xObject.Attribute((XName) "name") ?? string.Empty;
      this.X = (double) xObject.Attribute((XName) "x");
      this.Y = (double) xObject.Attribute((XName) "y");
      this.Width = (double?) xObject.Attribute((XName) "width") ?? 0.0;
      this.Height = (double?) xObject.Attribute((XName) "height") ?? 0.0;
      this.Type = (string) xObject.Attribute((XName) "type") ?? string.Empty;
      this.Visible = (bool?)xObject.Attribute((XName)"visible") ?? true;
      this.Rotation = (double?) xObject.Attribute((XName) "rotation") ?? 0.0;
      XAttribute id = xObject.Attribute((XName) "gid");
      XElement xelement = xObject.Element((XName) "ellipse");
      XElement xPoints1 = xObject.Element((XName) "polygon");
      XElement xPoints2 = xObject.Element((XName) "polyline");
      if (id != null)
      {
        this.Tile = new TmxLayerTile((uint) id, Convert.ToInt32(Math.Round(this.X)), Convert.ToInt32(Math.Round(this.X)));
        this.ObjectType = TmxObjectType.Tile;
      }
      else if (xelement != null)
        this.ObjectType = TmxObjectType.Ellipse;
      else if (xPoints1 != null)
      {
        this.Points = this.ParsePoints(xPoints1);
        this.ObjectType = TmxObjectType.Polygon;
      }
      else if (xPoints2 != null)
      {
        this.Points = this.ParsePoints(xPoints2);
        this.ObjectType = TmxObjectType.Polyline;
      }
      else
        this.ObjectType = TmxObjectType.Basic;
      this.Properties = new PropertyDict((XContainer) xObject.Element((XName) "properties"));
    }

    public Collection<TmxObjectPoint> ParsePoints(XElement xPoints)
    {
      Collection<TmxObjectPoint> points = new Collection<TmxObjectPoint>();
      string str = (string) xPoints.Attribute((XName) "points");
      char[] chArray = new char[1]{ ' ' };
      foreach (string s in str.Split(chArray))
      {
        TmxObjectPoint tmxObjectPoint = new TmxObjectPoint(s);
        points.Add(tmxObjectPoint);
      }
      return points;
    }
  }
}
