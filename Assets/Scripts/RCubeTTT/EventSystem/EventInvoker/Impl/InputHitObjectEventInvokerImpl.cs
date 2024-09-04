using RCubeTTT.EventSystem.EventContext;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem.EventInvoker.Impl {
  public sealed class InputHitObjectEventInvokerImpl : InputHitObjectEventInvoker {
    private readonly UnityEvent<InputHitObjectEventContext> rCubeDragStartEvent = new UnityEvent<InputHitObjectEventContext>();
    public UnityEvent<InputHitObjectEventContext> GetEvent() => rCubeDragStartEvent;
  }
}