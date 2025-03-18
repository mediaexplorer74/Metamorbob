// Decompiled with JetBrains decompiler
// Type: HydroGene.Camera
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

#nullable disable
namespace HydroGene
{
  public static class Camera
  {
    private static Vector2 InitialPosition = Vector2.Zero;
    public static IActor FollowingActor = (IActor) null;
    public static float FollowLerp = 1f;
    public static FollowingType FollowingType = FollowingType.LOCKON;
    public static Rectangle VisibleArea = new Rectangle(0, 0, MainGame.WIDTH, MainGame.HEIGHT);
    private static Texture2D CameraDebugArea;
    private static Vector2 BaseShakePosition;
    private static Vector2 OldShakeIntensity;
    private static float currentShakeIntensity = 0.0f;
    private static float currentShakeDuration = 0.0f;
    private static Axe currentShakeAxe = Axe.HORIZONTAL_AND_VERTICAL;
    private static bool canShake = false;
    private static bool canFlash = false;
    private static float currentFlashDuration = 0.0f;
    private static Texture2D flashRectangle;
    private static Color flashColor = Color.White;
    private static float alphaFlash = 1f;
    private static bool canFade = false;
    private static float currentFadeDuration = 0.0f;
    private static Texture2D fadeRectangle;
    private static Color fadeColor = Color.White;
    private static float alphaFade = 0.0f;
    private static bool StayFaded = false;
    public static OnComplete OnCompleteFade;
    public static OnComplete OnCompleteFlash;
    private static bool DEBUG_IS_ON = false;
    private static bool AUTHORIZED_FORCED_ZOOM = false;

    public static Vector2 Position { get; set; } = Vector2.Zero;

    public static float Zoom { get; set; } = 1f;

    public static Vector2 Offset { get; set; } = Vector2.Zero;

    public static Vector2 Origin { get; set; } = Vector2.Zero;

    public static float Angle { get; set; } = 0.0f;

    public static Rectangle WorldBounds { get; set; }

    public static bool ShakeIsFinished { get; private set; } = false;

    public static bool FlashIsFinished { get; private set; } = false;

    public static bool FadeIsFinished { get; private set; } = false;

    public static Matrix Transformation
    {
      get
      {
        return Matrix.Multiply(Matrix.Multiply(Matrix.Multiply(
            Matrix.CreateTranslation(-Camera.Position.X, -Camera.Position.Y, 0.0f), 
            Matrix.CreateScale(Camera.Zoom, Camera.Zoom, 1f)), 
            Matrix.CreateRotationZ(MathHelper.ToRadians(Camera.Angle))),
            Matrix.CreateTranslation(Camera.Origin.X, Camera.Origin.Y, 0.0f));
      }
    }

    private static float Lerp(float start, float end, float pLerp)
    {
      return (float) Math.Round((Decimal) (start + pLerp * (end - start)));
    }

    public static void Follow(Sprite actor = null, FollowingType followType = FollowingType.LOCKON, float lerp = 1f)
    {
      Camera.FollowLerp = lerp;
      Camera.FollowingType = followType;
      if (actor == null)
        Camera.FollowingType = FollowingType.NOTHING;
      switch (Camera.FollowingType)
      {
        case FollowingType.NOTHING:
          Camera.Position = Vector2.Zero;
          break;
        case FollowingType.LOCKON:
          Camera.Position = new Vector2(Camera.Lerp(Camera.Position.X, actor.Position.X - ((float) (Camera.VisibleArea.Width / 2) - (float) ((double) actor.Width * (double) actor.Scale.X / 2.0)) + Camera.Offset.X, lerp), Camera.Lerp(Camera.Position.Y, actor.Position.Y - ((float) (Camera.VisibleArea.Height / 2) - (float) ((double) actor.Height * (double) actor.Scale.Y / 2.0)) + Camera.Offset.Y, lerp));
          break;
        case FollowingType.ALWAYS_ON_LEFT:
          Camera.Position = actor.Position;
          break;
        case FollowingType.ALWAYS_ON_RIGHT:
          Camera.Position = new Vector2(actor.Position.X - ((float) MainGame.WIDTH - (float) actor.Width * actor.Scale.X), actor.Position.Y - ((float) MainGame.HEIGHT - (float) actor.Height * actor.Scale.Y));
          break;
      }
      Rectangle worldBounds = Camera.WorldBounds;
      if ((double) Camera.Position.X <= (double) Camera.WorldBounds.X)
        Camera.Position = new Vector2((float) Camera.WorldBounds.X, Camera.Position.Y);
      if ((double) Camera.Position.X + (double) Camera.VisibleArea.Width >= (double) Camera.WorldBounds.Width)
        Camera.Position = new Vector2((float) (Camera.WorldBounds.Width - Camera.VisibleArea.Width), Camera.Position.Y);
      if ((double) Camera.Position.Y <= (double) Camera.WorldBounds.Y)
        Camera.Position = new Vector2(Camera.Position.X, (float) Camera.WorldBounds.Y);
      if ((double) Camera.Position.Y + (double) Camera.VisibleArea.Height >= (double) Camera.WorldBounds.Height)
        Camera.Position = new Vector2(Camera.Position.X, (float) (Camera.WorldBounds.Height - Camera.VisibleArea.Height));
      if (Camera.FollowingType != FollowingType.NOTHING && Camera.FollowingActor == null)
      {
        Camera.FollowingActor = (IActor) actor;
        Camera.InitialPosition = Camera.Position;
      }
      Camera.FollowingActor = (IActor) actor;
    }

    public static void Shake(float intensity, float duration, Axe axe = Axe.HORIZONTAL_AND_VERTICAL)
    {
      if (Camera.canShake || (double) Camera.currentShakeDuration != 0.0)
        return;
      Camera.ShakeIsFinished = false;
      Camera.currentShakeIntensity = intensity;
      Camera.currentShakeDuration = duration;
      Camera.BaseShakePosition = Camera.FollowingActor != null ? Camera.Position : Vector2.Zero;
      Camera.currentShakeAxe = axe;
      Camera.canShake = true;
    }

    public static void Flash(float duration, Color color)
    {
      if (Camera.canFlash || (double) Camera.currentFlashDuration != 0.0)
        return;
      Camera.FlashIsFinished = false;
      Camera.currentFlashDuration = duration;
      Camera.alphaFlash = 1f;
      Camera.flashColor = color;
      Camera.flashRectangle = new Texture2D(MainGame.Instance.GraphicsDevice, 1, 1);
      Camera.flashRectangle.SetData<Color>(new Color[1]
      {
        Camera.flashColor
      });
      Camera.canFlash = true;
    }

    public static void Fade(float duration, Color color, bool stayFaded = false)
    {
      Camera.StayFaded = stayFaded;
      if (Camera.canFade || (double) Camera.currentFadeDuration != 0.0)
        return;
      Camera.FadeIsFinished = false;
      Camera.currentFadeDuration = duration;
      Camera.alphaFade = 0.0f;
      Camera.fadeColor = color;
      Camera.fadeRectangle = new Texture2D(MainGame.Instance.GraphicsDevice, 1, 1);
      Camera.fadeRectangle.SetData<Color>(new Color[1]
      {
        Camera.fadeColor
      });
      Camera.canFade = true;
    }

    private static void Debug()
    {
      if (!Camera.DEBUG_IS_ON)
        return;
      Camera.CameraDebugArea = new Texture2D(MainGame.Instance.GraphicsDevice, 1, 1);
      Camera.CameraDebugArea.SetData<Color>(new Color[1]
      {
        Color.White
      });
    }

    public static void Unload()
    {
      Camera.Angle = 0.0f;
      Camera.Follow();
      Camera.StayFaded = false;
      Camera.FadeIsFinished = false;
      Camera.FlashIsFinished = false;
      Camera.ShakeIsFinished = false;
      Camera.canFade = false;
      Camera.currentFadeDuration = 0.0f;
      Camera.OnCompleteFade = Camera.OnCompleteFlash = (OnComplete) null;
      Camera.FadeIsFinished = true;
      Camera.alphaFade = 0.0f;
      Camera.Position = Vector2.Zero;
      Camera.Offset = Vector2.Zero;
      Camera.Origin = Vector2.Zero;
      Camera.Zoom = 1f;
    }

    public static void Update(GameTime gameTime)
    {
      if (Camera.DEBUG_IS_ON)
        Camera.Debug();
      TimeSpan elapsedGameTime;
      if (Camera.canShake && !MainGame.IS_PAUSED)
      {
        double currentShakeDuration = (double) Camera.currentShakeDuration;
        elapsedGameTime = gameTime.ElapsedGameTime;
        double totalSeconds = elapsedGameTime.TotalSeconds;
        Camera.currentShakeDuration = (float) (currentShakeDuration - totalSeconds);
        float num1 = Util.RandomFloat(-Camera.currentShakeIntensity, Camera.currentShakeIntensity);
        float num2 = Util.RandomFloat(-Camera.currentShakeIntensity, Camera.currentShakeIntensity);
        Vector2 oldShakeIntensity = Camera.OldShakeIntensity;
        if ((double) num1 < 0.0 && (double) Camera.OldShakeIntensity.X < 0.0)
          num1 = Math.Abs(num1);
        if ((double) num2 < 0.0 && (double) Camera.OldShakeIntensity.Y < 0.0)
          num2 = Math.Abs(num2);
        if ((double) num1 > 0.0 && (double) Camera.OldShakeIntensity.X > 0.0)
          num1 = -Math.Abs(num1);
        if ((double) num2 > 0.0 && (double) Camera.OldShakeIntensity.Y > 0.0)
          num2 = -Math.Abs(num2);
        switch (Camera.currentShakeAxe)
        {
          case Axe.HORIZONTAL:
            Camera.Position = new Vector2(Camera.Position.X + num1, Camera.Position.Y);
            break;
          case Axe.VERTICAL:
            Camera.Position = new Vector2(Camera.Position.X, Camera.Position.Y + num2);
            break;
          case Axe.HORIZONTAL_AND_VERTICAL:
            Camera.Position = new Vector2(Camera.Position.X + num1, Camera.Position.Y + num2);
            Camera.OldShakeIntensity = new Vector2(num1, num2);
            break;
        }
      }
      if ((double) Camera.currentShakeDuration < 0.0)
      {
        Camera.ShakeIsFinished = true;
        Camera.currentShakeDuration = 0.0f;
        Camera.canShake = false;
        if (Camera.FollowingActor == null)
          Camera.Position = Vector2.Zero;
      }
      if (Camera.canFlash)
      {
        double alphaFlash = (double) Camera.alphaFlash;
        elapsedGameTime = gameTime.ElapsedGameTime;
        double num = elapsedGameTime.TotalSeconds / (double) Camera.currentFlashDuration;
        Camera.alphaFlash = (float) (alphaFlash - num);
      }
      if ((double) Camera.alphaFlash < 0.0)
      {
        Camera.currentFlashDuration = 0.0f;
        Camera.canFlash = false;
        if (Camera.OnCompleteFlash != null)
          Camera.OnCompleteFlash();
        Camera.FlashIsFinished = true;
        Camera.alphaFlash = 1f;
      }
      if (Camera.canFade)
      {
        double alphaFade = (double) Camera.alphaFade;
        elapsedGameTime = gameTime.ElapsedGameTime;
        double num = elapsedGameTime.TotalSeconds / (double) Camera.currentFadeDuration;
        Camera.alphaFade = (float) (alphaFade + num);
      }
      if ((double) Camera.alphaFade > 1.0)
      {
        if (!Camera.StayFaded)
        {
          Camera.currentFadeDuration = 0.0f;
          Camera.canFade = false;
        }
        if (Camera.OnCompleteFade != null)
          Camera.OnCompleteFade();
        Camera.FadeIsFinished = true;
        Camera.alphaFade = 0.0f;
      }
      if ((double) Camera.Angle >= 360.0)
        Camera.Angle -= 360f;
      if ((double) Camera.Angle == 180.0)
        Camera.Origin = new Vector2((float) Camera.VisibleArea.Width, (float) Camera.VisibleArea.Height);
      if ((double) Camera.Angle == 0.0)
        Camera.Origin = Vector2.Zero;
      Camera.VisibleArea.X = (int) Camera.Position.X;
      Camera.VisibleArea.Y = (int) Camera.Position.Y;
      Camera.VisibleArea.Width = (int) ((double) MainGame.WIDTH / (double) Camera.Zoom);
      Camera.VisibleArea.Height = (int) ((double) MainGame.HEIGHT / (double) Camera.Zoom);
      if (!Camera.AUTHORIZED_FORCED_ZOOM)
        return;
      if (KBInput.JustPressed((Keys) 33))
        Camera.Zoom += 0.1f;
      if (!KBInput.JustPressed((Keys) 34))
        return;
      Camera.Zoom -= 0.1f;
    }

    public static void Draw()
    {
      if (Camera.DEBUG_IS_ON)
        MainGame.Instance.spriteBatch.Draw(Camera.CameraDebugArea, new Vector2((float) Camera.VisibleArea.X, (float) Camera.VisibleArea.Y), Color.Multiply(Color.White, 0.4f));
      if (Camera.canFlash)
        Primitive.DrawRectangle(Primitive.PrimitiveStyle.FILL, MainGame.Instance.spriteBatch, (float) (Camera.VisibleArea.X - 4 * Camera.VisibleArea.Width), (float) (Camera.VisibleArea.Y - 4 * Camera.VisibleArea.Height), 10 * Camera.VisibleArea.Width, 10 * Camera.VisibleArea.Height, Color.Multiply(Camera.flashColor, Camera.alphaFlash));
      if (!Camera.canFade)
        return;
      Primitive.DrawRectangle(Primitive.PrimitiveStyle.FILL, MainGame.Instance.spriteBatch, (float) (Camera.VisibleArea.X - 4 * Camera.VisibleArea.Width), (float) (Camera.VisibleArea.Y - 4 * Camera.VisibleArea.Height), 10 * Camera.VisibleArea.Width, 10 * Camera.VisibleArea.Height, Color.Multiply(Camera.fadeColor, Camera.alphaFade));
    }

    public static Vector2 ScreenToWorld(Vector2 pos)
    {
      return Vector2.Transform(pos, Matrix.Invert(Camera.Transformation));
    }

    public static Vector2 WorldToScreen(Vector2 pos)
    {
      return Vector2.Transform(pos, Camera.Transformation);
    }
  }
}
