using Infrastructure.States;
using Loading;
using UnityEngine;

namespace Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    public LoadingCurtain CurtainPrefab;
    
    private Game _game;
    
    private void Awake()
    {
      _game = new Game(this, Instantiate(CurtainPrefab));
      _game.stateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }
  }
}