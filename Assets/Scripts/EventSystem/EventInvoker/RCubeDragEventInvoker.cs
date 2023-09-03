using UnityEngine.Events;

/// <summary>
/// Interface for invoking <see cref="RCubeDragEvent"/>.
/// </summary>
public interface RCubeDragEventInvoker : Invoker, EventProvider<RCubeDragEvent> {
  /// <summary>
  /// Adds a listener to the <see cref="RCubeDragEvent"/>.
  /// </summary>
  /// <param name="listener">The listener to add.</param>
  void Invoker.AddListener(UnityAction listener) => GetEvent().AddListener(listener);

  /// <summary>
  /// Invokes the <see cref="RCubeDragEvent"/>.
  /// </summary>
  void Invoker.Invoke() => GetEvent().Invoke();
}