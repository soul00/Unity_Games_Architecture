using CameraScripts;
using Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
  public class LoadingLevelState : IPayloadedState<string>
  {
    private const string InitialPointTag = "InitialPoint";
    private const string PlayerPath = "Player/Player";
    private const string HudPath = "HUD/HUD";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;

    public LoadingLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      this._loadingCurtain = loadingCurtain;
    }

    public void Enter(string sceneName)
    {
      _loadingCurtain.Show();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() => 
      _loadingCurtain.Hide();

    private void OnLoaded()
    {
      GameObject initialPoint = GameObject.FindWithTag(InitialPointTag);
      
      GameObject hero = Instantiate(PlayerPath, initialPoint.transform.position);
      Instantiate(HudPath);
      
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

    private static GameObject Instantiate(string path)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab);
    }
    
    private static GameObject Instantiate(string path, Vector3 at)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, at, Quaternion.identity);
    }
  }
}