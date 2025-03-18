// Decompiled with JetBrains decompiler
// Type: HydroGene.MouseInput
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

#nullable disable
namespace HydroGene
{
  internal class MouseInput
  {
    public static MouseState newMouseState;
    public static MouseState oldMouseState;

    public static bool JustLeftClicked()
    {
      return newMouseState.LeftButton == ButtonState.Pressed 
                && oldMouseState.LeftButton != ButtonState.Pressed;
    }

    public static bool JustLeftReleased()
    {
      return newMouseState.LeftButton == null && oldMouseState.LeftButton > 0;
    }

    public static bool LeftClicked() => newMouseState.LeftButton == ButtonState.Pressed;

    public static bool JustRightClicked()
    {
      return (newMouseState.RightButton == ButtonState.Pressed 
                && oldMouseState.RightButton != ButtonState.Pressed);
    }

    public static bool JustRightReleased()
    {
      return (newMouseState.RightButton == null && 
                oldMouseState.RightButton > ButtonState.Released);
    }

    public static bool RightClicked()
    {
      return (MouseInput.newMouseState.RightButton == ButtonState.Pressed);
    }

    public static Vector2 GetPosition()
    {
      MouseState state = Mouse.GetState();
      Point position = state.Position;
      return position.ToVector2();
    }
  }
}
