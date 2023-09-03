using UnityEngine;

[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeFaceFixedRotationController : FixedRotationController {
  #region fields

  private readonly RCubeFaceRotationStartEventInvoker rCubeFaceRotationStartEventInvoker =
  new RCubeFaceFixedRotationStartEventInvoker();

  private readonly RCubeFaceRotationEventInvoker rCubeFaceRotationEventInvoker = new RCubeFaceFixedRotationEventInvoker();
  private readonly RCubeFaceRotationEndEventInvoker rCubeFaceRotationEndEventInvoker = new RCubeFaceFixedRotationEndEventInvoker();

  private bool isRotating;
  private bool isDragging;
  private float rotationElapsedTime;
  private Quaternion derivative;

  private GlobalIdentifier globalIdentifier;

  #endregion

  #region properties

  public Quaternion currentRotation => GetCurrentRotation();
  public Quaternion targetRotation { get; private set; }

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddRCubeFaceDragStartListener(StartDragRCubeFaceHandler);
    EventManager.AddRCubeFaceDragEndListener(EndDragRCubeFaceHandler);

    EventManager.AddRCubeFaceRotationStartInvoker(rCubeFaceRotationStartEventInvoker);
    EventManager.AddRCubeFaceRotationInvoker(rCubeFaceRotationEventInvoker);
    EventManager.AddRCubeFaceRotationEndInvoker(rCubeFaceRotationEndEventInvoker);

    globalIdentifier = GetComponent<GlobalIdentifier>();

    targetRotation = GetCurrentRotation();
  }

  private void Update() {
    if (!isDragging) {
      RotateSlerp();
      // RotateSmooth();
      rotationElapsedTime += Time.deltaTime;
    }
  }

  private void LateUpdate() {
    if (Quaternion.Angle(GetCurrentRotation(), targetRotation) > 0) {
      if (isRotating) {
        rCubeFaceRotationEventInvoker.Invoke(globalIdentifier.id);
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
  }

  #endregion

  #region methods

  private void RotateSlerp() {
    transform.localRotation = Quaternion.Slerp(GetCurrentRotation(), targetRotation, rotationElapsedTime / rotateDuration);
  }

  private void RotateSmooth() {
    transform.localRotation = GetCurrentRotation().SmoothDamp(targetRotation, ref derivative, rotateDuration);
  }

  private void StartDragRCubeFaceHandler(string faceGlobalId) {
    rotationElapsedTime = 0;
    isDragging = true;
    targetRotation = GetCurrentRotation();
  }

  private void EndDragRCubeFaceHandler(string faceGlobalId) {
    isDragging = false;
    targetRotation = GetNearestRotation();
  }

  protected override Quaternion GetCurrentRotation() => transform.localRotation;

  #endregion

  #region event invoker classes

  private sealed class RCubeFaceFixedRotationStartEventInvoker : RCubeFaceRotationStartEventInvoker {
    private readonly RCubeFaceRotationStartEvent rCubeFaceRotationStartEvent = new RCubeFaceRotationStartEvent();
    public RCubeFaceRotationStartEvent GetEvent() => rCubeFaceRotationStartEvent;
  }

  private sealed class RCubeFaceFixedRotationEventInvoker : RCubeFaceRotationEventInvoker {
    private readonly RCubeFaceRotationEvent cubeFaceRotationEvent = new RCubeFaceRotationEvent();
    public RCubeFaceRotationEvent GetEvent() => cubeFaceRotationEvent;
  }

  private sealed class RCubeFaceFixedRotationEndEventInvoker : RCubeFaceRotationEndEventInvoker {
    private readonly RCubeFaceRotationEndEvent rCubeFaceRotationEndEvent = new RCubeFaceRotationEndEvent();
    public RCubeFaceRotationEndEvent GetEvent() => rCubeFaceRotationEndEvent;
  }

  #endregion
}