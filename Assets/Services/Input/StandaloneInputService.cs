﻿using UnityEngine;

namespace Services.Input
{
  class StandaloneInputService : InputService
  {
    public override Vector2 Axis
    {
      get
      {
        Vector2 axis = SimpleInputAxis();

        if (axis == Vector2.zero)
          axis = UnityAxis();
                
        return axis;
      }
    }

    private static Vector2 UnityAxis() =>
      new(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Horizontal));
  }
}