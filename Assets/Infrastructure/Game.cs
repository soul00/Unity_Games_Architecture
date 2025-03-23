using Loading;
using Services.Input;

namespace Infrastructure
{
  public class Game
  {
    public static IInputService inputService;
    public GameStateMachine stateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
    {
      stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain );
    }
  }
}