// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxObjectPoint
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using System;
using System.Globalization;

#nullable disable
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
