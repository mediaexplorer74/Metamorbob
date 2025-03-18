// Decompiled with JetBrains decompiler
// Type: HydroGene.AssetManager
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

#nullable disable
namespace HydroGene
{
  internal class AssetManager
  {
    public static Texture2D Bob { get; private set; }

    public static Texture2D Bobicon { get; private set; }

    public static Texture2D Boss { get; private set; }

    public static Texture2D Btnexit { get; private set; }

    public static Texture2D Btnfullscreen { get; private set; }

    public static Texture2D Btnplay { get; private set; }

    public static Texture2D Bullet { get; private set; }

    public static Texture2D Canonicon { get; private set; }

    public static Texture2D Crate { get; private set; }

    public static Texture2D Flyicon { get; private set; }

    public static Texture2D Lb { get; private set; }

    public static Texture2D Monster1 { get; private set; }

    public static Texture2D Rb { get; private set; }

    public static Texture2D Rules { get; private set; }

    public static Texture2D Saw { get; private set; }

    public static Texture2D Spike { get; private set; }

    public static Texture2D Tileset { get; private set; }

    public static SpriteFont FontPixelmaster28 { get; private set; }

    public static Song Song_Boss { get; private set; }

    public static Song Song_Ingame { get; private set; }

    public static Song Song_Menu { get; private set; }

    public static Sound Sound_Bosslanding { get; private set; }

    public static Sound Sound_Boxexplode { get; private set; }

    public static Sound Sound_Enemydamage { get; private set; }

    public static Sound Sound_Enemydie { get; private set; }

    public static Sound Sound_Footstep { get; private set; }

    public static Sound Sound_Impact { get; private set; }

    public static Sound Sound_Jump { get; private set; }

    public static Sound Sound_Landing { get; private set; }

    public static Sound Sound_Shoot { get; private set; }

    public static Sound Sound_Switch { get; private set; }

    private static T Load<T>(string contentName) => MainGame.Instance.Content.Load<T>(contentName);

    public static void Load()
    {
      AssetManager.Bob = AssetManager.Load<Texture2D>("Bob");
      AssetManager.Bobicon = AssetManager.Load<Texture2D>("BobIcon");
      AssetManager.Boss = AssetManager.Load<Texture2D>("Boss");
      AssetManager.Btnexit = AssetManager.Load<Texture2D>("BtnExit");
      AssetManager.Btnfullscreen = AssetManager.Load<Texture2D>("BtnFullscreen");
      AssetManager.Btnplay = AssetManager.Load<Texture2D>("BtnPlay");
      AssetManager.Bullet = AssetManager.Load<Texture2D>("Bullet");
      AssetManager.Canonicon = AssetManager.Load<Texture2D>("CanonIcon");
      AssetManager.Crate = AssetManager.Load<Texture2D>("Crate");
      AssetManager.Flyicon = AssetManager.Load<Texture2D>("FlyIcon");
      AssetManager.Lb = AssetManager.Load<Texture2D>("LB");
      AssetManager.Monster1 = AssetManager.Load<Texture2D>("Monster1");
      AssetManager.Rb = AssetManager.Load<Texture2D>("RB");
      AssetManager.Rules = AssetManager.Load<Texture2D>("Rules");
      AssetManager.Saw = AssetManager.Load<Texture2D>("Saw");
      AssetManager.Spike = AssetManager.Load<Texture2D>("Spike");
      AssetManager.Tileset = AssetManager.Load<Texture2D>("Tileset");
      AssetManager.FontPixelmaster28 = AssetManager.Load<SpriteFont>("Fonts/PixelMaster28");
      AssetManager.Song_Boss = AssetManager.Load<Song>("Musics/Boss");
      AssetManager.Song_Ingame = AssetManager.Load<Song>("Musics/InGame");
      AssetManager.Song_Menu = AssetManager.Load<Song>("Musics/Menu");
      AssetManager.Sound_Bosslanding = new Sound(AssetManager.Load<SoundEffect>("Sounds/BossLanding"), MainGame.VOLUME_SFX);
      AssetManager.Sound_Boxexplode = new Sound(AssetManager.Load<SoundEffect>("Sounds/BoxExplode"), MainGame.VOLUME_SFX);
      AssetManager.Sound_Enemydamage = new Sound(AssetManager.Load<SoundEffect>("Sounds/EnemyDamage"), MainGame.VOLUME_SFX);
      AssetManager.Sound_Enemydie = new Sound(AssetManager.Load<SoundEffect>("Sounds/EnemyDie"), MainGame.VOLUME_SFX);
      AssetManager.Sound_Footstep = new Sound(AssetManager.Load<SoundEffect>("Sounds/Footstep"), MainGame.VOLUME_SFX);
      AssetManager.Sound_Impact = new Sound(AssetManager.Load<SoundEffect>("Sounds/Impact"), MainGame.VOLUME_SFX);
      AssetManager.Sound_Jump = new Sound(AssetManager.Load<SoundEffect>("Sounds/Jump"), MainGame.VOLUME_SFX);
      AssetManager.Sound_Landing = new Sound(AssetManager.Load<SoundEffect>("Sounds/Landing"), MainGame.VOLUME_SFX);
      AssetManager.Sound_Shoot = new Sound(AssetManager.Load<SoundEffect>("Sounds/Shoot"), MainGame.VOLUME_SFX);
      AssetManager.Sound_Switch = new Sound(AssetManager.Load<SoundEffect>("Sounds/Switch"), MainGame.VOLUME_SFX);
    }
  }
}
