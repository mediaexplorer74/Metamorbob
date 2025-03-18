
// Type: GameManager.Bob




using GameManager.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace GameManager
{
  internal class Bob : AnimatedSprite
  {
    private const float FULL_GRAVITY = 0.8f;
    private float GRAVITY = 0.8f;
    private const byte NB_SWITCH_PARTICLE = 26;
    private const float SCALE_FACTOR = 1.8f;
    private const byte MAX_VELOCITY_Y = 14;
    private const Keys KeyJump = (Keys) 32;
    private const Keys KeySwitchLeft = (Keys) 87;
    private const Keys KeySwitchRight = (Keys) 67;
    private const Keys KeyShoot = (Keys) 88;
    private const Buttons ButtonJump = (Buttons) 4096;
    private const Buttons ButtonSwitchLeft = (Buttons) 256;
    private const Buttons ButtonSwitchRight = (Buttons) 512;
    private const Buttons ButtonShoot = (Buttons) 16384;
    private Bob.State CurrentState;
    public TileMap tileMap;
    public Vector2 InitialPosition;
    private List<SwitchParticle> ListSwitchParticles = new List<SwitchParticle>();
    public List<Projectile> ListProjectile = new List<Projectile>();
    private bool collide;
    public bool CanJump;
    public bool IsOnCrate;
    public bool IsStanding;
    public bool IsJumping;
    public bool IsDead;
    private bool HasJustBeginToDie;
    private Sprite IconBob;
    private Sprite IconFly;
    private Sprite IconCanon;
    private Text TextMode;
    private Text TextSwitchLeft;
    private Text TextSwitchRight;
    private Sprite IconLB;
    private Sprite IconRB;
    private int HUDOffsetX = 120;

    public Bob.Mode CurrentMode { get; private set; }

    public Bob()
      : base(AssetManager.Bob, 64, 64)
    {
      this.CurrentState = Bob.State.IDLE;
      this.CurrentMode = Bob.Mode.NORMAL;
      this.AddAnimation(Bob.State.IDLE.ToString(), new int[4]
      {
        0,
        1,
        2,
        3
      });
      this.AddAnimation(Bob.State.WALK.ToString(), new int[4]
      {
        4,
        5,
        6,
        7
      });
      this.AddAnimation(Bob.State.JUMP.ToString(), new int[2]
      {
        8,
        9
      });
      this.AddAnimation(Bob.State.FLY.ToString(), new int[4]
      {
        16,
        17,
        18,
        19
      });
      this.AddAnimation(Bob.State.CANON.ToString(), new int[4]
      {
        24,
        25,
        26,
        27
      });
      this.Drag = new Vector2(8f, 16f);
      this.Friction = new Vector2(0.8f, 0.0f);
      this.IconBob = new Sprite(AssetManager.Bobicon);
      this.IconFly = new Sprite(AssetManager.Flyicon);
      this.IconCanon = new Sprite(AssetManager.Canonicon);
      this.TextMode = new Text(AssetManager.FontPixelmaster28, string.Concat((object) this.CurrentMode), Vector2.Zero, Color.DarkViolet);
      this.TextMode.EffectBold = true;
      this.TextMode.Align = Util.Alignement.CENTER_X;
      string pString = GamePadInput.capabilities.IsConnected ? string.Concat((object) (Buttons) 256) : string.Concat((object) (Keys) 87);
      this.TextSwitchLeft = new Text(AssetManager.FontPixelmaster28, pString, Vector2.Zero, Color.DarkViolet);
      this.TextSwitchLeft.EffectBold = true;
      if (!(GamePadInput.capabilities.IsConnected))
        string.Concat((object) (Keys) 67);
      else
        string.Concat((object) (Buttons) 512);
      this.TextSwitchRight = new Text(AssetManager.FontPixelmaster28, pString, Vector2.Zero, Color.DarkViolet);
      this.TextSwitchRight.EffectBold = true;
      this.IconLB = new Sprite(AssetManager.Lb);
      this.IconRB = new Sprite(AssetManager.Rb);
      this.IconLB.Scale = this.IconRB.Scale = new Vector2(2f);
      this.IconLB.IsActive = this.IconRB.IsActive = GamePadInput.capabilities.IsConnected;
    }

    private void CheckCollision()
    {
      this.collide = false;
      if ((double) this.Velocity.Y < 0.0)
      {
        this.collide = this.tileMap.CollideAbove((Sprite) this);
        if (this.collide)
        {
          this.Velocity.Y = 1f;
          this.tileMap.AlignOnLine((Sprite) this);

            if (this.CurrentMode == Bob.Mode.FLY)
            {
                //this.IsDead = true;
            }
        }
      }
      this.collide = false;
      if (this.IsStanding || (double) this.Velocity.Y > 0.0)
      {
        this.collide = this.tileMap.CollideBelow((Sprite) this);
        if (this.collide)
        {
          if (!this.IsStanding)
          {
            AssetManager.Sound_Landing.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
            this.CanJump = true;

            if (this.CurrentMode == Bob.Mode.FLY)
            {
                //this.IsDead = true;
                Debug.WriteLine("[warning] IsDead -> true!");
            }

            if (this.CurrentMode == Bob.Mode.NORMAL)
              this.CreateSwitchParticle((byte) 26, Color.DarkGray, true);
          }

          this.Velocity.Y = 0.0f;
          this.IsJumping = false;
          this.CanJump = true;
          this.IsStanding = true;
          this.tileMap.AlignOnLine((Sprite) this);
          this.Color = Color.White;
        }
        else if ((double) this.GRAVITY != 0.0)
          this.IsStanding = false;
      }
      this.collide = false;
      if ((double) this.Velocity.X < 0.0)
      {
        this.collide = this.tileMap.CollideLeft((Sprite) this);
        if (this.collide)
        {
          this.tileMap.AlignOnColumn((Sprite) this);
          this.Velocity.X = 0.0f;

            if (this.CurrentMode == Bob.Mode.FLY)
            {
                //this.IsDead = true;
            }
        }
      }
      if ((double) this.Velocity.X > 0.0)
      {
        this.collide = this.tileMap.CollideRight((Sprite) this);
        if (this.collide)
        {
          this.tileMap.AlignOnColumn((Sprite) this);
          this.Velocity.X = 0.0f;

            if (this.CurrentMode == Bob.Mode.FLY)
            {
               // this.IsDead = true;
            }
        }
      }
      if (this.IsOnCrate)
      {
        this.Velocity.Y = 0.0f;
        this.IsJumping = false;
        this.CanJump = true;
        this.IsStanding = true;
      }
      if (!this.IsJumping || !this.tileMap.CollideBelow((Sprite) this))
        return;
      this.IsJumping = false;
      this.IsStanding = true;
      this.CanJump = true;
      this.Velocity.Y = 0.0f;
      this.tileMap.AlignOnLine((Sprite) this);
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
        switchParticle.Position = CustomPosition.HasValue
                    ? CustomPosition.Value 
                    : (switchParticle.Position = Vector2.Add(this.Position, new Vector2((float) (this.Width / 2),
                    (float) (this.Height / 2))));
        switchParticle.Color = color;
        this.ListSwitchParticles.Add(switchParticle);
      }
    }

    public override void Update(GameTime gameTime)
    {
      this.Velocity.Y += (this.CurrentMode == Bob.Mode.CANON ? 1.1f : 1f) * this.GRAVITY;
      if ((double) this.Velocity.Y >= 14.0)
        this.Velocity.Y = 14f;
      if (this.tileMap != null)
        this.CheckCollision();
      this.IsOnCrate = false;
      if (this.CurrentMode == Bob.Mode.FLY)
        this.EffectTrail(nbMaxPosition: 12);
      else
        this.StopEffectTrail();
      switch (this.CurrentMode)
      {
        case Bob.Mode.FLY:
          this.CurrentState = Bob.State.FLY;
          this.CanJump = true;
          break;
        case Bob.Mode.CANON:
          this.CurrentState = Bob.State.CANON;
          break;
      }
      this.PlayAnimation(this.CurrentState.ToString(), 0.2f);

      if (!this.IsDead)
      {
        if (this.CurrentState != Bob.State.CANON)
        {
          if (KBInput.Pressed((Keys) 37) 
            || GamePadInput.Pressed((Buttons) 2097152) 
            || GamePadInput.Pressed((Buttons) 4))
          {
            if (this.IsStanding)
              AssetManager.Sound_Footstep.Instance.Play();
            this.Velocity.X = this.CurrentMode == Bob.Mode.NORMAL ? -this.Drag.X : (float) (-(double) this.Drag.X * 0.699999988079071);
          }
          if (KBInput.Pressed((Keys) 39) || GamePadInput.Pressed((Buttons) 1073741824) || GamePadInput.Pressed((Buttons) 8))
          {
            if (this.IsStanding)
              AssetManager.Sound_Footstep.Instance.Play();
            this.Velocity.X = this.CurrentMode == Bob.Mode.NORMAL ? this.Drag.X : this.Drag.X * 0.7f;
          }
          if (this.CanJump && (KBInput.JustPressed((Keys) 32) || GamePadInput.JustPressed((Buttons) 4096)))
          {
            AssetManager.Sound_Jump.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
            if (this.CurrentMode == Bob.Mode.NORMAL)
            {
              this.Velocity.Y = -this.Drag.Y;
              this.CanJump = false;
              this.CreateSwitchParticle((byte) 13, Color.DarkGray, true);
            }
            else
              this.Velocity.Y = (float) (-(double) this.Drag.Y / 2.0);
          }
        }
        if (this.CurrentMode == Bob.Mode.CANON)
        {
          if (KBInput.Pressed((Keys) 37) || GamePadInput.Pressed((Buttons) 2097152) || GamePadInput.Pressed((Buttons) 4))
            this.Flip.X = true;
          if (KBInput.Pressed((Keys) 39) || GamePadInput.Pressed((Buttons) 1073741824) || GamePadInput.Pressed((Buttons) 8))
            this.Flip.X = false;
          if (KBInput.JustPressed((Keys) 88) || GamePadInput.JustPressed((Buttons) 16384))
          {
            AssetManager.Sound_Shoot.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
            Projectile projectile = new Projectile(this);
            this.ListProjectile.Add(projectile);
            Camera.Shake(6f, 0.1f, Axe.HORIZONTAL);
            this.CreateSwitchParticle((byte) 10, Color.Maroon, true, new Vector2?(projectile.Position));
          }
          if (this.CanJump && (KBInput.JustPressed((Keys) 32) || GamePadInput.JustPressed((Buttons) 4096)))
          {
            AssetManager.Sound_Jump.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
            this.Velocity.Y = -0.9f * this.Drag.Y;
            this.CanJump = false;
            this.CreateSwitchParticle((byte) 13, Color.DarkGray, true);
          }
        }
        if (KBInput.JustPressed((Keys) 87) || GamePadInput.JustPressed((Buttons) 256))
        {
          AssetManager.Sound_Switch.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
          Camera.Shake(4f, 0.2f, Axe.VERTICAL);
          this.CanJump = false;
          if (this.IsStanding && this.CurrentMode == Bob.Mode.CANON)
          {
            AssetManager.Sound_Jump.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
            this.Velocity.Y = -0.8f * this.Drag.Y;
          }
          else
            this.Scale = new Vector2(1.8f);
          --this.CurrentMode;
          if (this.CurrentMode == ~Bob.Mode.NORMAL)
            this.CurrentMode = Bob.Mode.CANON;
          switch (this.CurrentMode)
          {
            case Bob.Mode.NORMAL:
              this.CreateSwitchParticle((byte) 26, Color.DarkBlue);
              break;
            case Bob.Mode.FLY:
              this.CreateSwitchParticle((byte) 26, Color.DarkGreen);
              break;
            case Bob.Mode.CANON:
              this.CreateSwitchParticle((byte) 26, Color.DarkOrange);
              break;
          }
        }
        if (KBInput.JustPressed((Keys) 67) || GamePadInput.JustPressed((Buttons) 512))
        {
          AssetManager.Sound_Switch.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
          Camera.Shake(4f, 0.2f, Axe.VERTICAL);
          this.CreateSwitchParticle((byte) 26, Color.Gray);
          this.CanJump = false;
          if (this.IsStanding && this.CurrentMode == Bob.Mode.NORMAL)
          {
            AssetManager.Sound_Jump.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
            this.Velocity.Y = -0.8f * this.Drag.Y;
          }
          else
            this.Scale = new Vector2(1.8f);
          ++this.CurrentMode;
          if (this.CurrentMode > Bob.Mode.CANON)
            this.CurrentMode = Bob.Mode.NORMAL;
          switch (this.CurrentMode)
          {
            case Bob.Mode.NORMAL:
              this.CreateSwitchParticle((byte) 26, Color.DarkBlue);
              break;
            case Bob.Mode.FLY:
              this.CreateSwitchParticle((byte) 26, Color.DarkGreen);
              break;
            case Bob.Mode.CANON:
              this.CreateSwitchParticle((byte) 26, Color.DarkOrange);
              break;
          }
        }
      }
      if ((double) this.Scale.X > 1.0)
        this.Scale = Vector2.Subtract(this.Scale, new Vector2(0.1f));
      if ((double) this.Scale.X <= 1.0)
        this.Scale = Vector2.One;
      if ((double) this.Velocity.X < 0.0)
        this.Flip.X = true;
      else if ((double) this.Velocity.X > 0.0)
        this.Flip.X = false;
      if ((double) this.Velocity.X != 0.0)
        this.CurrentState = Bob.State.WALK;
      if ((double) this.Velocity.Y != 0.0)
      {
        this.CurrentState = Bob.State.JUMP;
        this.IsJumping = (double) this.Velocity.Y < 0.0;
      }
      if (Vector2.Equals(this.Velocity, Vector2.Zero))
      {
        this.IsStanding = true;
        this.CurrentState = Bob.State.IDLE;
      }
      if (this.IsDead)
      {
        this.Alpha = 0.0f;
        if (!this.HasJustBeginToDie)
        {
          ++Game1.NB_DEATH[(int) Game1.CURRENT_LEVEL - 1];
          GamePadInput.Vibrate(0.42f, 0.42f, 0.6f);
          AssetManager.Sound_Enemydie.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
          this.StopEffectTrail();
          Camera.Shake(18f, 0.5f);
          Camera.Flash(0.2f, Color.Red);
          this.CreateSwitchParticle((byte) 52, Color.DarkRed);
          this.HasJustBeginToDie = true;
        }
        if (this.ListSwitchParticles.Count == 0)
        {
          this.Alpha = 1f;
          this.CurrentMode = Bob.Mode.NORMAL;
          this.CurrentState = Bob.State.IDLE;
          this.Position = this.InitialPosition;
          this.IsDead = false;
          this.HasJustBeginToDie = false;
        }
      }
      if (this.ListSwitchParticles.Count >= 200)
      {
        this.ListSwitchParticles.ForEach((Action<SwitchParticle>) (item => item.ToRemove = true));
        this.ListSwitchParticles.RemoveAll((Predicate<SwitchParticle>) (item => item.ToRemove));
      }
      foreach (Sprite listSwitchParticle in this.ListSwitchParticles)
        listSwitchParticle.Update(gameTime);
      foreach (Sprite sprite in this.ListProjectile)
        sprite.Update(gameTime);
      this.ListSwitchParticles.RemoveAll((Predicate<SwitchParticle>) (item => item.ToRemove));
      this.ListProjectile.RemoveAll((Predicate<Projectile>) (item => item.ToRemove));
      this.TextMode.Position = new Vector2(this.TextMode.Position.X, (float) ((double) Camera.Position.Y + (double) Camera.VisibleArea.Height * 0.699999988079071 + 26.0));
      this.TextMode.CurrentString = string.Concat((object) this.CurrentMode);
      this.TextMode.Update(gameTime);
      if (GamePadInput.capabilities.IsConnected)
      {
        this.TextSwitchLeft.IsActive = false;
        this.TextSwitchRight.IsActive = false;
        this.IconLB.IsActive = true;
        this.IconLB.Position = new Vector2(Camera.Position.X + (float) Camera.VisibleArea.Width * 0.3f, Camera.Position.Y + (float) Camera.VisibleArea.Height * 0.86f);
        this.IconLB.Update(gameTime);
        this.IconRB.IsActive = true;
        this.IconRB.Position = new Vector2(Camera.Position.X + (float) Camera.VisibleArea.Width * 0.65f, Camera.Position.Y + (float) Camera.VisibleArea.Height * 0.86f);
        this.IconRB.Update(gameTime);
      }
      else
      {
        this.TextSwitchLeft.IsActive = true;
        this.TextSwitchRight.IsActive = true;
        this.TextSwitchLeft.CurrentString = GamePadInput.capabilities.IsConnected ? "[" + (object) (Buttons) 256 + "]" : "[" + (object) (Keys) 87 + "]";
        this.TextSwitchLeft.Position = new Vector2(Camera.Position.X + (float) Camera.VisibleArea.Width * 0.32f, Camera.Position.Y + (float) Camera.VisibleArea.Height * 0.86f);
        this.TextSwitchLeft.Update(gameTime);
        this.TextSwitchRight.CurrentString = GamePadInput.capabilities.IsConnected ? "[" + (object) (Buttons) 512 + "]" : "[" + (object) (Keys) 67 + "]";
        this.TextSwitchRight.Position = new Vector2(Camera.Position.X + (float) Camera.VisibleArea.Width * 0.64f, Camera.Position.Y + (float) Camera.VisibleArea.Height * 0.86f);
        this.TextSwitchRight.Update(gameTime);
      }
      this.IconBob.Alpha = this.IconFly.Alpha = this.IconCanon.Alpha = 0.4f;
      switch (this.CurrentMode)
      {
        case Bob.Mode.NORMAL:
          this.IconBob.Alpha = 1f;
          break;
        case Bob.Mode.FLY:
          this.IconFly.Alpha = 1f;
          break;
        case Bob.Mode.CANON:
          this.IconCanon.Alpha = 1f;
          break;
      }
      this.IconBob.Position = new Vector2(Camera.Position.X + (float) Camera.VisibleArea.Width * 0.38f, Camera.Position.Y + (float) Camera.VisibleArea.Height * 0.85f);
      this.IconFly.Position = new Vector2(Camera.Position.X + (float) Camera.VisibleArea.Width * 0.38f + (float) this.HUDOffsetX, Camera.Position.Y + (float) Camera.VisibleArea.Height * 0.85f);
      this.IconCanon.Position = new Vector2(Camera.Position.X + (float) Camera.VisibleArea.Width * 0.38f + (float) (this.HUDOffsetX * 2), Camera.Position.Y + (float) Camera.VisibleArea.Height * 0.85f);
      base.Update(gameTime);
      switch (this.CurrentMode)
      {
        case Bob.Mode.NORMAL:
          this.BoundingBox = new Rectangle((int) this.Position.X + 14, (int) this.Position.Y + 10, (int) ((double) (this.Width - 28) * (double) this.Scale.X), (int) ((double) (this.Height - 12) * (double) this.Scale.Y));
          break;
        case Bob.Mode.FLY:
          this.BoundingBox = new Rectangle((int) this.Position.X + 14, (int) this.Position.Y + 26, (int) ((double) (this.Width - 28) * (double) this.Scale.X), (int) ((double) (this.Height - 26) * (double) this.Scale.Y));
          break;
        case Bob.Mode.CANON:
          this.BoundingBox = new Rectangle((int) this.Position.X + 8, (int) this.Position.Y + 26, (int) ((double) (this.Width - 24) * (double) this.Scale.X), (int) ((double) (this.Height - 26) * (double) this.Scale.Y));
          break;
      }
    }

    public void DrawHUD(SpriteBatch spriteBatch)
    {
      byte num1 = this.CurrentMode == Bob.Mode.NORMAL ? (byte) 4 : (byte) 2;
      float num2 = num1 == (byte) 4 ? 1f : 0.6f;
      Primitive.DrawRectangle(Primitive.PrimitiveStyle.LINE, spriteBatch, new Vector2(this.IconBob.Position.X - 6f, this.IconBob.Position.Y - 4f - (float) (8 * (int) num1)), new Vector2(70f, (float) (70 + 16 * (int) num1)),
          Color.Multiply(Color.Blue, num2), (float) ((int) num1 - 1));
      byte thickness1 = this.CurrentMode == Bob.Mode.FLY ? (byte) 4 : (byte) 2;
      float num3 = thickness1 == (byte) 4 ? 1f : 0.6f;
      Primitive.DrawRectangle(Primitive.PrimitiveStyle.LINE, spriteBatch, new Vector2(this.IconFly.Position.X - 5f, this.IconFly.Position.Y - 4f - (float) (8 * (int) thickness1)), new Vector2(70f, (float) (70 + 16 * (int) thickness1)), Color.Multiply(Color.Green, num3), (float) thickness1);
      byte thickness2 = this.CurrentMode == Bob.Mode.CANON ? (byte) 4 : (byte) 2;
      float num4 = thickness2 == (byte) 4 ? 1f : 0.6f;
      Primitive.DrawRectangle(Primitive.PrimitiveStyle.LINE, spriteBatch, new Vector2(this.IconCanon.Position.X - 6f, 
          this.IconCanon.Position.Y - 4f - (float) (8 * (int) thickness2)), new Vector2(70f, 
          (float) (70 + 16 * (int) thickness2)), Color.Multiply(Color.Orange, num4), (float) thickness2);

      Primitive.DrawLine(spriteBatch, new Vector2(Camera.Position.X + 128f,
          (float) ((double) Camera.Position.Y + (double) Camera.VisibleArea.Height * 0.75 + 8.0)), 
          new Vector2(Camera.Position.X + 576f, (float) ((double) Camera.Position.Y 
          + (double) Camera.VisibleArea.Height * 0.75 + 8.0)), Color.DarkViolet, 5);

      Primitive.DrawLine(spriteBatch, new Vector2(Camera.Position.X + 704f,
          (float) ((double) Camera.Position.Y + (double) Camera.VisibleArea.Height * 0.75 + 8.0)), 
          new Vector2(Camera.Position.X + 1152f, (float) ((double) Camera.Position.Y 
          + (double) Camera.VisibleArea.Height * 0.75 + 8.0)), Color.DarkViolet, 5);

      this.IconBob.Draw(spriteBatch);
      this.IconFly.Draw(spriteBatch);
      this.IconCanon.Draw(spriteBatch);
      this.TextMode.Draw(spriteBatch);
      if (GamePadInput.capabilities.IsConnected)
      {
        this.IconLB.Draw(spriteBatch);
        this.IconRB.Draw(spriteBatch);
      }
      this.TextSwitchLeft.Draw(spriteBatch);
      this.TextSwitchRight.Draw(spriteBatch);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      foreach (Sprite listSwitchParticle in this.ListSwitchParticles)
        listSwitchParticle.Draw(spriteBatch);
      foreach (Sprite sprite in this.ListProjectile)
        sprite.Draw(spriteBatch);
    }

    public enum Mode : byte
    {
      NORMAL,
      FLY,
      CANON,
    }

    private enum State : byte
    {
      IDLE,
      WALK,
      JUMP,
      FLY,
      CANON,
    }
  }
}
