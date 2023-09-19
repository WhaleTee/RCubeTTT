using UnityEngine.Events;

/// <summary>
/// Interface for invoking <see cref="RCubeFaceRotationEvent"/>
/// </summary>
public interface RCubeFaceRotationEventInvoker : Invoker<RCubeFaceRotationEventContext>, EventProvider<RCubeFaceRotationEvent> {
  /// <summary>
  /// Adds a listener to the <see cref="RCubeFaceRotationEvent"/>
  /// </summary>
  /// <param name="listener">The listener to add.</param>
  void Invoker<RCubeFaceRotationEventContext>.AddListener(UnityAction<RCubeFaceRotationEventContext> listener) => GetEvent().AddListener(listener);

  /// <summary>
  /// Invokes the <see cref="RCubeFaceRotationEvent"/>
  /// </summary>
  /// <param name="context">The context of the <see cref="RCubeFaceRotationEvent"/>.</param>
  void Invoker<RCubeFaceRotationEventContext>.Invoke(RCubeFaceRotationEventContext context) => GetEvent().Invoke(context);
}