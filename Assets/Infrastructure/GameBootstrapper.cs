using UnityEngine;

namespace Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    private Game _game;

    private void Awake()
    {
      _game = new Game(this);
      _game.stateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }
  }
}