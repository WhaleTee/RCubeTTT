using System.Collections;
using UnityEngine;
/// <summary>
/// Controls the fixed rotation behavior of the Rubik's Cube face.
/// </summary>
[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeFaceFixedRotationController : FixedRotationController {
  #region fields

  private readonly RCubeFaceRotationEventInvoker rCubeFaceRotationEventInvoker = new RCubeFaceRotationEventInvokerImpl();
  private readonly RCubeFaceRotationEndEventInvoker rCubeFaceRotationEndEventInvoker = new RCubeFaceRotationEndEventInvokerImpl();

  private GlobalIdentifier globalIdentifier;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddRCubeFaceRotationInvoker(rCubeFaceRotationEventInvoker);
    EventManager.AddRCubeFaceRotationEndInvoker(rCubeFaceRotationEndEventInvoker);

    EventManager.AddRCubeFaceDragStartListener(OnRCubeFaceDragStart);
    EventManager.AddRCubeFaceDragEndListener(OnRCubeFaceDragEnd);

    rotationContext = RotationContext.Local;
    targetRotation = currentRotation;

    globalIdentifier = GetComponent<GlobalIdentifier>();
  }

  #endregion

  #region methods
  
  /// <summary>
  /// Rotates the Rubik's Cube face to the target rotation.
  /// </summary>
  /// <returns>An <see cref="IEnumerator"/> used for coroutine execution.</returns>
  private IEnumerator RotateRCubeFace() {
    while (Quaternion.Angle(currentRotation, targetRotation) > 0) {
      Rotate();
      rCubeFaceRotationEventInvoker.Invoke(globalIdentifier.id);
      yield return null;
    }

    rCubeFaceRotationEndEventInvoker.Invoke(globalIdentifier.id);
  }

  /// <summary>
  /// Handles the start of dragging for a specific Rubik's Cube face.
  /// If the face's global identifier matches the current global identifier,
  /// it stops the rotation coroutine and sets the target rotation to the current rotation.
  /// </summary>
  /// <param name="faceGlobalId">The context of the <see cref="RCubeFaceDragEndEvent"/> that represents the Rubik's Cube face's global UUID.</param>
  private void OnRCubeFaceDragStart(string faceGlobalId) {
    if (faceGlobalId.Equals(globalIdentifier.id)) {
      targetRotation = currentRotation;
      StopCoroutine(RotateRCubeFace());
    }
  }

  /// <summary>
  /// Handles the end of dragging for a specific Rubik's Cube face.
  /// If the face's global identifier matches the current global identifier,
  /// it sets the target rotation to the nearest rotation and starts the rotation coroutine
  /// </summary>
  /// <param name="faceGlobalId">The context of the <see cref="RCubeFaceDragEndEvent"/> that represents the Rubik's Cube face's global UUID.</param>
  private void OnRCubeFaceDragEnd(string faceGlobalId) {
    if (faceGlobalId.Equals(globalIdentifier.id)) {
      targetRotation = GetNearestRotation();
      StartCoroutine(RotateRCubeFace());
    }
  }

  #endregion

  #region event invoker classes

  private sealed class RCubeFaceRotationEventInvokerImpl : RCubeFaceRotationEventInvoker {
    private readonly RCubeFaceRotationEvent cubeFaceRotationEvent = new RCubeFaceRotationEvent();
    public RCubeFaceRotationEvent GetEvent() => cubeFaceRotationEvent;
  }

  private sealed class RCubeFaceRotationEndEventInvokerImpl : RCubeFaceRotationEndEventInvoker {
    private readonly RCubeFaceRotationEndEvent rCubeFaceRotationEndEvent = new RCubeFaceRotationEndEvent();
    public RCubeFaceRotationEndEvent GetEvent() => rCubeFaceRotationEndEvent;
  }

  #endregion
}