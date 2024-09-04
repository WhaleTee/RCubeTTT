using RCubeTTT.Model;
using UnityEngine;

namespace RCubeTTT.EventSystem.EventContext {
  public sealed class RotationEventContext : ObjectInstanceContext {
    public Quaternion rotation { get; private set; }
    public Quaternion localRotation { get; private set; }

    public RotationEventContext(int instanceId, Quaternion rotation, Quaternion localRotation) : base(instanceId) {
      this.rotation = rotation;
      this.localRotation = localRotation;
    }
  }
}