using UnityEngine.Events;

/// <summary>
/// Interface for invoking the <see cref="RCubeFaceDragStartEvent"/>.
/// </summary>
public interface RCubeFaceDragStartEventInvoker : Invoker<RCubeFaceDragStartEventContext>, EventProvider<RCubeFaceDragStartEvent> {
  /// <summary>
  /// Adds a listener to the <see cref="RCubeFaceDragStartEvent"/>.
  /// </summary>
  /// <param name="listener">The UnityAction listener to add.</param>
  void Invoker<RCubeFaceDragStartEventContext>.AddListener(UnityAction<RCubeFaceDragStartEventContext> listener) => GetEvent().AddListener(listener);

  /// <summary>
  /// Invokes the <see cref="RCubeFaceDragStartEvent"/>.
  /// </summary>
  /// <param name="context">The <see cref="RCubeFaceDragStartEventContext"/>.</param>
  void Invoker<RCubeFaceDragStartEventContext>.Invoke(RCubeFaceDragStartEventContext context) => GetEvent().Invoke(context);
}