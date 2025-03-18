// Decompiled with JetBrains decompiler
// Type: HydroGene.Tweening
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

#nullable disable
namespace HydroGene
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
