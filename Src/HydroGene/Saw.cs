// Decompiled with JetBrains decompiler
// Type: HydroGene.Saw
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace HydroGene
{
  internal class Saw : Sprite
  {
    private Bob bob;
    private int factor;
    private int AngleSpeed;
    private Tween TweenY;
    private bool descent = true;
    private bool CanMove;

    public Saw(Bob b, bool canMove = false)
      : base(AssetManager.Saw)
    {
      this.bob = b;
      this.AngleSpeed = Util.RandomInt(14, 20);
      this.factor = Util.RandomIntBetween2Numbers(-1, 1);
      this.Origin = new Vector2((float) (this.Width / 2), (float) (this.Height / 2));
      this.CanMove = canMove;
      if (!canMove)
        return;
      this.TweenY = new Tween(-2, Ease.SINE_OUT);
      this.TweenY.ChangeValue(4, 2.0);
    }

    public override void Update(GameTime gameTime)
    {
      this.Angle += (float) (this.AngleSpeed * this.factor);
      if (this.CanMove)
      {
        this.Position = new Vector2(this.Position.X, this.Position.Y + this.TweenY.Target);
        if (this.TweenY.IsFinished)
        {
          if (this.descent)
          {
            this.TweenY.ChangeValue(-4, 2.0);
            this.descent = false;
          }
          else
          {
            this.TweenY.ChangeValue(4, 2.0);
            this.descent = true;
          }
        }
      }

        if (Util.Overlaps((IActor)this, (IActor)this.bob))
        {
            //this.bob.IsDead = true;
        }

      base.Update(gameTime);
      this.BoundingBox = new Rectangle((int) ((double) this.Position.X - (double) this.Origin.X), (int) ((double) this.Position.Y - (double) this.Origin.Y), (int) ((double) this.Width * (double) this.Scale.X), (int) ((double) this.Height * (double) this.Scale.Y));
    }

    public override void Draw(SpriteBatch spriteBatch) => base.Draw(spriteBatch);
  }
}
