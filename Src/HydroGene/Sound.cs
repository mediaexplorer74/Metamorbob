// Decompiled with JetBrains decompiler
// Type: HydroGene.Sound
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework.Audio;

#nullable disable
namespace HydroGene
{
  internal class Sound
  {
    public SoundEffect SoundEffect { get; private set; }

    public SoundEffectInstance Instance { get; set; }

    public Sound(SoundEffect pSF, float pVolume = 1f, float pPan = 0.0f)
    {
      this.SoundEffect = pSF;
      this.Instance = this.SoundEffect.CreateInstance();
      this.Instance.Volume = pVolume;
      this.Instance.Pan = pPan;
    }
  }
}
