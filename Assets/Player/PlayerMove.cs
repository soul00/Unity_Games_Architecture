using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
  public class PlayerMove : MonoBehaviour, ISavedProgress
  {
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float MovementSpeed;

    private IInputService _inputService;

    private void Awake()
    {
      _inputService = AllServices.Container.Single<IInputService>();
    }

    private void Update()
    {
      Vector3 movementVector = Vector3.zero;

      if (_inputService?.Axis.sqrMagnitude > Constants.Epsilon)
      {
        if (Camera.main != null)
        {
          movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
        }
        
        movementVector.y = 0;
        movementVector.Normalize();
        transform.forward = movementVector;
      }
      movementVector += Physics.gravity;
      _characterController.Move(MovementSpeed * movementVector * Time.deltaTime);
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
    }

    public void LoadProgress(PlayerProgress progress)
    {
      if(CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
      {
        Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
        if (savedPosition != null)
        {
          Warp(to: savedPosition);
        }
      }
    }

    private void Warp(Vector3Data to)
    {
      _characterController.enabled = false;
      transform.position = to.AsUnityVector().AddY(_characterController.height);
      _characterController.enabled = true;
    }

    private static string CurrentLevel() =>
      SceneManager.GetActiveScene().name;
  }
}
