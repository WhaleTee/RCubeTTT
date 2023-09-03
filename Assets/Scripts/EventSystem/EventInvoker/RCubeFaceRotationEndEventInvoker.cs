using UnityEngine.Events;

/// <summary>
/// Interface for invoking <see cref="RCubeFaceDragEndEvent"/>.
/// </summary>
public interface RCubeFaceRotationEndEventInvoker : Invoker<string>, EventProvider<RCubeFaceRotationEndEvent> {
  /// <summary>
  /// Adds a listener to the <see cref="RCubeFaceRotationEndEvent"/>.
  /// </summary>
  /// <param name="listener">The listener to add.</param>
  void Invoker<string>.AddListener(UnityAction<string> listener) => GetEvent().AddListener(listener);

  /// <summary>
  /// Invokes the <see cref="RCubeFaceRotationEndEvent"/>.
  /// </summary>
  /// <param name="faceGlobalId">The context of the <see cref="RCubeFaceRotationEndEvent"/> that represents Rubik's Cube's face global UUID.</param>
  void Invoker<string>.Invoke(string faceGlobalId) => GetEvent().Invoke(faceGlobalId);
}