using UnityEngine.Events;

/// <summary>
/// Interface for an invoker that can add listeners and invoke them.
/// </summary>
public interface Invoker {
  /// <summary>
  /// Adds a listener to the invoker.
  /// </summary>
  /// <param name="listener">The listener to add.</param>
  void AddListener(UnityAction listener);

  /// <summary>
  /// Invokes all the added listeners.
  /// </summary>
  void Invoke();
}

/// <summary>
/// Interface for an invoker that can add listeners and invoke them with a context of type T0.
/// </summary>
/// <typeparam name="T0">The type of the context.</typeparam>
public interface Invoker<T0> {
  /// <summary>
  /// Adds a listener to the invoker.
  /// </summary>
  /// <param name="listener">The listener to add.</param>
  void AddListener(UnityAction<T0> listener);

  /// <summary>
  /// Invokes all the added listeners with the provided context.
  /// </summary>
  /// <param name="context">The context to pass to the listeners.</param>
  void Invoke(T0 context);
}