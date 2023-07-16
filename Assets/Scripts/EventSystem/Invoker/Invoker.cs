using UnityEngine.Events;

public interface Invoker {
  public void AddListener(UnityAction listener);
  void Invoke();
}

public interface Invoker<T0> {
  void AddListener(UnityAction<T0> listener);
  void Invoke(T0 context);
}

