
// Type: GameManager.Primitive




using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace GameManager
{
  internal static class Primitive
  {
    public static Texture2D pixel { get; private set; }

    private static void CreatePixel(SpriteBatch spriteBatch)
    {
      Primitive.pixel = new Texture2D(((GraphicsResource) spriteBatch).GraphicsDevice, 1, 1, false, (SurfaceFormat) 0);
      Primitive.pixel.SetData<Color>(new Color[1]
      {
        Color.White
      });
    }

    public static Texture2D CreatePixel()
    {
      Primitive.pixel = new Texture2D(((GraphicsResource) Game1.Instance.spriteBatch).GraphicsDevice, 1, 1, false, (SurfaceFormat) 0);
      Primitive.pixel.SetData<Color>(new Color[1]
      {
        Color.White
      });
      return Primitive.pixel;
    }

    public static void DrawLine(
      SpriteBatch spriteBatch,
      Vector2 startPoint,
      Vector2 endPoint,
      Color color,
      int thickness = 1)
    {
      if (Primitive.pixel == null)
        Primitive.CreatePixel(spriteBatch);
      Vector2 vector2 = Vector2.Subtract(endPoint, startPoint);
      float num = (float) Math.Atan2((double) vector2.Y, (double) vector2.X);
      spriteBatch.Draw(Primitive.pixel, new Rectangle((int) startPoint.X, (int) startPoint.Y,
          (int) vector2.Length(), thickness), new Rectangle?(), color, num, new Vector2(0.0f, 0.0f), (SpriteEffects) 0, 0.0f);
    }

    private static void DrawLine(
      SpriteBatch spriteBatch,
      Vector2 position,
      Vector2 destination,
      Color color,
      float thickness = 1f,
      float angle = 0.0f)
    {
      if (Primitive.pixel == null)
        Primitive.CreatePixel(spriteBatch);
      spriteBatch.Draw(Primitive.pixel, position, new Rectangle?(), color, angle, Vector2.Zero, destination, (SpriteEffects) 0, 0.0f);
    }

    public static void DrawRectangle(
      Primitive.PrimitiveStyle style,
      SpriteBatch spriteBatch,
      float posX,
      float posY,
      int width,
      int height,
      Color color,
      float thickness = 0.0f,
      float angle = 0.0f)
    {
      if (Primitive.pixel == null)
        Primitive.CreatePixel(spriteBatch);
      if (style != Primitive.PrimitiveStyle.LINE)
      {
        if (style != Primitive.PrimitiveStyle.FILL)
          return;
        Primitive.DrawLine(spriteBatch, new Vector2(posX, posY), new Vector2((float) width, (float) height), color, thickness);
      }
      else
      {
        Primitive.DrawLine(spriteBatch, new Vector2(posX, posY), new Vector2((float) width + thickness, 1f + thickness), color, 1f, 0.0f);
        Primitive.DrawLine(spriteBatch, new Vector2(posX + (float) width, posY), new Vector2(1f + thickness, (float) height + thickness), color, 1f, 0.0f);
        Primitive.DrawLine(spriteBatch, new Vector2(posX, posY + (float) height), new Vector2((float) width + thickness, 1f + thickness), color, 1f, 0.0f);
        Primitive.DrawLine(spriteBatch, new Vector2(posX, posY), new Vector2(1f + thickness, (float) height + thickness), color, 1f, 0.0f);
      }
    }

    public static void DrawRectangle(
      Primitive.PrimitiveStyle style,
      SpriteBatch spriteBatch,
      Rectangle Rectangle,
      Color color,
      float thickness = 0.0f,
      float angle = 0.0f)
    {
      if (Primitive.pixel == null)
        Primitive.CreatePixel(spriteBatch);
      if (style != Primitive.PrimitiveStyle.LINE)
      {
        if (style != Primitive.PrimitiveStyle.FILL)
          return;
        Primitive.DrawLine(spriteBatch, new Vector2((float) Rectangle.X, (float) Rectangle.Y), new Vector2((float) Rectangle.Width, (float) Rectangle.Height), color, thickness);
      }
      else
      {
        Primitive.DrawLine(spriteBatch, new Vector2((float) Rectangle.X, (float) Rectangle.Y), new Vector2((float) Rectangle.Width + thickness, 1f + thickness), color, 1f, 0.0f);
        Primitive.DrawLine(spriteBatch, new Vector2((float) (Rectangle.X + Rectangle.Width), (float) Rectangle.Y), new Vector2(1f + thickness, (float) Rectangle.Height + thickness), color, 1f, 0.0f);
        Primitive.DrawLine(spriteBatch, new Vector2((float) Rectangle.X, (float) (Rectangle.Y + Rectangle.Height)), new Vector2((float) Rectangle.Width + thickness, 1f + thickness), color, 1f, 0.0f);
        Primitive.DrawLine(spriteBatch, new Vector2((float) Rectangle.X, (float) Rectangle.Y), new Vector2(1f + thickness, (float) Rectangle.Height + thickness), color, 1f, 0.0f);
      }
    }

    public static void DrawRectangle(
      Primitive.PrimitiveStyle style,
      SpriteBatch spriteBatch,
      Vector2 Position,
      Vector2 Size,
      Color color,
      float thickness = 0.0f,
      float angle = 0.0f)
    {
      if (Primitive.pixel == null)
        Primitive.CreatePixel(spriteBatch);
      if (style != Primitive.PrimitiveStyle.LINE)
      {
        if (style != Primitive.PrimitiveStyle.FILL)
          return;
        Primitive.DrawLine(spriteBatch, new Vector2(Position.X, Position.Y), new Vector2(Size.X, Size.Y), color, thickness);
      }
      else
      {
        Primitive.DrawLine(spriteBatch, new Vector2(Position.X, Position.Y), new Vector2(Size.X + thickness, 1f + thickness), color, 1f, 0.0f);
        Primitive.DrawLine(spriteBatch, new Vector2(Position.X + Size.X, Position.Y), new Vector2(1f + thickness, Size.Y + thickness), color, 1f, 0.0f);
        Primitive.DrawLine(spriteBatch, new Vector2(Position.X, Position.Y + Size.Y), new Vector2(Size.X + thickness, 1f + thickness), color, 1f, 0.0f);
        Primitive.DrawLine(spriteBatch, new Vector2(Position.X, Position.Y), new Vector2(1f + thickness, Size.Y + thickness), color, 1f, 0.0f);
      }
    }

    public static void DrawRectangle(
      Primitive.PrimitiveStyle style,
      SpriteBatch spriteBatch,
      Vector2 Position,
      int width,
      int height,
      Color color,
      float thickness = 0.0f,
      float angle = 0.0f)
    {
      if (Primitive.pixel == null)
        Primitive.CreatePixel(spriteBatch);
      if (style != Primitive.PrimitiveStyle.LINE)
      {
        if (style != Primitive.PrimitiveStyle.FILL)
          return;
        Primitive.DrawLine(spriteBatch, new Vector2(Position.X, Position.Y), new Vector2((float) width, (float) height), color, thickness);
      }
      else
      {
        Primitive.DrawLine(spriteBatch, new Vector2(Position.X, Position.Y), new Vector2((float) width + thickness, 1f + thickness), color, 1f, 0.0f);
        Primitive.DrawLine(spriteBatch, new Vector2(Position.X + (float) width, Position.Y), new Vector2(1f + thickness, (float) height + thickness), color, 1f, 0.0f);
        Primitive.DrawLine(spriteBatch, new Vector2(Position.X, Position.Y + (float) height), new Vector2((float) width + thickness, 1f + thickness), color, 1f, 0.0f);
        Primitive.DrawLine(spriteBatch, new Vector2(Position.X, Position.Y), new Vector2(1f + thickness, (float) height + thickness), color, 1f, 0.0f);
      }
    }

    public static void DrawCircle(
      Primitive.PrimitiveStyle style,
      SpriteBatch spriteBatch,
      Vector2 position,
      float pRadius,
      Color color,
      float ratio = 0.01f)
    {
      if (Primitive.pixel == null)
        Primitive.CreatePixel(spriteBatch);
      float x = position.X;
      float y = position.Y;
      float thickness = pRadius;
      for (float num = 0.0f; (double) num < 2.0 * Math.PI; num += ratio)
      {
        float posX = x + (float) Math.Cos((double) num) * thickness;
        float posY = y + (float) Math.Sin((double) num) * thickness;
        Primitive.DrawRectangle(Primitive.PrimitiveStyle.FILL, spriteBatch, posX, posY, 1, 1, color);
        if (style == Primitive.PrimitiveStyle.FILL)
          Primitive.DrawLine(spriteBatch, new Vector2(posX, posY), position, color, (int) thickness);
      }
    }

    public enum PrimitiveStyle : byte
    {
      LINE,
      FILL,
    }
  }
}
