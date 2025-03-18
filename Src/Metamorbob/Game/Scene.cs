
// Type: GameManager.Scene




using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace GameManager
{
  public abstract class Scene
  {
    protected Game1 mainGame;
    public List<IActor> listActors;

    public Scene()
    {
      this.mainGame = Game1.Instance;
      this.listActors = new List<IActor>();
    }

    public void Clean() => this.listActors.RemoveAll((Predicate<IActor>) (item => item.ToRemove));

    public virtual void Load()
    {
    }

    public virtual void Unload()
    {
      Camera.Unload();
      Tweening.Unload();
    }

    public virtual void Update(GameTime gameTime)
    {
      foreach (IActor listActor in this.listActors)
      {
        if (!Game1.IS_PAUSED && listActor.IsActive)
          listActor.Update(gameTime);
      }
      Tweening.Update(gameTime);
    }

    public virtual void Draw(GameTime gameTime)
    {
      foreach (IActor listActor in this.listActors)
      {
        if (listActor.IsActive && listActor.IsVisible)
          listActor.Draw(this.mainGame.spriteBatch);
      }
    }
  }
}
