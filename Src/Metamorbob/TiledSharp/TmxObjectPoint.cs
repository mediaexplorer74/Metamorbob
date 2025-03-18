
// Type: TiledSharp.TmxObjectPoint




using System;
using System.Globalization;


namespace TiledSharp
{
  public class TmxObjectPoint
  {
    public double X { get; private set; }

    public double Y { get; private set; }

    public TmxObjectPoint(double x, double y)
    {
      this.X = x;
      this.Y = y;
    }

    public TmxObjectPoint(string s)
    {
      string[] strArray = s.Split(',');
      this.X = double.Parse(strArray[0], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture);
      this.Y = double.Parse(strArray[1], NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }
}
