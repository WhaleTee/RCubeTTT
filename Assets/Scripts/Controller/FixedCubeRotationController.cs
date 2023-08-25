using UnityEngine;

public class FixedCubeRotationController : FixedRotationController {
  #region fields

  private bool isDragging;
  private Quaternion targetRotation;
  private float rotationElapsedTime;

  #endregion
  
  #region unity methods

  private void Awake() {
    targetRotation = CurrentRotation();
    EventManager.AddStartDragRCubeListener(StartDragRCubeHandler);
    EventManager.AddEndDragRCubeListener(EndDragRCubeHandler);
  }

  private void Update() {
    if (!isDragging) {
      transform.localRotation = Quaternion.Slerp(CurrentRotation(), targetRotation, rotationElapsedTime / rotateDuration);
      rotationElapsedTime += Time.deltaTime;
    }
  }

  #endregion

  #region methods

  private void StartDragRCubeHandler() {
    rotationElapsedTime = 0;
    isDragging = true;
    targetRotation = CurrentRotation();
  }

  private void EndDragRCubeHandler() {
    isDragging = false;
    targetRotation = GetNearestRotation();
  }

  protected override Quaternion CurrentRotation() => transform.localRotation;

    #endregion
}