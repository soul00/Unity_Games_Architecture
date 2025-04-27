using Infrastructure.AnimatorSM;

namespace Infrastructure.Enemy
{
  public interface IAnimationStateReader
  {
    void EnteredState(int stateHash);
    void ExitedState(int stateHash);
    AnimatorState State { get; }
  }
}