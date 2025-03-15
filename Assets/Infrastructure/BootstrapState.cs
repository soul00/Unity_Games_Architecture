using Services.Input;
using UnityEngine;

namespace Infrastructure
{
  public class BootstrapState : IState
  {
    private readonly GameStateMachine stateMachine;

    public BootstrapState(GameStateMachine stateMachine)
    {
      this.stateMachine = stateMachine;
    }

    public void Enter()
    {
      RegisterServices();
    }

    private void RegisterServices()
    {
      Game.InputService = RegisterInputService();
    }

    public void Exit()
    {
      
    }

    private static IInputService RegisterInputService()
    {
      if (Application.isEditor)
        return new StandaloneInputService();
      else
        return new MobileInput();
    }
  }
}