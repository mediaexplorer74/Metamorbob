// Decompiled with JetBrains decompiler
// Type: HydroGene.Scene
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

#nullable disable
namespace HydroGene
{
  public abstract class Scene
  {
    protected MainGame mainGame;
    public List<IActor> listActors;

    public Scene()
    {
      this.mainGame = MainGame.Instance;
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
        if (!MainGame.IS_PAUSED && listActor.IsActive)
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
