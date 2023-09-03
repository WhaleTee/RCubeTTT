using UnityEngine.Events;

/// <summary>
/// Interface for invoking <see cref="RCubeFaceRotationEvent"/>
/// </summary>
public interface RCubeFaceRotationEventInvoker : Invoker<string>, EventProvider<RCubeFaceRotationEvent> {
  /// <summary>
  /// Adds a listener to the <see cref="RCubeFaceRotationEvent"/>
  /// </summary>
  /// <param name="listener">The listener to add.</param>
  void Invoker<string>.AddListener(UnityAction<string> listener) => GetEvent().AddListener(listener);

  /// <summary>
  /// Invokes the <see cref="RCubeFaceRotationEvent"/>
  /// </summary>
  /// <param name="faceGlobalId">The context of the <see cref="RCubeFaceRotationEvent"/> that represents Rubik's Cube's face global UUID.</param>
  void Invoker<string>.Invoke(string faceGlobalId) => GetEvent().Invoke(faceGlobalId);
}