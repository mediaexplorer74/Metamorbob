
// Type: GameManager.Sound




using Microsoft.Xna.Framework.Audio;


namespace GameManager
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
