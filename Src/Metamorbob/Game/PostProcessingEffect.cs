
// Type: GameManager.PostProcessingEffect




using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameManager
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
