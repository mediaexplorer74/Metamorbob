// Decompiled with JetBrains decompiler
// Type: HydroGene.PostProcessingEffect
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace HydroGene
{
  internal class PostProcessingEffect
  {
    protected GraphicsDevice graphicsDevice;
    protected SpriteBatch spriteBatch;

    public PostProcessingEffect(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
    {
      this.graphicsDevice = graphicsDevice;
      this.spriteBatch = spriteBatch;
    }

    public virtual Texture2D Apply(Texture2D input, GameTime gameTime) => input;
  }
}
