using UnityEngine.Events;

namespace RCubeTTT.EventSystem.EventInvoker.Impl
{
  public sealed class UnityEventInvoker : IUnityEventInvoker {
    private readonly UnityEvent rCubeDragStartEvent = new UnityEvent();
    public UnityEvent GetEvent() => rCubeDragStartEvent;
  }
}