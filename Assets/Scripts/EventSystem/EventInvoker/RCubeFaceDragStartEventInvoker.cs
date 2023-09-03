using UnityEngine.Events;

/// <summary>
/// Interface for invoking the <see cref="RCubeFaceDragStartEvent"/>.
/// </summary>
public interface RCubeFaceDragStartEventInvoker : Invoker<string>, EventProvider<RCubeFaceDragStartEvent> {
  /// <summary>
  /// Adds a listener to the <see cref="RCubeFaceDragStartEvent"/>.
  /// </summary>
  /// <param name="listener">The UnityAction listener to add.</param>
  void Invoker<string>.AddListener(UnityAction<string> listener) => GetEvent().AddListener(listener);

  /// <summary>
  /// Invokes the <see cref="RCubeFaceDragStartEvent"/>.
  /// </summary>
  /// <param name="faceGlobalId">The context of the <see cref="RCubeFaceDragStartEvent"/> that represents Rubik's Cube's face global UUID.</param>
  void Invoker<string>.Invoke(string faceGlobalId) => GetEvent().Invoke(faceGlobalId);
}