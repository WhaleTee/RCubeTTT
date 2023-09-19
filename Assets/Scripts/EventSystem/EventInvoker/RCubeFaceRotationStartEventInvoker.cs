using UnityEngine.Events;

/// <summary>
/// Interface for invoking the <see cref="RCubeFaceRotationStartEvent"/>.
/// </summary>
public interface RCubeFaceRotationStartEventInvoker : Invoker<RCubeFaceRotationStartEventContext>, EventProvider<RCubeFaceRotationStartEvent> {
  /// <summary>
  /// Adds a listener to the <see cref="RCubeFaceRotationStartEvent"/>.
  /// </summary>
  /// <param name="listener">The UnityAction listener to add.</param>
  void Invoker<RCubeFaceRotationStartEventContext>.AddListener(UnityAction<RCubeFaceRotationStartEventContext> listener) => GetEvent().AddListener(listener);

  /// <summary>
  /// Invokes the <see cref="RCubeFaceRotationStartEvent"/>.
  /// </summary>
  /// <param name="context">The context of the <see cref="RCubeFaceRotationStartEvent"/>.</param>
  void Invoker<RCubeFaceRotationStartEventContext>.Invoke(RCubeFaceRotationStartEventContext context) => GetEvent().Invoke(context);
}