using UnityEngine.Events;

/// <summary>
/// Interface for invoking the <see cref="RCubeFaceDragEndEvent"/>.
/// </summary>
public interface RCubeFaceDragEndEventInvoker : Invoker<string>, EventProvider<RCubeFaceDragEndEvent> {
  /// <summary>
  /// Adds a listener to the <see cref="RCubeFaceDragEndEvent"/>.
  /// </summary>
  /// <param name="listener">The UnityAction listener to add.</param>
  void Invoker<string>.AddListener(UnityAction<string> listener) => GetEvent().AddListener(listener);
  
  /// <summary>
  /// Invokes the <see cref="RCubeFaceDragEndEvent"/>.
  /// </summary>
  /// <param name="faceGlobalId">The context of the <see cref="RCubeFaceDragEndEvent"/> that represents Rubik's Cube's face global UUID.</param>
  void Invoker<string>.Invoke(string faceGlobalId) => GetEvent().Invoke(faceGlobalId);
}