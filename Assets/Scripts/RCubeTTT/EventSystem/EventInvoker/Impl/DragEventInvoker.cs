using RCubeTTT.EventSystem.EventContext;
using RCubeTTT.Model;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem.EventInvoker.Impl {
  public sealed class DragEventInvoker : IDragEventInvoker {
    private readonly UnityEvent<ObjectInstanceContext> rCubeDragEvent = new UnityEvent<ObjectInstanceContext>();
    public UnityEvent<ObjectInstanceContext> GetEvent() => rCubeDragEvent;
  }
}