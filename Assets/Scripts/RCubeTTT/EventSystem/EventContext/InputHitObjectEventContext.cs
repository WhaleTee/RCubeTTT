using RCubeTTT.Model;
using UnityEngine;

namespace RCubeTTT.EventSystem.EventContext
{
  public sealed class InputHitObjectEventContext : ObjectInstanceContext {
    public Vector2 hitPosition { get; private set; }

    public InputHitObjectEventContext(int instanceId, Vector2 hitPosition) : base(instanceId) {
      this.hitPosition = hitPosition;
    }
  }
}