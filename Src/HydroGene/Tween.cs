// Decompiled with JetBrains decompiler
// Type: HydroGene.Tween
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace HydroGene
{
  internal class Tween
  {
    public double Time;
    public double Value;
    public int Distance;
    public double Duration;
    public Ease Ease;
    public float Target;
    public bool ToRemove;
    public float Amplitude = 0.2f;
    public float Period = 1f;

    public bool IsFinished { get; private set; }

    public bool ContinueIfPaused { get; set; } = true;

    public OnComplete OnComplete { get; set; }

    public bool StopWhenPause { get; set; }

    public Tween(int target, int distance, double duration, Ease ease = Ease.LINEAR)
    {
      this.Time = 0.0;
      this.Value = (double) target;
      this.Distance = distance;
      this.Duration = duration;
      this.Ease = ease;
      this.Target = (float) target;
      Tweening.list_tweens.Add(this);
    }

    public Tween(int target, Ease ease = Ease.LINEAR)
    {
      this.Time = 0.0;
      this.Value = (double) target;
      this.Distance = 0;
      this.Duration = 0.0099999997764825821;
      this.Ease = ease;
      this.Target = (float) target;
      Tweening.list_tweens.Add(this);
    }

    public void ChangeValue(int distance, double duration, Ease ease)
    {
      this.Value = (double) this.Target;
      this.Time = 0.0;
      this.Distance = distance;
      this.Duration = duration;
      this.Ease = ease;
    }

    public void ChangeValue(int distance, double duration)
    {
      this.Value = (double) this.Target;
      this.Time = 0.0;
      this.Distance = distance;
      this.Duration = duration;
    }

    private void PrintTrace(Tween tween)
    {
      Console.Write("EASE = " + (object) tween.Ease + " | ");
      Console.Write("TARGET = " + (object) tween.Target + " | ");
      Console.Write("TIME = " + (object) tween.Time + " | ");
      Console.WriteLine("VALUE = " + (object) tween.Value);
    }

    public void Update(GameTime gameTime)
    {
      if (this.Time < this.Duration)
      {
        if (!this.StopWhenPause || !MainGame.IS_PAUSED && MainGame.Instance.Screen.Effect == null)
        {
          this.Time += gameTime.ElapsedGameTime.TotalSeconds;
          this.IsFinished = false;
        }
      }
      else
      {
        this.IsFinished = true;
        if (this.OnComplete != null)
          this.OnComplete();
      }
      switch (this.Ease)
      {
        case Ease.LINEAR:
          this.Target = Easing.Linear(this.Time, this.Value, this.Distance, this.Duration);
          break;
        case Ease.SINE_IN:
          this.Target = Easing.SineIn(this.Time, this.Value, this.Distance, this.Duration);
          break;
        case Ease.SINE_OUT:
          this.Target = Easing.SineOut(this.Time, this.Value, this.Distance, this.Duration);
          break;
        case Ease.SINE_IN_OUT:
          this.Target = Easing.SineInOut(this.Time, this.Value, this.Distance, this.Duration);
          break;
        case Ease.QUAD_IN:
          this.Target = Easing.QuadIn(this.Time, this.Value, this.Distance, this.Duration);
          break;
        case Ease.QUAD_OUT:
          this.Target = Easing.QuadOut(this.Time, this.Value, this.Distance, this.Duration);
          break;
        case Ease.QUAD_IN_OUT:
          this.Target = Easing.QuadInOut(this.Time, this.Value, this.Distance, this.Duration);
          break;
        case Ease.QUINT_IN:
          this.Target = Easing.QuintIn(this.Time, this.Value, this.Distance, this.Duration);
          break;
        case Ease.QUINT_OUT:
          this.Target = Easing.QuintOut(this.Time, this.Value, this.Distance, this.Duration);
          break;
        case Ease.QUINT_IN_OUT:
          this.Target = Easing.QuintInOut(this.Time, this.Value, this.Distance, this.Duration);
          break;
        case Ease.CIRC_IN:
          this.Target = Easing.CircIn(this.Time, this.Value, this.Distance, this.Duration);
          break;
        case Ease.CIRC_OUT:
          this.Target = Easing.CircOut(this.Time, this.Value, this.Distance, this.Duration);
          break;
        case Ease.CIRC_IN_OUT:
          this.Target = Easing.CircInOut(this.Time, this.Value, this.Distance, this.Duration);
          break;
        case Ease.ELASTIC_OUT:
          this.Target = Easing.ElasticOut((float) this.Time, (float) this.Value, (float) this.Distance, (float) this.Duration, this.Amplitude, this.Period);
          break;
      }
    }
  }
}
