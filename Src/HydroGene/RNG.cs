// Decompiled with JetBrains decompiler
// Type: HydroGene.RNG
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using System;

#nullable disable
namespace HydroGene
{
  public static class RNG
  {
    private static Random rng;

    public static void Init(int pSeed = 0)
    {
      if (pSeed == 0)
        RNG.rng = new Random();
      else
        RNG.rng = new Random(pSeed);
    }

    public static void SetSeed(int pSeed) => RNG.rng = new Random(pSeed);

    public static int GetInt(int min, int max) => RNG.rng.Next(min, max + 1);

    public static float GetFloat(float range) => (float) RNG.rng.NextDouble() * range;

    public static float GetFloat(float min, float max)
    {
      return (float) RNG.rng.NextDouble() * (max - min) + min;
    }
  }
}
