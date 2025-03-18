
// Type: GameManager.IActor




using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameManager
{
  public interface IActor
  {
    Vector2 Position { get; }

    Rectangle BoundingBox { get; }

    void Update(GameTime gameTime);

    void Draw(SpriteBatch spriteBatch);

    void TouchedBy(IActor By);

    bool ToRemove { get; set; }

    bool IsOnScreen();

    bool IsActive { get; set; }

    bool IsVisible { get; set; }
  }
}
