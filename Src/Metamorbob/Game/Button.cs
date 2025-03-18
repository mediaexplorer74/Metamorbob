// Type: GameManager.Button


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;



namespace GameManager
{
  public class Button : Sprite//Button(Texture2D Texture) : Sprite(Texture)
  {
    public Button(Texture2D Texture)
        : base(Texture)
    {
    }

    private MouseState oldMouseState;
    private TouchCollection oldTouchState;

    public bool isHover { get; private set; }

    public OnClick onClick { get; set; }

    public override void Update(GameTime gameTime)
    {
      MouseState state = Mouse.GetState();
      TouchCollection state1 = TouchPanel.GetState();
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

      if (/*this.isHover &&*/state1.Count == 1
                    && this.oldTouchState.Count == null)
            this.onClick(this);

        this.oldMouseState = state;
      this.oldTouchState = state1;

      base.Update(gameTime);
    }
  }
}
