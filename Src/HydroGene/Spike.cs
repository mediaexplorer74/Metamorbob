// Decompiled with JetBrains decompiler
// Type: HydroGene.Spike
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace HydroGene
{
  internal class Spike : Sprite
  {
    private Bob bob;

    public Spike(Bob b)
      : base(AssetManager.Spike)
    {
      this.bob = b;
    }

    public override void Update(GameTime gameTime)
    {
      if (Util.Overlaps((IActor)this, (IActor)this.bob))
      {
        //this.bob.IsDead = true;
      }

      base.Update(gameTime);
      if (this.Flip.Y)
        this.BoundingBox = new Rectangle((int) this.Position.X + 8, (int) this.Position.Y + 6, (int) ((double) (this.Width - 12) * (double) this.Scale.X), (int) ((double) (this.Height - 38) * (double) this.Scale.Y));
      else
        this.BoundingBox = new Rectangle((int) this.Position.X + 8, (int) this.Position.Y + 32, (int) ((double) (this.Width - 12) * (double) this.Scale.X), (int) ((double) (this.Height - 38) * (double) this.Scale.Y));
    }

    public override void Draw(SpriteBatch spriteBatch) => base.Draw(spriteBatch);
  }
}
