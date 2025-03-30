using Infrastructure.Services;
using Infrastructure.States;
using Loading;

namespace Infrastructure
{
  public class Game
  {
    public GameStateMachine stateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
    {
      stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container);
    }
  }
}