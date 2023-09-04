using UnityEngine;

[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeFaceRotationStateController : MonoBehaviour {
  #region fields

  private readonly RCubeFaceRotationStartEventInvoker rCubeFaceRotationStartEventInvoker = new RCubeFaceRotationStateStartEventInvoker();
  private readonly RCubeFaceRotationEventInvoker rCubeFaceRotationEventInvoker = new RCubeFaceRotationStateEventInvoker();
  private readonly RCubeFaceRotationEndEventInvoker rCubeFaceRotationEndEventInvoker = new RCubeFaceRotationStateEndEventInvoker();

  private bool isRotating;
  private bool isDragging;
  private Quaternion previousLocalRotation;

  private GlobalIdentifier globalIdentifier;

  #endregion

  #region properties

  private Quaternion currentLocalRotation => transform.localRotation;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddRCubeFaceRotationStartInvoker(rCubeFaceRotationStartEventInvoker);
    EventManager.AddRCubeFaceRotationInvoker(rCubeFaceRotationEventInvoker);
    EventManager.AddRCubeFaceRotationEndInvoker(rCubeFaceRotationEndEventInvoker);

    EventManager.AddRCubeFaceDragStartListener(
      faceGlobalId => {
        if (IsThisFaceId(faceGlobalId)) {
          isDragging = true;
        }
      }
    );

    EventManager.AddRCubeFaceDragEndListener(
      faceGlobalId => {
        if (IsThisFaceId(faceGlobalId)) {
          isDragging = false;
        }
      }
    );

    globalIdentifier = GetComponent<GlobalIdentifier>();

    previousLocalRotation = transform.localRotation;
  }

  private void LateUpdate() {
    if (!previousLocalRotation.Equals(currentLocalRotation)) {
      if (isRotating) {
        rCubeFaceRotationEventInvoker.Invoke(globalIdentifier.id);
      } else {
        isRotating = true;
        rCubeFaceRotationStartEventInvoker.Invoke(globalIdentifier.id);
      }
    } else if (!isDragging) {
      if (isRotating) {
        isRotating = false;
        rCubeFaceRotationEndEventInvoker.Invoke(globalIdentifier.id);
      }
    }

    previousLocalRotation = transform.localRotation;
  }

  private bool IsThisFaceId(string faceGlobalId) => faceGlobalId.Equals(globalIdentifier.id);

  #endregion

  #region event invoker classes

  private sealed class RCubeFaceRotationStateStartEventInvoker : RCubeFaceRotationStartEventInvoker {
    private readonly RCubeFaceRotationStartEvent rCubeFaceRotationStartEvent = new RCubeFaceRotationStartEvent();
    public RCubeFaceRotationStartEvent GetEvent() => rCubeFaceRotationStartEvent;
  }

  private sealed class RCubeFaceRotationStateEventInvoker : RCubeFaceRotationEventInvoker {
    private readonly RCubeFaceRotationEvent cubeFaceRotationEvent = new RCubeFaceRotationEvent();
    public RCubeFaceRotationEvent GetEvent() => cubeFaceRotationEvent;
  }

  private sealed class RCubeFaceRotationStateEndEventInvoker : RCubeFaceRotationEndEventInvoker {
    private readonly RCubeFaceRotationEndEvent rCubeFaceRotationEndEvent = new RCubeFaceRotationEndEvent();
    public RCubeFaceRotationEndEvent GetEvent() => rCubeFaceRotationEndEvent;
  }

  #endregion
}