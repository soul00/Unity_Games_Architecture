using CameraScripts;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
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
    private readonly IPersistentProgressService _progressService;

    public LoadingLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersistentProgressService progressService)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _progressService = progressService;
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
      InitGameWorld();
      InformProgressReaders();

      _stateMachine.Enter<GameLoopState>();
    }

    private void InformProgressReaders()
    {
      foreach (ISavedProgressReader progressReaders in _gameFactory.ProgressReaders)
      {
        progressReaders.LoadProgress(_progressService.Progress);
      }
    }

    private void InitGameWorld()
    {
      GameObject hero = _gameFactory.CreateHero(at: GameObject.FindWithTag(InitialPointTag));
      _gameFactory.CreateHud();

      CameraFollow(hero);
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