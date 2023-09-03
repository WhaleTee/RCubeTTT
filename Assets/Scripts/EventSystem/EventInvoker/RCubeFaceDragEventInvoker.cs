using UnityEngine.Events;

/// <summary>
/// Interface for invoking <see cref="RCubeFaceDragEvent"/>.
/// </summary>
public interface RCubeFaceDragEventInvoker : Invoker<string>, EventProvider<RCubeFaceDragEvent> {
  /// <summary>
  /// Adds a listener to the <see cref="RCubeFaceDragEvent"/>.
  /// </summary>
  /// <param name="listener">The listener to add.</param>
  void Invoker<string>.AddListener(UnityAction<string> listener) => GetEvent().AddListener(listener);

  /// <summary>
  /// Invokes the <see cref="RCubeFaceDragEvent"/>.
  /// </summary>
  /// <param name="faceGlobalId">The context of the <see cref="RCubeFaceDragEvent"/> that represents Rubik's Cube's face global UUID.</param>
  void Invoker<string>.Invoke(string faceGlobalId) => GetEvent().Invoke(faceGlobalId);
}