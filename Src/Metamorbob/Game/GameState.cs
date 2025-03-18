
// Type: GameManager.GameState





namespace GameManager
{
  public class GameState
  {
    protected Game1 mainGame;
    public GameState.SceneType CurrentSceneType;

    public Scene currentScene { get; set; }

    public GameState(Game1 mainGame) => this.mainGame = mainGame;

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
