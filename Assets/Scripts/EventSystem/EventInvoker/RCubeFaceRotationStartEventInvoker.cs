using UnityEngine.Events;

/// <summary>
/// Interface for invoking the <see cref="RCubeFaceRotationStartEvent"/>.
/// </summary>
public interface RCubeFaceRotationStartEventInvoker : Invoker<string>, EventProvider<RCubeFaceRotationStartEvent> {
  /// <summary>
  /// Adds a listener to the <see cref="RCubeFaceRotationStartEvent"/>.
  /// </summary>
  /// <param name="listener">The UnityAction listener to add.</param>
  void Invoker<string>.AddListener(UnityAction<string> listener) => GetEvent().AddListener(listener);

  /// <summary>
  /// Invokes the <see cref="RCubeFaceRotationStartEvent"/>.
  /// </summary>
  /// <param name="faceGlobalId">The context of the <see cref="RCubeFaceRotationStartEvent"/> that represents Rubik's Cube's face global UUID.</param>
  void Invoker<string>.Invoke(string faceGlobalId) => GetEvent().Invoke(faceGlobalId);
}