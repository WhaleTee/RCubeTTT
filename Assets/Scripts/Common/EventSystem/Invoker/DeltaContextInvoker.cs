using Common.EventSystem.Context;
using UnityEngine.Events;

namespace Common.EventSystem.Invoker {
  public sealed class DeltaContextInvoker : IEventInvoker<DeltaContext>, EventProvider<UnityEvent<DeltaContext>> {
    private readonly UnityEvent<DeltaContext> pointerDeltaEvent = new UnityEvent<DeltaContext>();

    public UnityEvent<DeltaContext> GetEvent() => pointerDeltaEvent;
    void IEventInvoker<DeltaContext>.AddListener(UnityAction<DeltaContext> listener) => GetEvent().AddListener(listener);
    void IEventInvoker<DeltaContext>.Invoke(DeltaContext context) => GetEvent().Invoke(context);
  }
}