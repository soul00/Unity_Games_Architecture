using Infrastructure.Factory;
using Infrastructure.Services;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Infrastructure.Enemy
{
  public class AgentMoveToPlayer : MonoBehaviour
  {
    public NavMeshAgent Agent;
    
    private const float MinimalDistance = 1;
    private Transform _heroTransform;
    private IGameFactory _gameFactory;

    private void Start()
    {
      _gameFactory = AllServices.Container.Single<IGameFactory>();

      if (_gameFactory.HeroGameObject != null)
      {
        InitializeHeroTranform();
      }
      else
      {
        _gameFactory.HeroCreated += HeroCreated;
      }
    }
    private void Update()
    {
      if (Initialized() && HeroNotReached())
      {
        Agent.destination = _heroTransform.position;
      }
    }
    private bool Initialized()
    {
      return _heroTransform != null;
    }

    private void InitializeHeroTranform()
    {
      _heroTransform = _gameFactory.HeroGameObject.transform;
    }
    private void HeroCreated()
    {
      InitializeHeroTranform();
    }

    private bool HeroNotReached() =>
      Vector3.Distance(Agent.transform.position, _heroTransform.position) >= MinimalDistance;
  }
}
