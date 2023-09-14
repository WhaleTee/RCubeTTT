using UnityEngine.Events;

/// <summary>
/// Interface for invoking the <see cref="RCubeFaceDragStartEvent"/>.
/// </summary>
public interface RCubeFaceDragStartEventInvoker : Invoker<RCubeFaceRaycastHitEventContext>, EventProvider<RCubeFaceDragStartEvent> {
  /// <summary>
  /// Adds a listener to the <see cref="RCubeFaceDragStartEvent"/>.
  /// </summary>
  /// <param name="listener">The UnityAction listener to add.</param>
  void Invoker<RCubeFaceRaycastHitEventContext>.AddListener(UnityAction<RCubeFaceRaycastHitEventContext> listener) => GetEvent().AddListener(listener);

  /// <summary>
  /// Invokes the <see cref="RCubeFaceDragStartEvent"/>.
  /// </summary>
  /// <param name="context">The <see cref="RCubeFaceRaycastHitEventContext"/>.</param>
  void Invoker<RCubeFaceRaycastHitEventContext>.Invoke(RCubeFaceRaycastHitEventContext context) => GetEvent().Invoke(context);
}