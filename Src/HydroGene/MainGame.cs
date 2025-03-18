// Decompiled with JetBrains decompiler
// Type: HydroGene.MainGame
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#nullable disable
namespace HydroGene
{
  public class MainGame : Game
  {
    public GraphicsDeviceManager graphics;
    public SpriteBatch spriteBatch;
    public GameState gameState;
    private RenderTarget2D render;
    public static int TargetWidth;
    public static int TargetHeight;
    public static string LANGUAGE = "EN";
    public static float VOLUME_MUSIC = 1f;
    public static float VOLUME_SFX = 0.6f;
    public static bool CAN_PAUSE = true;
    public static bool IS_PAUSED = false;
    public static bool IS_CINEMATIC = false;
    public static byte CURRENT_LEVEL = 1;
    public const int DEFAULT_WIDTH = 1280;
    public const int DEFAULT_HEIGHT = 720;
    public float Scale = 1f;
    public static int WIDTH;
    public static int HEIGHT;
    public static string GAME_VERSION = "1.0";
    public static int[] NB_DEATH = new int[5];
    public Screen Screen;

    public static MainGame Instance { get; private set; }

    public MainGame()
    {
      this.Window.Title = "Metamorbob " + MainGame.GAME_VERSION;
      this.graphics = new GraphicsDeviceManager((Game) this);
      this.graphics.GraphicsProfile = (GraphicsProfile) 1;
      this.Content.RootDirectory = "Content";
      this.graphics.PreferredBackBufferWidth = 1280;
      this.graphics.PreferredBackBufferHeight = 720;
      MainGame.WIDTH = this.graphics.PreferredBackBufferWidth;
      MainGame.HEIGHT = this.graphics.PreferredBackBufferHeight;
      this.graphics.IsFullScreen = false;
      this.gameState = new GameState(this);
      MainGame.Instance = this;
    }

    protected override void Initialize()
    {
      PresentationParameters presentationParameters = this.graphics.GraphicsDevice.PresentationParameters;
           
            MainGame.TargetWidth = 1280;
            MainGame.TargetHeight = 720;

      this.render = new RenderTarget2D(this.graphics.GraphicsDevice, MainGame.TargetWidth, MainGame.TargetHeight);
      this.Screen = new Screen(this, MainGame.TargetWidth, MainGame.TargetHeight, this.Scale);
      //this.Screen.EnableFullscreen();
      base.Initialize();
    }

    protected override void LoadContent()
    {
      this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
      AssetManager.Load();
      this.Screen.Initialize();
      GamePadInput.TimerVibration.OnComplete = (OnComplete) (() => GamePadInput.StopVibration());
      for (int index = 0; index < 5; ++index)
        MainGame.NB_DEATH[index] = 0;
      this.gameState.ChangeScene(GameState.SceneType.Menu);
    }

    protected override void UnloadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
      MainGame.WIDTH = this.graphics.PreferredBackBufferWidth;
      MainGame.HEIGHT = this.graphics.PreferredBackBufferHeight;
      GamePadInput.capabilities = GamePad.GetCapabilities((PlayerIndex) 0);
      KBInput.newKBState = Keyboard.GetState();
      GamePadInput.newGPState = GamePad.GetState((PlayerIndex) 0, (GamePadDeadZone) 1);
      MouseInput.newMouseState = Mouse.GetState();

      if ((KBInput.Pressed((Keys) 164) || KBInput.Pressed((Keys) 165)) 
                && KBInput.JustPressed((Keys) 13))
      {
        if (!this.Screen.IsFullscreen)
          this.Screen.EnableFullscreen();
        else
          this.Screen.DisableFullscreen();
      }

      MainGame.WIDTH = this.Screen.Width;
      MainGame.HEIGHT = this.Screen.Height;
      if (!MainGame.CAN_PAUSE)
        MainGame.IS_PAUSED = false;
      if (MainGame.IS_CINEMATIC)
        MainGame.CAN_PAUSE = false;
      if (MainGame.IS_PAUSED)
      {
        Camera.Angle = 0.0f;
        Camera.Zoom = 1f;
      }
      this.gameState.currentScene?.Update(gameTime);
      Camera.Update(gameTime);
      GamePadInput.oldGPState = GamePadInput.newGPState;
      KBInput.oldKBState = KBInput.newKBState;
      MouseInput.oldMouseState = MouseInput.newMouseState;
      if ((double) GamePadInput.TimerVibration.TotalTimer != 0.0)
        GamePadInput.TimerVibration.Update(gameTime);
      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      if (((GraphicsResource) this.Screen.RenderTarget).IsDisposed)
        this.Screen.Initialize();
      this.GraphicsDevice.SetRenderTarget(this.Screen.RenderTarget);
      this.GraphicsDevice.Clear(this.Screen.ClearColor);
      this.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend,
          SamplerState.PointClamp, (DepthStencilState) null, (RasterizerState) null, 
          (Effect) null, new Matrix?(Camera.Transformation));

      this.gameState.currentScene?.Draw(gameTime);
      Camera.Draw();
      if (!this.IsActive)
        Primitive.DrawRectangle(Primitive.PrimitiveStyle.FILL, this.spriteBatch, 
            Camera.Position.X, Camera.Position.Y, Camera.VisibleArea.Width + 2,
            Camera.VisibleArea.Height + 2, Color.Multiply(Color.Black, 0.4f));
      this.spriteBatch.End();
      this.GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
      this.GraphicsDevice.Clear(Color.Black);
      this.Screen.Render();
      this.spriteBatch.Begin((SpriteSortMode) 0, (BlendState) null, 
          (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, 
          (Effect) null, new Matrix?());
      this.spriteBatch.End();
      base.Draw(gameTime);
    }
  }
}
