﻿using System;
using UnityEngine;

namespace CameraScripts
{
  public class FollowCamera : MonoBehaviour
  {
    public float RotationAngleX;
    public float Distance;
    public float OffsetY;
    public float RotationAngleY;

    [SerializeField] private Transform _following;

    private void LateUpdate()
    {
      if (_following == null)
        return;

      Quaternion rotation = Quaternion.Euler(RotationAngleX, RotationAngleY, 0);

      Vector3 position = rotation * new Vector3(0, 0, -Distance) + FollowingPositionPoint();

      transform.rotation = rotation;
      transform.position = position;
    }

    public void Follow(GameObject following) => _following = following.transform;

    private Vector3 FollowingPositionPoint()
    {
      Vector3 followingPosition = _following.position;
      followingPosition.y += OffsetY;

      return followingPosition;
    }
  }
}