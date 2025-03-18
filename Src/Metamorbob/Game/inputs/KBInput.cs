// Type: GameManager.KBInput

using Microsoft.Xna.Framework.Input;


namespace GameManager
{
  public static class KBInput
  {
    public static KeyboardState oldKBState;
    public static KeyboardState newKBState;

    public static Keys? GetLastKeyJustPressed()
    {
      return KBInput.JustPressed((Keys) (int) KBInput.newKBState.GetPressedKeys()[0]) 
                ? new Keys?((Keys) (int) KBInput.newKBState.GetPressedKeys()[0]) : new Keys?();
    }

    public static bool JustPressed(Keys key)
    {
      return KBInput.newKBState.IsKeyDown(key) && !KBInput.oldKBState.IsKeyDown(key);
    }

    public static bool JustReleased(Keys key)
    {
      return KBInput.newKBState.IsKeyUp(key) && !KBInput.oldKBState.IsKeyUp(key);
    }

    public static bool Pressed(Keys key) => KBInput.newKBState.IsKeyDown(key);

    public static bool Released(Keys key) => KBInput.newKBState.IsKeyUp(key);
  }
}
