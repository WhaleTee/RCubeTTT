using UnityEngine.Events;

/// <summary>
/// Interface for invoking the <see cref="RCubeDragStartEvent"/>.
/// </summary>
public interface RCubeDragStartEventInvoker : Invoker, EventProvider<RCubeDragStartEvent> {
  /// <summary>
  /// Adds a listener to the <see cref="RCubeDragStartEvent"/>.
  /// </summary>
  /// <param name="listener">The listener to add.</param>
  void Invoker.AddListener(UnityAction listener) => GetEvent().AddListener(listener);

  /// <summary>
  /// Invokes the <see cref="RCubeDragStartEvent"/>.
  /// </summary>
  void Invoker.Invoke() => GetEvent().Invoke();
}