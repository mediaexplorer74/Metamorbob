
// Type: GameManager.Button




using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GameManager
{
  public class Button(Texture2D Texture) : Sprite(Texture)
  {
    private MouseState oldMouseState;

    public bool isHover { get; private set; }

    public OnClick onClick { get; set; }

    public override void Update(GameTime gameTime)
    {
      MouseState state = Mouse.GetState();
      Point position = state.Position;
      Rectangle boundingBox = this.BoundingBox;
      if (boundingBox.Contains(position))
      {
        if (!this.isHover)
          this.isHover = true;
      }
      else
      {
        int num = this.isHover ? 1 : 0;
        this.isHover = false;
      }
      if (this.isHover && state.LeftButton == ButtonState.Pressed 
                && this.oldMouseState.LeftButton == null && this.onClick != null)
        this.onClick(this);
      this.oldMouseState = state;
      base.Update(gameTime);
    }
  }
}
