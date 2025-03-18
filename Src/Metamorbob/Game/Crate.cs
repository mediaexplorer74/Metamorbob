
// Type: GameManager.Crate




using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameManager
{
  internal class Crate : Sprite
  {
    private Bob bob;
    private const byte MAX_HP = 5;
    private byte HP = 5;
    private Timer TimerRed = new Timer(0.1f);

    public Crate(Bob b)
      : base(AssetManager.Crate)
    {
      this.bob = b;
      this.TimerRed.OnComplete = (OnComplete) (() => this.Color = Color.White);
    }

    private void Reset()
    {
      this.HP = (byte) 5;
      this.Alpha = 1f;
      this.IsActive = true;
      this.Scale = Vector2.One;
      this.Color = Color.White;
    }

    private bool CollideRight()
    {
      return (double) this.bob.Position.X + (double) this.bob.Width + 1.0 >= (double) this.Position.X && (double) this.bob.Position.X + (double) this.bob.Width + 1.0 <= (double) this.Position.X + (double) this.Width && (double) this.bob.Position.Y + (double) (this.bob.Height / 2) >= (double) this.Position.Y && (double) this.bob.Position.Y + (double) (this.bob.Height / 2) <= (double) this.Position.Y + (double) this.Height;
    }

    private bool CollideLeft()
    {
      return (double) this.bob.Position.X - 1.0 >= (double) this.Position.X && (double) this.bob.Position.X - 1.0 <= (double) this.Position.X + (double) this.Width && (double) this.bob.Position.Y + (double) (this.bob.Height / 2) >= (double) this.Position.Y && (double) this.bob.Position.Y + (double) (this.bob.Height / 2) <= (double) this.Position.Y + (double) this.Height;
    }

    private bool CollideAbove()
    {
      return (double) this.bob.Position.X + (double) (this.bob.Width / 2) >= (double) this.Position.X && (double) this.bob.Position.X + (double) (this.bob.Width / 2) <= (double) this.Position.X + (double) this.Width && (double) this.bob.Position.Y - 1.0 >= (double) this.Position.Y && (double) this.bob.Position.Y - 1.0 <= (double) this.Position.Y + (double) this.Height;
    }

    private bool CollideBelow()
    {
      return (double) this.bob.Position.X + (double) (this.bob.Width / 2) >= (double) this.Position.X && (double) this.bob.Position.X + (double) (this.bob.Width / 2) <= (double) this.Position.X + (double) this.Width && (double) this.bob.Position.Y + (double) this.bob.Height + 1.0 >= (double) this.Position.Y && (double) this.bob.Position.Y + (double) this.bob.Height + 1.0 <= (double) this.Position.Y + (double) this.Height;
    }

    public override void Update(GameTime gameTime)
    {
      if ((double) this.Alpha != 0.0 && Util.DistanceBetween((IActor) this, (IActor) this.bob) <= 1.2000000476837158 * (double) Camera.VisibleArea.Width)
      {
        if (!Vector2.Equals(this.Scale, Vector2.One))
          this.Scale = Vector2.Subtract(this.Scale, new Vector2(0.1f));
        if ((double) this.Scale.X <= 1.0)
          this.Scale = Vector2.One;
        if (this.bob.CurrentMode == Bob.Mode.FLY)
        {
            if (Util.Overlaps((IActor)this, (IActor)this.bob))
            {
               // this.bob.IsDead = true;
            }
        }
        else
        {
          if (this.CollideRight())
          {
            this.bob.Velocity.X = 0.0f;
            this.bob.Position = new Vector2(this.Position.X - (float) this.bob.Width, this.bob.Position.Y);
          }
          if (this.CollideLeft())
          {
            this.bob.Velocity.X = 0.0f;
            this.bob.Position = new Vector2(this.Position.X + (float) this.Width, this.bob.Position.Y);
          }
          if (this.CollideAbove())
          {
            this.bob.Velocity.Y = 1f;
            this.bob.Position = new Vector2(this.bob.Position.X, this.Position.Y + (float) this.Height);
          }
          if (this.CollideBelow())
          {
            if (!this.bob.IsStanding)
              AssetManager.Sound_Landing.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
            this.bob.Velocity.Y = 0.0f;
            this.bob.Position = new Vector2(this.bob.Position.X, (float) ((double) this.Position.Y - (double) this.bob.Height - 1.0));
            this.bob.IsOnCrate = true;
          }
        }
        foreach (Projectile actor2 in this.bob.ListProjectile)
        {
          if (!actor2.IsTouchingSomething && Util.Overlaps((IActor) this, (IActor) actor2))
          {
            this.HP -= actor2.Damage;
            actor2.IsTouchingSomething = true;
            this.Scale = new Vector2(1.6f);
            this.Color = Color.DarkRed;
            AssetManager.Sound_Enemydamage.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
          }
        }
      }
      if (this.HP <= (byte) 0)
      {
        if ((double) this.Alpha != 0.0)
          AssetManager.Sound_Boxexplode.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
        this.Alpha = 0.0f;
      }

      if (this.bob.IsDead)
        this.Reset();

      if (Color.Equals(this.Color, Color.DarkRed))
        this.TimerRed.Update(gameTime);
      base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch) => base.Draw(spriteBatch);
  }
}
