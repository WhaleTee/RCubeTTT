using UnityEngine.Events;

namespace Common.EventSystem.Invoker {
  public class NoContextInvoker : IEventInvoker, EventProvider<UnityEvent> {
    private readonly UnityEvent noContextEvent = new UnityEvent();

    public UnityEvent GetEvent() => noContextEvent;
    void IEventInvoker.AddListener(UnityAction listener) => GetEvent().AddListener(listener);
    void IEventInvoker.Invoke() => GetEvent().Invoke();
  }
}