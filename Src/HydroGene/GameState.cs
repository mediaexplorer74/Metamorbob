// Decompiled with JetBrains decompiler
// Type: HydroGene.GameState
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

#nullable disable
namespace HydroGene
{
  public class GameState
  {
    protected MainGame mainGame;
    public GameState.SceneType CurrentSceneType;

    public Scene currentScene { get; set; }

    public GameState(MainGame mainGame) => this.mainGame = mainGame;

    public void ChangeScene(GameState.SceneType sceneType)
    {
      if (this.currentScene != null)
      {
        this.currentScene.Unload();
        this.currentScene = (Scene) null;
      }
      switch (sceneType)
      {
        case GameState.SceneType.End:
          this.currentScene = (Scene) new SceneEnd();
          break;
        case GameState.SceneType.Menu:
          this.currentScene = (Scene) new SceneMenu();
          break;
        case GameState.SceneType.Game:
          this.currentScene = (Scene) new SceneGame();
          break;
      }
      this.CurrentSceneType = sceneType;
      this.currentScene.Load();
    }

    public enum SceneType : byte
    {
      End,
      Menu,
      Game,
    }
  }
}
