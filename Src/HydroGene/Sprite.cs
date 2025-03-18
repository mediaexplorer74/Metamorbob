// Decompiled with JetBrains decompiler
// Type: HydroGene.Sprite
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

#nullable disable
namespace HydroGene
{
  public class Sprite : IActor
  {
    public Vector2 Velocity;
    public Vector2 Friction = Vector2.Zero;
    public float Angle;
    public Color Color;
    public float Alpha;
    public Vector2 Scale;
    public Flip Flip;
    private SpriteEffects flipEffect;
    public bool EffectBlink;
    public float timerVisible = 0.5f;
    public float BlinkFrequency = 0.5f;
    protected internal List<Vector2> listTrailPosition;
    protected internal bool ActiveEffectTrail;
    protected internal List<float> listAngle = new List<float>();
    protected internal int MaxPosition;
    private Timer TrailUpdateTimer = new Timer(0.1f);

    public Vector2 Position { get; set; }

    public Vector2 Origin { get; set; }

    public string Name { get; set; }

    public int Width { get; protected set; }

    public int Height { get; protected set; }

    public Vector2 Drag { get; set; }

    public Rectangle BoundingBox { get; protected set; }

    public bool DrawBoundingBox { get; set; }

    public Color BoundingBoxColor { get; set; } = Color.Red;

    public bool ToRemove { get; set; }

    public Texture2D Texture { get; set; }

    public bool IsVisible { get; set; } = true;

    public bool IsActive { get; set; } = true;

    private Util.Alignement Align { get; set; }

    public Sprite(Texture2D texture)
    {
      this.Texture = texture;
      this.Angle = 0.0f;
      this.Alpha = 1f;
      this.Color = Color.White;
      this.Scale = new Vector2(1f, 1f);
      this.Flip = new Flip();
      this.Velocity = new Vector2(0.0f, 0.0f);
      this.Origin = Vector2.Zero;
      this.ToRemove = false;
      this.Width = this.Texture.Width;
      this.Height = this.Texture.Height;
      this.flipEffect = (SpriteEffects) 0;
      this.listTrailPosition = new List<Vector2>();
      this.Align = Util.Alignement.NONE;
    }

    // ?
    public virtual void TouchedBy(IActor By)
    {
    }

    public void Move(float posX, float posY)
    {
      this.Position = new Vector2(this.Position.X + posX, this.Position.Y + posY);
      if ((double) this.Velocity.X < 0.0)
      {
        this.Velocity.X += this.Friction.X;
        if ((double) this.Velocity.X > 0.0)
          this.Velocity.X = 0.0f;
      }
      if ((double) this.Velocity.X > 0.0)
      {
        this.Velocity.X -= this.Friction.X;
        if ((double) this.Velocity.X < 0.0)
          this.Velocity.X = 0.0f;
      }
      if ((double) this.Velocity.Y < 0.0)
      {
        this.Velocity.Y += this.Friction.Y;
        if ((double) this.Velocity.Y > 0.0)
          this.Velocity.Y = 0.0f;
      }
      if ((double) this.Velocity.Y <= 0.0)
        return;
      this.Velocity.Y -= this.Friction.Y;
      if ((double) this.Velocity.Y >= 0.0)
        return;
      this.Velocity.Y = 0.0f;
    }

    public bool IsOnScreen() => Util.Overlaps((IActor) this, Camera.VisibleArea);

    public void EffectTrail(float frameUpdate = 0.01f, int nbMaxPosition = 25)
    {
      if (!this.ActiveEffectTrail)
      {
        if (this.TrailUpdateTimer == null)
          this.TrailUpdateTimer = new Timer(frameUpdate);
        else
          this.TrailUpdateTimer.ChangeTimerValue(frameUpdate);
        this.ActiveEffectTrail = true;
      }
      this.MaxPosition = nbMaxPosition;
    }

    public void StopEffectTrail()
    {
      this.ActiveEffectTrail = false;
      this.listTrailPosition.RemoveRange(0, this.listTrailPosition.Count);
    }

    public virtual void Update(GameTime gameTime)
    {
      if (MainGame.IS_PAUSED || !this.IsActive)
        return;
      this.Move(this.Velocity.X, this.Velocity.Y);
      this.BoundingBox = new Rectangle((int) this.Position.X, (int) this.Position.Y, (int) ((double) this.Texture.Width * (double) this.Scale.X), (int) ((double) this.Texture.Height * (double) this.Scale.Y));
      if ((double) this.Alpha >= 1.0)
        this.Alpha = 1f;
      else if ((double) this.Alpha <= 0.0)
        this.Alpha = 0.0f;
      if (this.ActiveEffectTrail)
      {
        this.TrailUpdateTimer.Update(gameTime);
        this.TrailUpdateTimer.OnComplete = (OnComplete) (() =>
        {
          this.listTrailPosition.Add(this.Position);
          this.listAngle.Add(this.Angle);
        });
        if (this.listTrailPosition.Count > this.MaxPosition)
        {
          this.listTrailPosition.RemoveAt(0);
          this.listAngle.RemoveAt(0);
        }
      }
      switch (this.Align)
      {
        case Util.Alignement.CENTER_X:
          this.Position = new Vector2(Camera.Position.X + (float) (Camera.VisibleArea.Width / 2 - this.Width / 2), this.Position.Y);
          break;
        case Util.Alignement.CENTER_Y:
          this.Position = new Vector2(this.Position.X, Camera.Position.Y + (float) (Camera.VisibleArea.Height / 2 - this.Height / 2));
          break;
        case Util.Alignement.CENTER_X | Util.Alignement.CENTER_Y:
          this.Position = new Vector2(Camera.Position.X + (float) (Camera.VisibleArea.Width / 2 - this.Width / 2), this.Position.Y);
          this.Position = new Vector2(this.Position.X, Camera.Position.Y + (float) (Camera.VisibleArea.Height / 2 - this.Height / 2));
          break;
      }
      if (!this.EffectBlink)
        return;
      this.timerVisible -= 0.01f;
      if ((double) this.timerVisible >= 0.0)
        return;
      this.timerVisible = !this.IsVisible ? this.BlinkFrequency : this.BlinkFrequency / 2f;
      this.IsVisible = !this.IsVisible;
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
      this.flipEffect = (SpriteEffects) 0;
      if (this.Flip.X)
        this.flipEffect = (SpriteEffects) 1;
      if (this.Flip.Y)
        this.flipEffect = (SpriteEffects) 2;
      if (this.Flip.X && this.Flip.Y)
        this.flipEffect = (SpriteEffects) 3;
      if (!this.IsActive || !this.IsVisible)
        return;
      if (this.ActiveEffectTrail)
      {
        foreach (Vector2 vector2 in this.listTrailPosition)
          spriteBatch.Draw(this.Texture, vector2, new Rectangle?(), Color.Multiply(this.Color, (float) this.listTrailPosition.IndexOf(vector2) / 100f), MathHelper.ToRadians(this.Angle), this.Origin, this.Scale, this.flipEffect, 0.0f);
      }
      if (this.Texture != null)
        spriteBatch.Draw(this.Texture, this.Position, new Rectangle?(), Color.Multiply(this.Color, this.Alpha), MathHelper.ToRadians(this.Angle), this.Origin, this.Scale, this.flipEffect, 0.0f);
      if (!this.DrawBoundingBox)
        return;
      Primitive.DrawRectangle(Primitive.PrimitiveStyle.FILL, spriteBatch, (float) this.BoundingBox.X, (float) this.BoundingBox.Y, this.BoundingBox.Width, this.BoundingBox.Height, Color.Multiply(this.BoundingBoxColor, 0.5f));
    }
  }
}
