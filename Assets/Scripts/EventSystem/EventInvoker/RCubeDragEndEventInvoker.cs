using UnityEngine.Events;

/// <summary>
/// Interface for invoking <see cref="RCubeDragEndEvent"/>.
/// </summary>
public interface RCubeDragEndEventInvoker : Invoker, EventProvider<RCubeDragEndEvent> {
  /// <summary>
  /// Adds a listener to the <see cref="RCubeDragEndEvent"/>.
  /// </summary>
  /// <param name="listener">The listener to add.</param>
  void Invoker.AddListener(UnityAction listener) => GetEvent().AddListener(listener);

  /// <summary>
  /// Invokes the <see cref="RCubeDragEndEvent"/>.
  /// </summary>
  void Invoker.Invoke() => GetEvent().Invoke();
}