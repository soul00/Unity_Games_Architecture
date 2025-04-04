﻿using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Services;
using Loading;

namespace Infrastructure.States
{
  public class GameStateMachine
  {
    private readonly Dictionary<Type,IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
    {
      _states = new()
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
        [typeof(LoadingLevelState)] = new LoadingLevelState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>()),
        [typeof(GameLoopState)] = new GameLoopState(this),
      };
    }
    
    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
       state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;
      
      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState
    {
      return _states[typeof(TState)] as TState;
    }
  }
}