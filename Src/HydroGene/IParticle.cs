// Decompiled with JetBrains decompiler
// Type: HydroGene.IParticle
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

#nullable disable
namespace HydroGene
{
  internal interface IParticle
  {
    float Life { get; set; }

    bool ToRemove { get; set; }
  }
}
