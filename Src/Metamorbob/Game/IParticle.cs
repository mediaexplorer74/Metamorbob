
// Type: GameManager.IParticle





namespace GameManager
{
  internal interface IParticle
  {
    float Life { get; set; }

    bool ToRemove { get; set; }
  }
}
