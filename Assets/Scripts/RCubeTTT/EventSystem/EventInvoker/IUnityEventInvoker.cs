using UnityEngine.Events;

namespace RCubeTTT.EventSystem.EventInvoker {
  /// <summary>
  /// Interface for invoking the <see cref="UnityEvent"/>.
  /// </summary>
  public interface IUnityEventInvoker : Invoker, EventProvider<UnityEvent> {
    /// <summary>
    /// Adds a listener to the <see cref="UnityEvent"/>.
    /// </summary>
    /// <param name="listener">The listener to add.</param>
    void Invoker.AddListener(UnityAction listener) => GetEvent().AddListener(listener);

    /// <summary>
    /// Invokes the <see cref="UnityEvent"/>.
    /// </summary>
    void Invoker.Invoke() => GetEvent().Invoke();
  }
}