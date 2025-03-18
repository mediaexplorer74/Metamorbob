
// Type: GameManager.Timer




using Microsoft.Xna.Framework;


namespace GameManager
{
  internal class Timer
  {
    private bool IsLaunched;
    public OnComplete OnComplete;

    public float CurrentTimer { get; private set; }

    public float TotalTimer { get; private set; }

    public int Turn { get; private set; }

    public bool IsFinished { get; private set; }

    public bool IsLooped { get; set; }

    public Timer(float initialValue, bool loop = true, bool launched = true)
    {
      this.TotalTimer = initialValue;
      this.CurrentTimer = this.TotalTimer;
      this.IsLaunched = launched;
      this.IsLooped = loop;
    }

    public void Reset(bool removeDelegateOnComplete = true)
    {
      this.CurrentTimer = this.TotalTimer;
      this.IsFinished = false;
      this.Turn = 0;
      if (!removeDelegateOnComplete)
        return;
      this.OnComplete = (OnComplete) null;
    }

    public void ChangeCurrentTimerOnly(float newValue) => this.CurrentTimer = newValue;

    public void ChangeTimerValue(float newValue)
    {
      this.TotalTimer = newValue;
      this.CurrentTimer = newValue;
    }

    public void Update(GameTime gameTime)
    {
      if (!this.IsLaunched || this.IsFinished)
        return;
      this.CurrentTimer -= (float) gameTime.ElapsedGameTime.TotalSeconds;
      if ((double) this.CurrentTimer >= 0.0)
        return;
      this.IsFinished = true;
      if (!this.IsLooped)
        return;
      ++this.Turn;
      if (this.OnComplete != null)
        this.OnComplete();
      this.CurrentTimer = this.TotalTimer;
      this.IsFinished = false;
    }
  }
}
