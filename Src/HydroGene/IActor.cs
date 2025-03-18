// Decompiled with JetBrains decompiler
// Type: HydroGene.IActor
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace HydroGene
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
