using System;
using Infrastructure;
using Services.Input;
using UnityEngine;

namespace Player
{
  public class PlayerMove : MonoBehaviour
  {
    public CharacterController CharacterController;
    private IInputService _inputService;
    private Camera _camera;
    public float MovementSpeed;

    private void Awake()
    {
      _inputService = Game.InputService;
    }

    private void Start()
    {
      _camera = Camera.main;
    }

    private void Update()
    {
      Vector3 movementVector = Vector3.zero;

      if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
      {
        movementVector = _camera.transform.TransformDirection(_inputService.Axis);
        movementVector.y = 0;
        movementVector.Normalize();

        transform.forward = movementVector;
      }

      movementVector += Physics.gravity;
      
      CharacterController.Move(MovementSpeed * movementVector * Time.deltaTime);
    }
  }
}
