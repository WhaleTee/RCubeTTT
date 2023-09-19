using UnityEngine.Events;

/// <summary>
/// Interface for invoking <see cref="RCubeFaceDragEndEvent"/>.
/// </summary>
public interface RCubeFaceRotationEndEventInvoker : Invoker<RCubeFaceRotationEndEventContext>, EventProvider<RCubeFaceRotationEndEvent> {
  /// <summary>
  /// Adds a listener to the <see cref="RCubeFaceRotationEndEvent"/>.
  /// </summary>
  /// <param name="listener">The listener to add.</param>
  void Invoker<RCubeFaceRotationEndEventContext>.AddListener(UnityAction<RCubeFaceRotationEndEventContext> listener) => GetEvent().AddListener(listener);

  /// <summary>
  /// Invokes the <see cref="RCubeFaceRotationEndEvent"/>.
  /// </summary>
  /// <param name="context">The context of the <see cref="RCubeFaceRotationEndEvent"/>.</param>
  void Invoker<RCubeFaceRotationEndEventContext>.Invoke(RCubeFaceRotationEndEventContext context) => GetEvent().Invoke(context);
}