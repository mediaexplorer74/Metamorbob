// Decompiled with JetBrains decompiler
// Type: HydroGene.Boss
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
  internal class Boss : AnimatedSprite
  {
    private const short NB_PARTICLES = 400;
    private const float TIMER_IDLE = 2f;
    private const int JUMP_MIN = 12;
    private const int JUMP_MAX = 28;
    private const byte MAX_NB_JUMP = 8;
    private byte CurrentNBJump;
    private Bob bob;
    private List<SwitchParticle> ListSwitchParticles = new List<SwitchParticle>();
    private Vector2 InitialPosition;
    private const byte HP_MAX = 40;
    private byte HP = 40;
    private Timer TimerRed = new Timer(0.1f);
    private Timer TimerWait = new Timer(2f);
    private Text TextName;
    private Boss.State CurrentState;

    public Boss(Vector2 pos, Bob b)
      : base(AssetManager.Boss, 136, 208)
    {
      this.bob = b;
      this.Name = "SUPER ANGRY JUMP-MAN";
      this.CurrentState = Boss.State.IDLE;
      this.Origin = new Vector2((float) (this.Width / 2), (float) (this.Height / 2));
      this.Position = Vector2.Add(pos, this.Origin);
      this.InitialPosition = Vector2.Add(pos, this.Origin);
      this.AddAnimation("IDLE", new int[4]{ 0, 1, 2, 3 });
      this.PlayAnimation("IDLE");
      this.TimerRed.OnComplete = (OnComplete) (() => this.Color = Color.White);
      this.TextName = new Text(AssetManager.FontPixelmaster28, this.Name, Vector2.Zero, Color.Red);
      this.TextName.EffectBold = true;
      this.TextName.Align = Util.Alignement.CENTER_X;
    }

    private void Reset()
    {
      this.HP = (byte) 40;
      this.Alpha = 1f;
      this.IsActive = true;
      this.Scale = Vector2.One;
      this.Color = Color.White;
      this.Position = this.InitialPosition;
    }

    private void CreateSwitchParticle(
      short nb,
      Color color,
      bool slowSpeed = false,
      Vector2? CustomPosition = null)
    {
      this.ListSwitchParticles.ForEach((Action<SwitchParticle>) (item => item.Alpha = 0.0f));
      this.ListSwitchParticles.RemoveAll((Predicate<SwitchParticle>) (item => item.ToRemove));
      for (int index = 0; index < (int) nb; ++index)
      {
        SwitchParticle switchParticle = new SwitchParticle(slowSpeed);
        switchParticle.Position = CustomPosition.HasValue ? CustomPosition.Value : (switchParticle.Position = this.Position);
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
      this.TextName.Update(gameTime);
      this.TextName.Position = new Vector2(this.TextName.Position.X, 
          Camera.Position.Y + (float) Camera.VisibleArea.Height * 0.01f);

        if ((double)this.Alpha != 0.0 && Util.Overlaps((IActor)this, (IActor)this.bob))
        {
            //this.bob.IsDead = true;
        }

      if ((double) this.Position.X - (double) (this.Width / 2) < 128.0)
        this.Velocity.X = -this.Velocity.X;
      if ((double) this.Position.X + (double) (this.Width / 2) > 1536.0)
        this.Velocity.X = -this.Velocity.X;
      if (this.CurrentState != Boss.State.JUMP)
      {
        if ((double) this.bob.Position.X < (double) this.Position.X)
          this.Flip.X = false;
        else if ((double) this.bob.Position.X > (double) this.Position.X + (double) this.Width * (double) this.Scale.X)
          this.Flip.X = true;
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
          this.Velocity.Y += 0.8f;
        switch (this.CurrentState)
        {
          case Boss.State.IDLE:
            this.TimerWait.Update(gameTime);
            this.TimerWait.OnComplete = (OnComplete) (() => this.CurrentState = Boss.State.JUMP);
            break;
          case Boss.State.JUMP:
            if ((double) this.Position.Y >= (double) this.InitialPosition.Y && (double) this.bob.Position.X < (double) this.Position.X)
            {
              if (this.CurrentNBJump >= (byte) 8)
              {
                this.CurrentNBJump = (byte) 0;
                this.CurrentState = Boss.State.IDLE;
                Camera.Shake(10f, 0.2f, Axe.VERTICAL);
              }
              this.Flip.X = false;
              this.Velocity.X = (float) -Util.RandomInt(1, 8);
              this.Velocity.Y = (float) -Util.RandomInt(12, 28);
              this.EffectTrail(0.005f, 14);
              AssetManager.Sound_Bosslanding.SoundEffect.Play(MainGame.VOLUME_SFX, -0.5f, 0.0f);
              this.CreateSwitchParticle((short) 40, Color.DarkGray, true, new Vector2?(new Vector2(this.Position.X, this.Position.Y + (float) (this.Height / 2))));
              ++this.CurrentNBJump;
              break;
            }
            if ((double) this.Position.Y >= (double) this.InitialPosition.Y && (double) this.bob.Position.X > (double) this.Position.X)
            {
              if (this.CurrentNBJump >= (byte) 8)
              {
                this.CurrentNBJump = (byte) 0;
                this.CurrentState = Boss.State.IDLE;
                Camera.Shake(10f, 0.2f, Axe.VERTICAL);
              }
              this.Flip.X = true;
              this.Velocity.X = (float) Util.RandomInt(1, 8);
              this.Velocity.Y = (float) -Util.RandomInt(12, 28);
              this.EffectTrail(0.005f, 14);
              this.CreateSwitchParticle((short) 40, Color.DarkGray, true, new Vector2?(new Vector2(this.Position.X, this.Position.Y + (float) (this.Height / 2))));
              AssetManager.Sound_Bosslanding.SoundEffect.Play(MainGame.VOLUME_SFX, -0.5f, 0.0f);
              ++this.CurrentNBJump;
              break;
            }
            break;
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
        if (this.HP <= (byte) 0)
        {
          this.Velocity = Vector2.Zero;
          if ((double) this.Alpha != 0.0)
          {
            this.CreateSwitchParticle((short) 400, Color.Black);
            this.StopEffectTrail();
            AssetManager.Sound_Enemydie.SoundEffect.Play(MainGame.VOLUME_SFX, 0.0f, 0.0f);
          }
          this.Alpha = 0.0f;
        }
      }

      if (this.bob.IsDead && (double) this.Alpha != 0.0)
        this.Reset();

      if (Color.Equals(this.Color, Color.DarkRed))
        this.TimerRed.Update(gameTime);
      base.Update(gameTime);
      this.BoundingBox = new Rectangle((int) ((double) this.Position.X - (double) this.Origin.X) + 14, (int) ((double) this.Position.Y - (double) this.Origin.Y) + 32, this.Width - 28, this.Height - 40);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      this.TextName.Draw(spriteBatch);
      base.Draw(spriteBatch);
      foreach (Sprite listSwitchParticle in this.ListSwitchParticles)
        listSwitchParticle.Draw(spriteBatch);
      Primitive.DrawRectangle(Primitive.PrimitiveStyle.FILL, spriteBatch, new Rectangle((int) ((double) Camera.Position.X + (double) Camera.VisibleArea.Width * 0.20000000298023224), (int) ((double) Camera.Position.Y + (double) Camera.VisibleArea.Height * 0.059999998658895493), 20 * (int) this.HP, 12), Color.Red);
      Primitive.DrawRectangle(Primitive.PrimitiveStyle.LINE, spriteBatch, new Rectangle((int) ((double) Camera.Position.X + (double) Camera.VisibleArea.Width * 0.20000000298023224) - 2, (int) ((double) Camera.Position.Y + (double) Camera.VisibleArea.Height * 0.059999998658895493) - 2, 802, 14), Color.Black, 2f);
    }

    private enum State : byte
    {
      IDLE,
      JUMP,
      MISSILE,
    }
  }
}
