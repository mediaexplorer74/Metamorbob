// Decompiled with JetBrains decompiler
// Type: HydroGene.AnimatedSprite
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

#nullable disable
namespace HydroGene
{
  internal class AnimatedSprite : Sprite
  {
    private SpriteEffects flipEffect;
    public int CurrentFrame;
    private Dictionary<string, int[]> ListAnimations;
    private int[] CurrentAnimation;
    private float globalSpeedAnimation = 0.2f;
    private double timeElapsed;

    public int nbColumns { get; private set; }

    public int nbLines { get; private set; }

    public int SpritesheetWidth { get; private set; }

    public int SpritesheetHeight { get; private set; }

    private bool animationIsStopped { get; set; }

    public bool HasAnimationFinishedOnce { get; set; }

    public string CurrentAnimationName { get; private set; }

    public bool IsAnimationFinish { get; set; }

    public bool IsAnimationLooped { get; set; }

    public AnimatedSprite(Texture2D texture, Vector2 pos, int tileWidth, int tileHeight)
      : base(texture)
    {
      this.Position = pos;
      this.nbColumns = texture.Width / tileWidth;
      this.nbLines = texture.Height / tileHeight;
      this.SpritesheetWidth = texture.Width;
      this.SpritesheetHeight = texture.Height;
      this.Width = tileWidth;
      this.Height = tileHeight;
      this.CurrentFrame = 0;
      this.CurrentAnimationName = (string) null;
      this.ListAnimations = new Dictionary<string, int[]>();
      this.CurrentFrame = 0;
    }

    public AnimatedSprite(Texture2D texture, int tileWidth, int tileHeight)
      : base(texture)
    {
      this.Position = Vector2.Zero;
      this.nbColumns = texture.Width / tileWidth;
      this.nbLines = texture.Height / tileHeight;
      this.SpritesheetWidth = texture.Width;
      this.SpritesheetHeight = texture.Height;
      this.Width = tileWidth;
      this.Height = tileHeight;
      this.CurrentFrame = 0;
      this.CurrentAnimationName = (string) null;
      this.ListAnimations = new Dictionary<string, int[]>();
      this.CurrentFrame = 0;
    }

    public void RefreshTexture(Texture2D newTexture, ushort tileWidth, ushort tileHeight)
    {
      this.Texture = newTexture;
      this.nbColumns = this.Texture.Width / (int) tileWidth;
      this.nbLines = this.Texture.Height / (int) tileHeight;
      this.SpritesheetWidth = this.Texture.Width;
      this.SpritesheetHeight = this.Texture.Height;
      this.Width = (int) tileWidth;
      this.Height = (int) tileHeight;
    }

    public void AddAnimation(string pName, int[] pFrames)
    {
      this.ListAnimations.Add(pName, pFrames);
    }

    public void PlayAnimation(string pName, float pSpeed = 0.1f, bool isLooped = true)
    {
      this.globalSpeedAnimation = pSpeed;
      if (this.CurrentAnimationName == null || this.CurrentAnimationName != pName || this.IsAnimationFinish)
      {
        this.CurrentFrame = 0;
        this.CurrentAnimation = this.ListAnimations[pName];
        this.CurrentAnimationName = pName;
        this.IsAnimationLooped = isLooped;
        this.IsAnimationFinish = false;
        this.timeElapsed = 0.0;
        this.HasAnimationFinishedOnce = false;
      }
      if (this.timeElapsed >= (double) this.globalSpeedAnimation)
      {
        this.timeElapsed -= (double) this.globalSpeedAnimation;
        ++this.CurrentFrame;
      }
      if (this.CurrentFrame < this.CurrentAnimation.Length)
        return;
      this.IsAnimationFinish = !this.IsAnimationLooped;
      this.CurrentFrame = this.IsAnimationLooped ? 0 : this.CurrentAnimation.Length - 1;
    }

    public void StopAnimation() => this.animationIsStopped = true;

    public override void Update(GameTime gameTime)
    {
      if (MainGame.IS_PAUSED || !this.IsActive)
        return;
      base.Update(gameTime);
      if (this.CurrentAnimationName != null)
      {
        this.timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
        this.PlayAnimation(this.CurrentAnimationName, this.globalSpeedAnimation);
      }
      if ((int) Math.Abs(this.Angle) == 90)
        this.BoundingBox = new Rectangle((int) ((double) this.Position.X - (double) this.Width * (double) this.Scale.X / 2.0), (int) ((double) this.Position.Y - (double) this.Height * (double) this.Scale.Y / 2.0), (int) ((double) this.Height * (double) this.Scale.Y), (int) ((double) this.Width * (double) this.Scale.X));
      else
        this.BoundingBox = new Rectangle((int) this.Position.X, (int) this.Position.Y, (int) ((double) this.Width * (double) this.Scale.X), (int) ((double) this.Height * (double) this.Scale.Y));
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      this.flipEffect = (SpriteEffects) 0;
      if (this.Flip.X)
        this.flipEffect = (SpriteEffects) 1;
      if (this.Flip.Y)
        this.flipEffect = (SpriteEffects) 2;
      if (this.Flip.X && this.Flip.Y)
        this.flipEffect = (SpriteEffects) 3;
      int num1 = 0;
      int num2 = 0;
      if (this.CurrentAnimation != null)
      {
        num1 = this.CurrentAnimation[this.CurrentFrame] % this.nbColumns;
        num2 = this.CurrentAnimation[this.CurrentFrame] / this.nbColumns;
      }
      Rectangle rectangle = new Rectangle(this.Width * num1, this.Height * num2, this.Width, this.Height);
      if (!this.IsVisible || !this.IsActive)
        return;
      if (this.ActiveEffectTrail)
      {
        foreach (Vector2 vector2 in this.listTrailPosition)
          spriteBatch.Draw(this.Texture, vector2, new Rectangle?(rectangle), Color.Multiply(this.Color, (float) this.listTrailPosition.IndexOf(vector2) / 100f), MathHelper.ToRadians(this.Angle), this.Origin, this.Scale, this.flipEffect, 0.0f);
      }
      spriteBatch.Draw(this.Texture, this.Position, new Rectangle?(rectangle), Color.Multiply(this.Color, this.Alpha), MathHelper.ToRadians(this.Angle), this.Origin, this.Scale, this.flipEffect, 0.0f);
      if (!this.DrawBoundingBox)
        return;
      Primitive.DrawRectangle(Primitive.PrimitiveStyle.FILL, spriteBatch, (float) this.BoundingBox.X, (float) this.BoundingBox.Y, this.BoundingBox.Width, this.BoundingBox.Height, 
          Color.Multiply(this.BoundingBoxColor, 0.5f));
    }
  }
}
