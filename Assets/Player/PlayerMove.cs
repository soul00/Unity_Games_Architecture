using Infrastructure;
using Infrastructure.Services;
using Services.Input;
using UnityEngine;

namespace Player
{
  public class PlayerMove : MonoBehaviour
  {
    public CharacterController CharacterController;
    public float MovementSpeed;

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
          movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
        
        movementVector.y = 0;
        movementVector.Normalize();

        transform.forward = movementVector;
      }
      movementVector += Physics.gravity;
      
      CharacterController.Move(MovementSpeed * movementVector * Time.deltaTime);
    }
  }
}
