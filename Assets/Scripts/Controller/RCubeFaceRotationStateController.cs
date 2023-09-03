using UnityEngine;

[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeFaceRotationStateController : MonoBehaviour {
  #region fields

  private readonly RCubeFaceRotationStartEventInvoker rCubeFaceRotationStartEventInvoker =
  new RCubeFaceRotationStateStartEventInvoker();

  private readonly RCubeFaceRotationEventInvoker cubeFaceInvoker = new RCubeFaceRotationStateEventInvoker();
  private readonly RCubeFaceRotationEndEventInvoker rCubeFaceRotationEndEventInvoker = new RCubeFaceRotationStateEndEventInvoker();

  private bool isRotating;

  private GlobalIdentifier globalIdentifier;

  #endregion

  #region properties

  public Quaternion previousLocalRotation { get; private set; }
  public Quaternion currentLocalRotation => transform.rotation;

  #endregion

  #region unity methods

  private void Awake() {
    // EventManager.AddStartRCubeFaceRotationInvoker(startRCubeFaceRotationInvoker);
    // EventManager.AddRCubeFaceRotationInvoker(cubeFaceRotationInvoker);
    // EventManager.AddEndRCubeFaceRotationInvoker(endRCubeFaceRotationInvoker);

    globalIdentifier = GetComponent<GlobalIdentifier>();

    previousLocalRotation = transform.rotation;
  }

  private void LateUpdate() {
    if (!previousLocalRotation.Equals(currentLocalRotation)) {
      if (isRotating) {
        cubeFaceInvoker.Invoke(globalIdentifier.id);
      } else {
        isRotating = true;
        rCubeFaceRotationStartEventInvoker.Invoke(globalIdentifier.id);
      }
    } else {
      if (isRotating) {
        isRotating = false;
        rCubeFaceRotationEndEventInvoker.Invoke(globalIdentifier.id);
      }
    }

    previousLocalRotation = transform.rotation;
  }

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