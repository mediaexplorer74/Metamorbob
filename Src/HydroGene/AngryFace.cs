// Decompiled with JetBrains decompiler
// Type: HydroGene.AngryFace
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
  internal class AngryFace : AnimatedSprite
  {
    private byte NB_PARTICLES = 200;
    private Bob bob;
    private List<SwitchParticle> ListSwitchParticles = new List<SwitchParticle>();
    private const byte MAX_HP = 10;
    private byte HP = 10;
    private Timer TimerRed = new Timer(0.1f);
    private Vector2 InitialPosition;

    public AngryFace(Vector2 position, Bob b)
      : base(AssetManager.Monster1, 64, 112)
    {
      this.bob = b;
      this.Origin = new Vector2((float) (this.Width / 2), (float) (this.Height / 2));
      this.InitialPosition = Vector2.Add(position, this.Origin);
      this.Position = Vector2.Add(position, this.Origin);
      this.AddAnimation("IDLE", new int[4]{ 0, 1, 2, 3 });
      this.PlayAnimation("IDLE");
      this.TimerRed.OnComplete = (OnComplete) (() => this.Color = Color.White);
    }

    private void Reset()
    {
      this.HP = (byte) 10;
      this.Alpha = 1f;
      this.IsActive = true;
      this.Scale = Vector2.One;
      this.Color = Color.White;
      this.Position = this.InitialPosition;
    }

    private void CreateSwitchParticle(
      byte nb,
      Color color,
      bool slowSpeed = false,
      Vector2? CustomPosition = null)
    {
      for (int index = 0; index < (int) nb; ++index)
      {
        SwitchParticle switchParticle = new SwitchParticle(slowSpeed);
        switchParticle.Position = CustomPosition.HasValue ? CustomPosition.Value : (switchParticle.Position = Vector2.Add(this.Position, new Vector2((float) (this.Width / 2), (float) (this.Height / 2))));
        switchParticle.Color = color;
        this.ListSwitchParticles.Add(switchParticle);
      }
    }

    public override void Update(GameTime gameTime)
    {
      foreach (Sprite listSwitchParticle in this.ListSwitchParticles)
        listSwitchParticle.Update(gameTime);
      this.ListSwitchParticles.RemoveAll((Predicate<SwitchParticle>) (item => item.ToRemove));
      if (!Vector2.Equals(this.Scale, Vector2.One))
        this.Scale = Vector2.Subtract(this.Scale, new Vector2(0.15f));
      if ((double) this.Scale.X <= 1.0)
        this.Scale = Vector2.One;

      if ((double)this.Alpha != 0.0 && Util.Overlaps((IActor)this, (IActor)this.bob))
      {
            //this.bob.IsDead = true;
      }
      
      if ((double) this.Alpha != 0.0)
      {
        if ((double) this.Position.Y >= (double) this.InitialPosition.Y)
        {
          this.StopEffectTrail();
          this.Velocity = Vector2.Zero;
          this.Position = new Vector2(this.Position.X, this.InitialPosition.Y);
        }
        else
          this.Velocity.Y += 0.6f;
        if (Util.DistanceBetween((IActor) this, (IActor) this.bob) < 512.0)
        {
          if ((double) this.Position.Y >= (double) this.InitialPosition.Y && (double) this.bob.Position.X < (double) this.Position.X)
          {
            this.Flip.X = false;
            this.Velocity.X = -(float) Util.RandomInt(2, 6);
            this.Velocity.Y = -14f;
            this.EffectTrail(0.005f, 18);
          }
          else if ((double) this.Position.Y >= (double) this.InitialPosition.Y && (double) this.bob.Position.X > (double) this.Position.X + (double) this.Width)
          {
            this.Flip.X = true;
            this.Velocity.X = (float) Util.RandomInt(2, 6);
            this.Velocity.Y = -14f;
            this.EffectTrail(0.005f, 18);
          }
        }
        foreach (Projectile actor2 in this.bob.ListProjectile)
        {
          if (!actor2.IsTouchingSomething && Util.Overlaps((IActor) this, (IActor) actor2))
          {
            this.HP -= actor2.Damage;
            actor2.IsTouchingSomething = true;
            this.Scale = new Vector2(1.8f);
            this.Color = Color.DarkRed;
            AssetManager.Sound_Enemydamage.SoundEffect.Play(MainGame.VOLUME_SFX, 0.0f, 0.0f);
          }
        }
      }
      if (this.HP <= (byte) 0)
      {
        this.Velocity = Vector2.Zero;
        if ((double) this.Alpha != 0.0)
        {
          this.CreateSwitchParticle(this.NB_PARTICLES, Color.Black);
          this.StopEffectTrail();
          AssetManager.Sound_Enemydie.SoundEffect.Play(MainGame.VOLUME_SFX, 0.0f, 0.0f);
        }
        this.Alpha = 0.0f;
      }

      if (this.bob.IsDead)
        this.Reset();

      if (Color.Equals(this.Color, Color.DarkRed))
        this.TimerRed.Update(gameTime);
      base.Update(gameTime);
      this.BoundingBox = new Rectangle((int) ((double) this.Position.X - (double) this.Origin.X) + 4, (int) ((double) this.Position.Y - (double) this.Origin.Y) + 26, (int) ((double) (this.Width - 5) * (double) this.Scale.X), (int) ((double) (this.Height - 20) * (double) this.Scale.Y));
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      foreach (Sprite listSwitchParticle in this.ListSwitchParticles)
        listSwitchParticle.Draw(spriteBatch);
    }
  }
}
