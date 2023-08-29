using UnityEngine;

public class RCubeFaceFixedRotationController : FixedRotationController {
  #region fields
  
  private bool isDragging;
  private float rotationElapsedTime;

  #endregion

  #region properties

  public Quaternion currentRotation => GetCurrentRotation();
  public Quaternion targetRotation { get; private set; }

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddStartDragRCubeFaceListener(StartDragRCubeFaceHandler);
    EventManager.AddEndDragRCubeFaceListener(EndDragRCubeFaceHandler);
    
    targetRotation = GetCurrentRotation();
  }

  private void Update() {
    if (!isDragging) {
      Rotate();
      rotationElapsedTime += Time.deltaTime;
    }
  }

  #endregion

  #region methods

  private void Rotate() {
    transform.localRotation = Quaternion.Slerp(GetCurrentRotation(), targetRotation, rotationElapsedTime / rotateDuration);
  }

  private void StartDragRCubeFaceHandler() {
    rotationElapsedTime = 0;
    isDragging = true;
    targetRotation = GetCurrentRotation();
  }

  private void EndDragRCubeFaceHandler() {
    isDragging = false;
    targetRotation = GetNearestRotation();
  }

  protected override Quaternion GetCurrentRotation() => transform.localRotation;

  #endregion
}