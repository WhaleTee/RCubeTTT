using UnityEngine.Events;

namespace Common.EventSystem.Invoker {
  public interface IEventInvoker {
    public void AddListener(UnityAction listener);
    public void Invoke();
  }

  public interface IEventInvoker<T0> {
    public void AddListener(UnityAction<T0> listener);
    public void Invoke(T0 context);
  }
}