using Infrastructure.Services.PersistentProgress;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Services.Input;
using UnityEngine;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.States
{
  public class BootstrapState : IState
  {
    private string Initial = "Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;
      
      RegisterServices();
    }

    public void Enter()
    {
      _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);   
    }

    public void Exit()
    {
      
    }

    private void EnterLoadLevel()
    {
      _stateMachine.Enter<LoadingProgressState>();
    }

    private void RegisterServices()
    {
      _services.RegisterSingle<IInputService>(InputService());
      _services.RegisterSingle<IAssets>(new AssetProvider());
      _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
      _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>()));
      _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(),_services.Single<IGameFactory>()));
    }

    private static IInputService InputService()
    {
      if (Application.isEditor)
        return new StandaloneInputService();
      else
        return new MobileInput();
    }
  }
}