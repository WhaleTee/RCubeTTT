using UnityEngine;

public class RCubeFixedRotationController : FixedRotationController {
  #region fields

  private bool isDragging;
  private Quaternion targetRotation;
  private float rotationElapsedTime;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddRCubeDragStartListener(StartDragRCubeHandler);
    EventManager.AddRCubeDragEndListener(EndDragRCubeHandler);

    targetRotation = GetCurrentRotation();
  }

  private void Update() {
    if (!isDragging) {
      transform.localRotation = Quaternion.Slerp(GetCurrentRotation(), targetRotation, rotationElapsedTime / rotateDuration);
      rotationElapsedTime += Time.deltaTime;
    }
  }

  #endregion

  #region methods

  private void StartDragRCubeHandler() {
    rotationElapsedTime = 0;
    isDragging = true;
    targetRotation = GetCurrentRotation();
  }

  private void EndDragRCubeHandler() {
    isDragging = false;
    targetRotation = GetNearestRotation();
  }

  protected override Quaternion GetCurrentRotation() => transform.localRotation;

  #endregion
}