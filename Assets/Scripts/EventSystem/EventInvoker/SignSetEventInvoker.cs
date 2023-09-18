using UnityEngine.Events;

public interface SignSetEventInvoker : Invoker, EventProvider<SignSetEvent> {
  void Invoker.AddListener(UnityAction listener) => GetEvent().AddListener(listener);

  void Invoker.Invoke() => GetEvent().Invoke();
}