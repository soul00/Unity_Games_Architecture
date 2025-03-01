using UnityEngine;

namespace Services.Input
{
  class MobileInput : InputService
  {
    public override Vector2 Axis => SimpleInputAxis();
  }
}