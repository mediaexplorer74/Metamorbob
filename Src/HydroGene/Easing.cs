// Decompiled with JetBrains decompiler
// Type: HydroGene.Easing
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using System;

#nullable disable
namespace HydroGene
{
  internal class Easing
  {
    public static float Linear(double t, double b, int c, double d)
    {
      return (float) ((double) c * t / d + b);
    }

    public static float SineIn(double t, double b, int c, double d)
    {
      return (float) ((double) -c * Math.Cos(t / d * (Math.PI / 2.0)) + (double) c + b);
    }

    public static float SineOut(double t, double b, int c, double d)
    {
      return (float) ((double) c * Math.Sin(t / d * (Math.PI / 2.0)) + b);
    }

    public static float SineInOut(double t, double b, int c, double d)
    {
      return (t /= d / 2.0) < 1.0 ? (float) ((double) (c / 2) * Math.Sin(Math.PI * t / 2.0) + b) : (float) ((double) (-c / 2) * (Math.Cos(Math.PI * --t / 2.0) - 2.0) + b);
    }

    public static float QuadIn(double t, double b, int c, double d)
    {
      t /= d;
      return (float) ((double) c * t * t + b);
    }

    public static float QuadOut(double t, double b, int c, double d)
    {
      t /= d;
      return (float) ((double) -c * t * (t - 2.0) + b);
    }

    public static float QuadInOut(double t, double b, int c, double d)
    {
      t /= d / 2.0;
      if (t < 1.0)
        return (float) ((double) (c / 2) * t * t + b);
      --t;
      return (float) ((double) (-c / 2) * (t * (t - 2.0) - 1.0) + b);
    }

    public static float QuintIn(double t, double b, int c, double d)
    {
      t /= d;
      return (float) ((double) c * t * t * t * t * t + b);
    }

    public static float QuintOut(double t, double b, int c, double d)
    {
      t /= d;
      --t;
      return (float) ((double) c * (t * t * t * t * t + 1.0) + b);
    }

    public static float QuintInOut(double t, double b, int c, double d)
    {
      t /= d / 2.0;
      if (t < 1.0)
        return (float) ((double) (c / 2) * t * t * t * t * t + b);
      t -= 2.0;
      return (float) ((double) (c / 2) * (t * t * t * t * t + 2.0) + b);
    }

    public static float CircIn(double t, double b, int c, double d)
    {
      t /= d;
      return (float) ((double) -c * (Math.Sqrt(1.0 - t * t) - 1.0) + b);
    }

    public static float CircOut(double t, double b, int c, double d)
    {
      t /= d;
      --t;
      return (float) ((double) c * Math.Sqrt(1.0 - t * t) + b);
    }

    public static float CircInOut(double t, double b, int c, double d)
    {
      t /= d / 2.0;
      if (t < 1.0)
        return (float) ((double) (-c / 2) * (Math.Sqrt(1.0 - t * t) - 1.0) + b);
      t -= 2.0;
      return (float) ((double) (c / 2) * (Math.Sqrt(1.0 - t * t) + 1.0) + b);
    }

    public static float ElasticIn(float t, float b, float c, float d, float a, float p)
    {
      if ((double) t == 0.0)
        return b;
      t /= d;
      if ((double) t == 1.0)
        return b + c;
      if ((double) p == 0.0)
        p = d * 0.3f;
      float num;
      if ((double) a == 0.0 || (double) a < (double) Math.Abs(c))
      {
        a = c;
        num = p / 4f;
      }
      else
        num = (float) ((double) p / (2.0 * Math.PI) * Math.Asin((double) c / (double) a));
      --t;
      return (float) -((double) a * Math.Pow(2.0, 10.0 * (double) t) * Math.Sin(((double) t * (double) d - (double) num) * (2.0 * Math.PI) / (double) p)) + b;
    }

    public static float ElasticOut(float t, float b, float c, float d, float a, float p)
    {
      if ((double) t == 0.0)
        return b;
      t /= d;
      if ((double) t == 1.0)
        return b + c;
      if ((double) p == 0.0)
        p = d * 0.3f;
      float num;
      if ((double) a == 0.0 || (double) a < (double) Math.Abs(c))
      {
        a = c;
        num = p / 4f;
      }
      else
        num = (float) ((double) p / (2.0 * Math.PI) * Math.Asin((double) c / (double) a));
      return (float) ((double) a * Math.Pow(2.0, -10.0 * (double) t) * Math.Sin(((double) t * (double) d - (double) num) * (2.0 * Math.PI) / (double) p)) + c + b;
    }
  }
}
