// Decompiled with JetBrains decompiler
// Type: HydroGene.KBInput
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework.Input;

#nullable disable
namespace HydroGene
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
