using Infrastructure.AnimatorSM;
using System;
using UnityEngine;

namespace Infrastructure.Enemy
{
  [RequireComponent(typeof(UnityEngine.Animator))]
  public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
  {
    private static readonly int Attack = UnityEngine.Animator.StringToHash("Attack");
    private static readonly int Speed = UnityEngine.Animator.StringToHash("Speed");
    private static readonly int IsMoving = UnityEngine.Animator.StringToHash("IsMoving");
    private static readonly int Die = UnityEngine.Animator.StringToHash("Die");
    private static readonly int Hit = UnityEngine.Animator.StringToHash("Hit");

    private readonly int _idleStateHash = UnityEngine.Animator.StringToHash("Idle");
    private readonly int _attackStateHash = UnityEngine.Animator.StringToHash("Attack_01");
    private readonly int _walkingStateHash = UnityEngine.Animator.StringToHash("Move");
    private readonly int _deathStateHash = UnityEngine.Animator.StringToHash("die");

    private Animator _animator;

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    public AnimatorState State { get; private set; }

    private void Awake() =>
      _animator = GetComponent<UnityEngine.Animator>();

    public void PlayHit() => _animator.SetTrigger(Hit);
    public void PlayDeath() => _animator.SetTrigger(Die);

    public void Move(float speed)
    {
      _animator.SetBool(IsMoving, true);
      _animator.SetFloat(Speed, speed);
    }

    public void StopMoving() => _animator.SetBool(IsMoving, false);
    public void PlayAttack() => _animator.SetTrigger(Attack);

    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash) => 
      StateExited?.Invoke(StateFor(stateHash));

    private AnimatorState StateFor(int stateHash)
    {
      AnimatorState state;
      if (stateHash == _idleStateHash)
      {
        state = AnimatorState.Idle;
      }
      else if (stateHash == _attackStateHash)
      {
        state = AnimatorState.Attack;
      }
      else if (stateHash == _walkingStateHash)
      {
        state = AnimatorState.Walking;
      }
      else if (stateHash == _deathStateHash)
      {
        state = AnimatorState.Died;
      }
      else
        state = AnimatorState.Unknown;

      return state;
    }
  }
}
