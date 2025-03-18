
// Type: GameManager.Tweening




using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace GameManager
{
  internal static class Tweening
  {
    public static List<Tween> list_tweens = new List<Tween>();

    public static void Update(GameTime gameTime)
    {
      foreach (Tween listTween in Tweening.list_tweens)
        listTween.Update(gameTime);
    }

    public static void Unload()
    {
      foreach (Tween listTween in Tweening.list_tweens)
        listTween.ToRemove = true;
      Tweening.list_tweens.RemoveAll((Predicate<Tween>) (item => item.ToRemove = true));
    }
  }
}
