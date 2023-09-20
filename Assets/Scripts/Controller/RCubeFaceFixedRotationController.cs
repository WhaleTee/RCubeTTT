using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Controls the fixed rotation behavior of the Rubik's Cube face.
/// </summary>
public class RCubeFaceFixedRotationController : FixedRotationController {
  #region serializable fields

  [SerializeField]
  private GlobalIdentifier faceIdentifier;

  [FormerlySerializedAs("facePosition")]
  [SerializeField]
  private RCubeFacePositionType facePositionType;

  #endregion

  #region fields

  private readonly RCubeFaceRotationEventInvoker rCubeFaceRotationEventInvoker = new RCubeFaceRotationEventInvokerImpl();
  private readonly RCubeFaceRotationEndEventInvoker rCubeFaceRotationEndEventInvoker = new RCubeFaceRotationEndEventInvokerImpl();

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddRCubeFaceRotationInvoker(rCubeFaceRotationEventInvoker);
    EventManager.AddRCubeFaceRotationEndInvoker(rCubeFaceRotationEndEventInvoker);

    EventManager.AddRCubeFaceDragStartListener(OnRCubeFaceDragStart);
    EventManager.AddRCubeFaceDragEndListener(OnRCubeFaceDragEnd);

    rotationContext = RotationContext.Local;
    targetRotation = currentRotation;
  }

  #endregion

  #region methods

  /// <summary>
  /// Rotates the Rubik's Cube face to the target rotation.
  /// </summary>
  /// <returns>An <see cref="IEnumerator"/> used for coroutine execution.</returns>
  private IEnumerator RotateRCubeFace() {
    while (Quaternion.Angle(currentRotation, targetRotation) > 0.5) {
      Rotate();
      rotationElapsedTime += Time.deltaTime;
      rCubeFaceRotationEventInvoker.Invoke(new RCubeFaceRotationEventContext(faceIdentifier.id, facePositionType));
      yield return null;
    }

    transform.localRotation = targetRotation;
    rotationElapsedTime = 0;
    rCubeFaceRotationEndEventInvoker.Invoke(new RCubeFaceRotationEndEventContext(faceIdentifier.id, facePositionType, targetRotation));
  }

  /// <summary>
  /// Handles the start of dragging for a specific Rubik's Cube face.
  /// If the face's global identifier matches the current global identifier,
  /// it stops the rotation coroutine and sets the target rotation to the current rotation.
  /// </summary>
  /// <param name="context">The <see cref="RCubeFaceDragStartEventContext"/>.</param>
  private void OnRCubeFaceDragStart(RCubeFaceDragStartEventContext context) {
    if (context.faceGlobalId.Equals(faceIdentifier.id)) {
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
    if (faceGlobalId.Equals(faceIdentifier.id)) {
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