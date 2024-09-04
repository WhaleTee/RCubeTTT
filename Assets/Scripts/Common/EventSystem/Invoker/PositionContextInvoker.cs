using Common.EventSystem.Context;
using UnityEngine.Events;

namespace Common.EventSystem.Invoker {
  public sealed class PositionContextInvoker : IEventInvoker<PositionContext>, EventProvider<UnityEvent<PositionContext>> {
    private readonly UnityEvent<PositionContext> pointerPositionEvent = new UnityEvent<PositionContext>();

    public UnityEvent<PositionContext> GetEvent() => pointerPositionEvent;
    void IEventInvoker<PositionContext>.AddListener(UnityAction<PositionContext> listener) => GetEvent().AddListener(listener);
    void IEventInvoker<PositionContext>.Invoke(PositionContext context) => GetEvent().Invoke(context);
  }
}