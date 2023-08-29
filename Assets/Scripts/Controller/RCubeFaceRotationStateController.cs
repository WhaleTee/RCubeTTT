using MyBox;
using UnityEngine;

[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeFaceRotationStateController : MonoBehaviour {
  #region fields

  private readonly StartRCubeFaceRotationInvoker startRCubeFaceRotationInvoker = new FaceStateStartRCubeFaceRotationInvoker();
  private readonly RCubeFaceRotationInvoker cubeFaceRotationInvoker = new FaceStateRCubeFaceRotationInvoker();
  private readonly EndRCubeFaceRotationInvoker endRCubeFaceRotationInvoker = new FaceStateEndRCubeFaceRotationInvoker();

  private bool isRotating;

  private GlobalIdentifier globalIdentifier;
  
  #endregion

  #region properties

  public Quaternion previousLocalRotation { get; private set; }
  public Quaternion currentLocalRotation => transform.localRotation;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddStartRCubeFaceRotationInvoker(startRCubeFaceRotationInvoker);
    EventManager.AddRCubeFaceRotationInvoker(cubeFaceRotationInvoker);
    EventManager.AddEndRCubeFaceRotationInvoker(endRCubeFaceRotationInvoker);
    
    globalIdentifier = GetComponent<GlobalIdentifier>();
    
    previousLocalRotation = transform.localRotation;
  }

  private void LateUpdate() {
    if (Mathf.Abs(Quaternion.Angle(previousLocalRotation, currentLocalRotation)) > 0f) {
      if (isRotating) {
        cubeFaceRotationInvoker.Invoke(globalIdentifier.id);
      } else {
        isRotating = true;
        startRCubeFaceRotationInvoker.Invoke(globalIdentifier.id);
      }
    } else {
      if (isRotating) {
        isRotating = false;
        endRCubeFaceRotationInvoker.Invoke(globalIdentifier.id);
      }
    }

    previousLocalRotation = transform.localRotation;
  }

  #endregion

  #region event invoker classes

  private sealed class FaceStateStartRCubeFaceRotationInvoker : StartRCubeFaceRotationInvoker {
    private readonly StartRCubeFaceRotationEvent startRCubeFaceRotationEvent = new StartRCubeFaceRotationEvent();
    public StartRCubeFaceRotationEvent GetInputEvent() => startRCubeFaceRotationEvent;
  }

  private sealed class FaceStateRCubeFaceRotationInvoker : RCubeFaceRotationInvoker {
    private readonly RCubeFaceRotationEvent cubeFaceRotationEvent = new RCubeFaceRotationEvent();
    public RCubeFaceRotationEvent GetInputEvent() => cubeFaceRotationEvent;
  }

  private sealed class FaceStateEndRCubeFaceRotationInvoker : EndRCubeFaceRotationInvoker {
    private readonly EndRCubeFaceRotationEvent endRCubeFaceRotationEvent = new EndRCubeFaceRotationEvent();
    public EndRCubeFaceRotationEvent GetInputEvent() => endRCubeFaceRotationEvent;
  }

  #endregion
}