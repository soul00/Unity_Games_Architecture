using Infrastructure.Enemy;
using System;
using UnityEngine;

public class Aggro : MonoBehaviour
{
  public TriggerObserver TriggerObserver;
  public AgentMoveToHero Follow;

  private void Start()
  {
    TriggerObserver.TriggerEnter += OnTriggerEnter;
    TriggerObserver.TriggerExit += OnTriggerExit;

    SwitchFollow(isOn: false);
  }
  private void OnTriggerEnter(Collider collider) => 
    SwitchFollow(isOn: true);

  private void OnTriggerExit(Collider collider) => 
    SwitchFollow(isOn: false);

  private void SwitchFollow(bool isOn) => 
    Follow.enabled = isOn;
}
