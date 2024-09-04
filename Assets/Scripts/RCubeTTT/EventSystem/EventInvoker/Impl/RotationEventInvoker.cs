using RCubeTTT.EventSystem.EventContext;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem.EventInvoker.Impl {
  public sealed class RotationEventInvoker : IRotationEventInvoker {
    private readonly UnityEvent<RotationEventContext> rotationEvent = new UnityEvent<RotationEventContext>();
    public UnityEvent<RotationEventContext> GetEvent() => rotationEvent;
  }
}