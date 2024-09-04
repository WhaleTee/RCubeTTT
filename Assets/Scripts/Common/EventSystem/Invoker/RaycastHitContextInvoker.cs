using Common.EventSystem.Context;
using UnityEngine.Events;

namespace Common.EventSystem.Invoker {
  public sealed class RaycastHitContextInvoker : IEventInvoker<RaycastHitContext>, EventProvider<UnityEvent<RaycastHitContext>> {
    private readonly UnityEvent<RaycastHitContext> pointerInstanceIdEvent = new UnityEvent<RaycastHitContext>();

    public UnityEvent<RaycastHitContext> GetEvent() => pointerInstanceIdEvent;
    void IEventInvoker<RaycastHitContext>.AddListener(UnityAction<RaycastHitContext> listener) => GetEvent().AddListener(listener);
    void IEventInvoker<RaycastHitContext>.Invoke(RaycastHitContext context) => GetEvent().Invoke(context);
  }
}