using Services.Input;
using UnityEngine;

namespace Infrastructure
{
  public class Game
  {
    public static IInputService InputService;
    public GameStateMachine stateMachine;

    public Game()
    {
      stateMachine = new GameStateMachine();
    }
  }
}