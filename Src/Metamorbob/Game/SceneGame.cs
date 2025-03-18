
// Type: GameManager.SceneGame




using GameManager.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;


namespace GameManager
{
  internal class SceneGame : Scene
  {
    private Bob bob;
    private Boss boss;
    private TileMap Level;
    private List<Sprite> ListLight = new List<Sprite>();
    public Flip Flip = new Flip();

    private void LoadEntity()
    {
      foreach (Vector2 vector2 in this.Level.CheckEntity(27, this.Level.LayerEntity))
      {
        Spike spike = new Spike(this.bob);
        spike.Position = vector2;
        this.listActors.Add((IActor) spike);
      }
      foreach (Vector2 vector2 in this.Level.CheckEntity(28, this.Level.LayerEntity))
      {
        Spike spike = new Spike(this.bob);
        spike.Position = vector2;
        spike.Flip.Y = true;
        this.listActors.Add((IActor) spike);
      }
      foreach (Vector2 vector2 in this.Level.CheckEntity(38, this.Level.LayerEntity))
      {
        Saw saw = new Saw(this.bob);
        saw.Position = Vector2.Add(vector2, saw.Origin);
        this.listActors.Add((IActor) saw);
      }
      foreach (Vector2 vector2 in this.Level.CheckEntity(39, this.Level.LayerEntity))
      {
        Saw saw = new Saw(this.bob, true);
        saw.Position = Vector2.Add(vector2, saw.Origin);
        this.listActors.Add((IActor) saw);
      }
      foreach (Vector2 vector2 in this.Level.CheckEntity(46, this.Level.LayerEntity))
      {
        Crate crate = new Crate(this.bob);
        crate.Position = Vector2.Add(vector2, crate.Origin);
        this.listActors.Add((IActor) crate);
      }
      foreach (Vector2 vector2 in this.Level.CheckEntity(40, this.Level.LayerEntity))
        this.listActors.Add((IActor) new AngryFace(Vector2.Add(vector2, new Vector2(0.0f, 16f)), 
            this.bob));
      foreach (Vector2 vector2 in this.Level.CheckEntity(41, this.Level.LayerEntity))
      {
        this.boss = new Boss(Vector2.Add(vector2, new Vector2(0.0f, -16f)), this.bob);
        this.listActors.Add((IActor) this.boss);
      }
    }

    public override void Load()
    {
      Camera.Flash(0.5f, Color.Black);
      if (Game1.CURRENT_LEVEL == (byte) 5)
      {
        MediaPlayer.Stop();
        MediaPlayer.Play(AssetManager.Song_Boss);
      }
      else if (MediaPlayer.State != MediaState.Playing)
      {
        MediaPlayer.Play(AssetManager.Song_Ingame);
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Volume = Game1.VOLUME_MUSIC;
      }
      this.Level = new TileMap("Content/Levels/Level" + (object) Game1.CURRENT_LEVEL + ".tmx", AssetManager.Tileset);
      this.Level.LayerEntity = 2;
      this.Level.SetSolidLayer(0);
      this.listActors.Add((IActor) this.Level);
      this.bob = new Bob();
      this.bob.tileMap = this.Level;
      foreach (Vector2 vector2 in this.Level.CheckEntity(37, this.Level.LayerEntity))
        this.bob.Position = vector2;
      this.bob.InitialPosition = this.bob.Position;
      this.listActors.Add((IActor) this.bob);
      this.LoadEntity();
      int num = 64;
      for (int index1 = 0; index1 < this.Level.MapWidth / num; ++index1)
      {
        for (int index2 = 0; index2 < this.Level.MapHeight / num; ++index2)
        {
          Sprite sprite = new Sprite(Primitive.CreatePixel());
          sprite.Scale = new Vector2((float) (byte) num);
          sprite.Position = new Vector2((float) (index1 * num), (float) (index2 * num));
          sprite.Color = Color.Black;
          sprite.Alpha = 0.7f;
          sprite.Name = "LIGHT";
          this.listActors.Add((IActor) sprite);
          this.ListLight.Add(sprite);
        }
      }
      Camera.WorldBounds = new Rectangle(0, 0, this.Level.MapWidth, this.Level.MapHeight);
      Camera.OnCompleteFade = (OnComplete) (() =>
      {
        if (Game1.CURRENT_LEVEL == (byte) 5)
        {
          this.mainGame.gameState.ChangeScene(GameState.SceneType.End);
        }
        else
        {
          ++Game1.CURRENT_LEVEL;
          this.mainGame.gameState.ChangeScene(GameState.SceneType.Game);
        }
      });
      base.Load();
    }

    public override void Update(GameTime gameTime)
    {
      Camera.Follow((Sprite) this.bob, lerp: 0.075f);
      if ((double) this.bob.Position.X <= 0.0)
        this.bob.Position = new Vector2(1f, this.bob.Position.Y);
      else if ((double) this.bob.Position.X + 2.0 > (double) this.Level.MapWidth)
      {
        this.bob.Velocity.Y = 0.0f;
        this.bob.Position = new Vector2((float) (this.Level.MapWidth + this.bob.Width), this.bob.Position.Y);
        Camera.Fade(0.5f, Color.Black);
      }
      foreach (Sprite actor1 in this.ListLight)
      {
        if (actor1.IsOnScreen() && Util.DistanceBetween((IActor) actor1, (IActor) this.bob) <= 900.0)
        {
          if (Util.DistanceBetween((IActor) actor1, (IActor) this.bob) <= 680.0)
            actor1.Alpha = 0.05f;
          else if (Util.DistanceBetween((IActor) actor1, (IActor) this.bob) > 680.0 && Util.DistanceBetween((IActor) actor1, (IActor) this.bob) <= 900.0)
            actor1.Alpha = 0.15f;
        }
        else
          actor1.Alpha = 0.2f;
      }
      if (Game1.CURRENT_LEVEL == (byte) 5 && (double) this.boss.Alpha == 0.0)
        Camera.Fade(1f, Color.Black);
      this.Clean();
      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      Primitive.DrawRectangle(Primitive.PrimitiveStyle.FILL, this.mainGame.spriteBatch, Camera.VisibleArea, Color.White, 2f);
      base.Draw(gameTime);
      this.bob.DrawHUD(this.mainGame.spriteBatch);
    }
  }
}
