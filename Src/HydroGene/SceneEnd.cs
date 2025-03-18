// Decompiled with JetBrains decompiler
// Type: HydroGene.SceneEnd
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

#nullable disable
namespace HydroGene
{
  internal class SceneEnd : Scene
  {
    private Text[] TextNbDeath = new Text[5];
    private Text TextScore;
    private Text TextTotalDeath;
    private Text TextPressKey;
    private bool CanValidate = true;

    public override void Load()
    {
      Camera.Flash(1f, Color.Black);
      MediaPlayer.Stop();
      MediaPlayer.Play(AssetManager.Song_Menu);
      MediaPlayer.IsRepeating = true;
      this.TextScore = new Text(AssetManager.FontPixelmaster28, "FINAL SCORE", new Vector2(0.0f, (float) Camera.VisibleArea.Height * 0.1f), Color.DarkOrange);
      this.TextScore.Align = Util.Alignement.CENTER_X;
      this.TextScore.Scale = new Vector2(1.6f);
      this.TextScore.EffectBold = true;
      this.listActors.Add((IActor) this.TextScore);
      int num = 0;
      for (int index = 0; index < 5; ++index)
      {
        this.TextNbDeath[index] = new Text(AssetManager.FontPixelmaster28, "Level " + (object) (index + 1) + " - " + (object) MainGame.NB_DEATH[index] + " Deaths", new Vector2(0.0f, Camera.Position.Y + (float) Camera.VisibleArea.Height * 0.3f + (float) (40 * index)), Color.Black);
        this.TextNbDeath[index].Align = Util.Alignement.CENTER_X;
        this.TextNbDeath[index].Scale = new Vector2(1.4f);
        this.listActors.Add((IActor) this.TextNbDeath[index]);
        num += MainGame.NB_DEATH[index];
      }
      this.TextTotalDeath = new Text(AssetManager.FontPixelmaster28, "Total Deaths : " + (object) num, new Vector2(0.0f, (float) Camera.VisibleArea.Height * 0.74f), Color.Red);
      this.TextTotalDeath.Align = Util.Alignement.CENTER_X;
      this.TextTotalDeath.Scale = new Vector2(2f);
      this.TextTotalDeath.EffectBold = true;
      this.listActors.Add((IActor) this.TextTotalDeath);
      this.TextPressKey = new Text(AssetManager.FontPixelmaster28, "Press [ENTER / SPACE] or [START / A] To Return to the menu", new Vector2(0.0f, (float) Camera.VisibleArea.Height * 0.86f), Color.DarkOrange);
      this.TextPressKey.Align = Util.Alignement.CENTER_X;
      this.TextPressKey.Scale = new Vector2(1.2f);
      this.TextPressKey.EffectBold = true;
      this.listActors.Add((IActor) this.TextPressKey);
      Camera.OnCompleteFade = (OnComplete) (() =>
      {
        MainGame.CURRENT_LEVEL = (byte) 1;
        for (int index = 0; index < 5; ++index)
          MainGame.NB_DEATH[index] = 0;
        this.mainGame.gameState.ChangeScene(GameState.SceneType.Menu);
      });
      base.Load();
    }

    public override void Update(GameTime gameTime)
    {
      if (this.CanValidate && (KBInput.JustPressed((Keys) 32) || KBInput.JustPressed((Keys) 13) || GamePadInput.JustPressed((Buttons) 4096) || GamePadInput.JustPressed((Buttons) 16)))
      {
        AssetManager.Sound_Switch.SoundEffect.Play(MainGame.VOLUME_SFX, 0.0f, 0.0f);
        Camera.Fade(1f, Color.Black);
        this.CanValidate = false;
      }
      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      Primitive.DrawRectangle(Primitive.PrimitiveStyle.FILL, this.mainGame.spriteBatch, Camera.VisibleArea, Color.White, 2f);
      base.Draw(gameTime);
    }
  }
}
