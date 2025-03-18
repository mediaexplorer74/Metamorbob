// Type: GameManager.Game1
// "Main game class"

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GameManager
{
  public class Game1 : Game
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

    public static Game1 Instance { get; private set; }

    public Game1()
    {
      this.Window.Title = "Metamorbob " + Game1.GAME_VERSION;
      this.graphics = new GraphicsDeviceManager((Game) this);
      this.graphics.GraphicsProfile = (GraphicsProfile) 1;
      this.Content.RootDirectory = "Content";
      this.graphics.PreferredBackBufferWidth = 1280;
      this.graphics.PreferredBackBufferHeight = 720;
      Game1.WIDTH = this.graphics.PreferredBackBufferWidth;
      Game1.HEIGHT = this.graphics.PreferredBackBufferHeight;
      this.graphics.IsFullScreen = false;
      this.gameState = new GameState(this);
      Game1.Instance = this;
    }

    protected override void Initialize()
    {
      PresentationParameters presentationParameters 
                = this.graphics.GraphicsDevice.PresentationParameters;
           
            Game1.TargetWidth = 1280;
            Game1.TargetHeight = 720;

      this.render = new RenderTarget2D(this.graphics.GraphicsDevice, 
          Game1.TargetWidth, Game1.TargetHeight);

      this.Screen = new Screen(this, Game1.TargetWidth, Game1.TargetHeight, this.Scale);
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
        Game1.NB_DEATH[index] = 0;
      this.gameState.ChangeScene(GameState.SceneType.Menu);
    }

    protected override void UnloadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
      Game1.WIDTH = this.graphics.PreferredBackBufferWidth;
      Game1.HEIGHT = this.graphics.PreferredBackBufferHeight;
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

      Game1.WIDTH = this.Screen.Width;
      Game1.HEIGHT = this.Screen.Height;
      if (!Game1.CAN_PAUSE)
        Game1.IS_PAUSED = false;
      if (Game1.IS_CINEMATIC)
        Game1.CAN_PAUSE = false;
      if (Game1.IS_PAUSED)
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
