
// Type: GameManager.Projectile




using GameManager.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace GameManager
{
  internal class Projectile : AnimatedSprite
  {
    private Bob bob;
    public Flip Flip = new Flip();
    private TileMap tileMap;
    private readonly int ANGLE_SPEED = 12;
    private const byte NB_SWITCH_PARTICLES = 20;
    private const byte SPEED = 18;
    private List<SwitchParticle> ListSwitchParticles;

    public byte Damage { get; set; }

    public bool IsTouchingSomething { get; set; }

    public Projectile(Bob bob)
      : base(AssetManager.Bullet, 32, 32)
    {
      this.Damage = (byte) 1;
      this.ListSwitchParticles = new List<SwitchParticle>();
      this.bob = bob;
      this.tileMap = bob.tileMap;
      this.Scale = new Vector2(2f);
      this.Drag = new Vector2(18f);
      this.Origin = new Vector2((float) (this.Width / 2), (float) (this.Height / 2));
      this.Position = bob.Flip.X ? new Vector2(bob.Position.X, bob.Position.Y + (float) (bob.Height / 2)) : new Vector2(bob.Position.X + (float) bob.Width, bob.Position.Y + (float) (bob.Height / 2));
      this.AddAnimation("MOVE", new int[4]{ 0, 1, 2, 3 });
      this.PlayAnimation("MOVE", 0.14f);
      this.EffectTrail(1f / 400f, 18);
      this.Velocity.X = bob.Flip.X ? -this.Drag.X : this.Drag.X;
      if (!bob.Flip.X)
        return;
      this.ANGLE_SPEED = -this.ANGLE_SPEED;
    }

    private void CreateSwitchParticle(byte nb, Color color, bool slowSpeed = false)
    {
      for (int index = 0; index < (int) nb; ++index)
      {
        SwitchParticle switchParticle = new SwitchParticle(slowSpeed);
        switchParticle.Position = this.Position;
        switchParticle.Color = color;
        this.ListSwitchParticles.Add(switchParticle);
      }
    }

    private void CheckCollision()
    {
      if (this.tileMap.CollideLeft((Sprite) this))
      {
        AssetManager.Sound_Impact.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
        this.IsTouchingSomething = true;
      }
      else
      {
        if (!this.tileMap.CollideRight((Sprite) this))
          return;
        AssetManager.Sound_Impact.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
        this.IsTouchingSomething = true;
      }
    }

    public override void Update(GameTime gameTime)
    {
      this.Angle += (float) this.ANGLE_SPEED;
      if (!this.IsTouchingSomething)
        this.CheckCollision();
      if (this.IsTouchingSomething && (double) this.Alpha != 0.0)
      {
        GamePadInput.Vibrate(0.2f, 0.2f, 0.2f);
        Camera.Shake(8f, 0.2f);
        this.Velocity = Vector2.Zero;
        this.CreateSwitchParticle((byte) 20, Color.DarkViolet, true);
        this.Alpha = 0.0f;
      }
      if ((double) this.Alpha == 0.0 && this.IsTouchingSomething && this.ListSwitchParticles.Count == 0)
        this.ToRemove = true;
      foreach (Sprite listSwitchParticle in this.ListSwitchParticles)
        listSwitchParticle.Update(gameTime);
      this.ListSwitchParticles.RemoveAll((Predicate<SwitchParticle>) (item => item.ToRemove));
      base.Update(gameTime);
      this.BoundingBox = new Rectangle((int) ((double) this.Position.X - (double) this.Origin.X), (int) ((double) this.Position.Y - (double) this.Origin.Y), (int) ((double) (this.Width - 16) * (double) this.Scale.X), (int) ((double) (this.Height - 16) * (double) this.Scale.Y));
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      foreach (Sprite listSwitchParticle in this.ListSwitchParticles)
        listSwitchParticle.Draw(spriteBatch);
    }
  }
}
