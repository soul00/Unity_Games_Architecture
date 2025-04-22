using CameraScripts;
using Infrastructure.Factory;
using Loading;
using UnityEngine;

namespace Infrastructure.States
{
  public class LoadingLevelState : IPayloadedState<string>
  {
    private const string InitialPointTag = "InitialPoint";

    
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;

    public LoadingLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
    }

    public void Enter(string sceneName)
    {
      _loadingCurtain.Show();
      _gameFactory.Cleanup();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() => 
      _loadingCurtain.Hide();

    private void OnLoaded()
    {
      GameObject hero = _gameFactory.CreateHero(at: GameObject.FindWithTag(InitialPointTag));
      _gameFactory.CreateHud();
      
      CameraFollow(hero);
      
      _stateMachine.Enter<GameLoopState>();
    }

    private void CameraFollow(GameObject hero)
    {
      if (Camera.main != null)
        Camera.main
          .GetComponent<FollowCamera>()
          .Follow(hero);
    }
  }
}