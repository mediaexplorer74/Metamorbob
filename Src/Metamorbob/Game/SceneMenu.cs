
// Type: GameManager.SceneMenu




using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace GameManager
{
  internal class SceneMenu : Scene
  {
    private Sprite BtnPlay;
    private Sprite BtnFullscreen;
    private Sprite BtnExit;
    private Sprite Rules;
    private Text TextTitle;
    private Text TextPlay;
    private Text TextFullscreen;
    private Text TextExit;
    private Text TextGamepad;
    private byte CursorPosition = 1;
    private bool CanMove = true;

    public override void Load()
    {
      Camera.Flash(1f, Color.Black);
      MediaPlayer.Play(AssetManager.Song_Menu);
      MediaPlayer.IsRepeating = true;
      this.TextTitle = new Text(AssetManager.FontPixelmaster28, "METAMORBOB", new Vector2(Camera.Position.X, 10f), Color.Black);
      this.TextTitle.Align = Util.Alignement.CENTER_X;
      this.TextTitle.Scale = new Vector2(2f);
      this.TextTitle.EffectBold = true;
      this.listActors.Add((IActor) this.TextTitle);
      this.BtnPlay = new Sprite(AssetManager.Btnplay);
      this.BtnPlay.Origin = new Vector2((float) (this.BtnPlay.Width / 2), (float) (this.BtnPlay.Height / 2));
      this.BtnPlay.Scale = new Vector2(2f);
      this.BtnPlay.Position = new Vector2(Camera.Position.X + (float) Camera.VisibleArea.Width * 0.2f, Camera.Position.Y + (float) Camera.VisibleArea.Height * 0.84f);
      this.listActors.Add((IActor) this.BtnPlay);
      this.BtnFullscreen = new Sprite(AssetManager.Btnfullscreen);
      this.BtnFullscreen.Origin = new Vector2((float) (this.BtnFullscreen.Width / 2), (float) (this.BtnFullscreen.Height / 2));
      this.BtnFullscreen.Scale = new Vector2(2f);
      this.BtnFullscreen.Position = new Vector2(Camera.Position.X + (float) Camera.VisibleArea.Width * 0.5f, Camera.Position.Y + (float) Camera.VisibleArea.Height * 0.84f);
      this.listActors.Add((IActor) this.BtnFullscreen);
      this.BtnExit = new Sprite(AssetManager.Btnexit);
      this.BtnExit.Origin = new Vector2((float) (this.BtnExit.Width / 2), (float) (this.BtnExit.Height / 2));
      this.BtnExit.Scale = new Vector2(2f);
      this.BtnExit.Position = new Vector2(Camera.Position.X + (float) Camera.VisibleArea.Width * 0.8f, Camera.Position.Y + (float) Camera.VisibleArea.Height * 0.84f);
      this.listActors.Add((IActor) this.BtnExit);
      this.TextPlay = new Text(AssetManager.FontPixelmaster28, "PLAY", new Vector2(Camera.Position.X, 10f), Color.White);
      this.TextPlay.Position = new Vector2(this.BtnPlay.Position.X - 50f, this.BtnPlay.Position.Y - 30f);
      this.TextPlay.Scale = new Vector2(1.8f);
      this.listActors.Add((IActor) this.TextPlay);
      this.TextFullscreen = new Text(AssetManager.FontPixelmaster28, "  On/Off\nFULLSCREEN", new Vector2(Camera.Position.X, 10f), Color.White);
      this.TextFullscreen.Position = new Vector2(this.BtnFullscreen.Position.X - 100f, this.BtnFullscreen.Position.Y - 60f);
      this.TextFullscreen.Scale = new Vector2(1.5f);
      this.listActors.Add((IActor) this.TextFullscreen);
      this.TextExit = new Text(AssetManager.FontPixelmaster28, "EXIT", new Vector2(Camera.Position.X, 10f), Color.White);
      this.TextExit.Position = new Vector2(this.BtnExit.Position.X - 50f, this.BtnExit.Position.Y - 30f);
      this.TextExit.Scale = new Vector2(1.8f);
      this.listActors.Add((IActor) this.TextExit);
      this.TextGamepad = new Text(AssetManager.FontPixelmaster28, "GAMEPAD STRONGLY RECOMMANDED (XBOX 360) / GAMEPAD FORTEMENT CONSEILLER (XBOX 360)", new Vector2(Camera.Position.X, 10f), Color.DarkViolet);
      this.TextGamepad.Position = new Vector2(0.0f, 70f);
      this.TextGamepad.Align = Util.Alignement.CENTER_X;
      this.TextGamepad.Scale = new Vector2(0.9f);
      this.listActors.Add((IActor) this.TextGamepad);
      this.Rules = new Sprite(AssetManager.Rules);
      this.Rules.Position = new Vector2(0.0f, Camera.Position.Y + (float) Camera.VisibleArea.Height * 0.22f);
      this.listActors.Add((IActor) this.Rules);
      Camera.OnCompleteFade = (OnComplete) (() =>
      {
        switch (this.CursorPosition)
        {
          case 1:
            MediaPlayer.Stop();
            this.mainGame.gameState.ChangeScene(GameState.SceneType.Game);
            break;
          case 3:
            Game1.Instance.Exit();
            break;
        }
      });
      base.Load();
    }

    public override void Update(GameTime gameTime)
    {
      this.TextPlay.Alpha = this.TextFullscreen.Alpha = this.TextExit.Alpha = 0.4f;
      this.BtnPlay.Alpha = this.BtnFullscreen.Alpha = this.BtnExit.Alpha = 0.4f;
      this.TextGamepad.IsActive = !GamePadInput.capabilities.IsConnected;
      if (!this.CanMove)
        MediaPlayer.Volume -= 0.005f;
      switch (this.CursorPosition)
      {
        case 1:
          this.TextPlay.Alpha = this.BtnPlay.Alpha = 1f;
          break;
        case 2:
          this.TextFullscreen.Alpha = this.BtnFullscreen.Alpha = 1f;
          break;
        case 3:
          this.TextExit.Alpha = this.BtnExit.Alpha = 1f;
          break;
      }
      if (this.CanMove)
      {
        if (this.CursorPosition < (byte) 3 && (KBInput.JustPressed((Keys) 39) || GamePadInput.JustPressed((Buttons) 8) || GamePadInput.JustPressed((Buttons) 1073741824)))
        {
          AssetManager.Sound_Switch.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
          ++this.CursorPosition;
        }
        if (this.CursorPosition > (byte) 1 && (KBInput.JustPressed((Keys) 37) || GamePadInput.JustPressed((Buttons) 4) || GamePadInput.JustPressed((Buttons) 2097152)))
        {
          AssetManager.Sound_Switch.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
          --this.CursorPosition;
        }
        if (KBInput.JustPressed((Keys) 13) || KBInput.JustPressed((Keys) 32) || KBInput.JustPressed((Keys) 88) || GamePadInput.JustPressed((Buttons) 4096) || GamePadInput.JustPressed((Buttons) 16))
        {
          AssetManager.Sound_Shoot.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
          switch (this.CursorPosition)
          {
            case 1:
              Camera.Fade(1f, Color.Black);
              this.CanMove = false;
              break;
            case 2:
              if (!Game1.Instance.Screen.IsFullscreen)
              {
                Game1.Instance.Screen.EnableFullscreen();
                break;
              }
              Game1.Instance.Screen.DisableFullscreen();
              break;
            case 3:
              Camera.Fade(1f, Color.Black);
              this.CanMove = false;
              break;
          }
        }
      }
      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      Primitive.DrawRectangle(Primitive.PrimitiveStyle.FILL, this.mainGame.spriteBatch, Camera.VisibleArea, Color.White, 2f);
      Primitive.DrawLine(this.mainGame.spriteBatch, Vector2.Add(Camera.Position, new Vector2(0.0f, (float) Camera.VisibleArea.Height * 0.2f)), new Vector2(Camera.Position.X + (float) Camera.VisibleArea.Width, (float) Camera.VisibleArea.Height * 0.2f), Color.Black, 2);
      Primitive.DrawLine(this.mainGame.spriteBatch, Vector2.Add(Camera.Position, new Vector2(0.0f, (float) Camera.VisibleArea.Height * 0.72f)), new Vector2(Camera.Position.X + (float) Camera.VisibleArea.Width, (float) Camera.VisibleArea.Height * 0.72f), Color.Black, 2);
      base.Draw(gameTime);
    }
  }
}
