using UnityEngine;

public class FixedCubeFaceRotation : FixedRotation {
  #region fields

  private bool isDragging;
  private Quaternion targetRotation;
  private float rotationElapsedTime;

  #endregion

  #region unity methods

  private void Awake() {
    targetRotation = CurrentRotation();
    EventManager.AddStartDragRCubeFaceListener(StartDragRCubeFaceHandler);
    EventManager.AddEndDragRCubeFaceListener(EndDragRCubeFaceHandler);
  }

  private void Update() {
    if (!isDragging) {
      transform.localRotation = Quaternion.Slerp(CurrentRotation(), targetRotation, rotationElapsedTime / rotateDuration);
      rotationElapsedTime += Time.deltaTime;
    }
  }

  #endregion

  #region methods

  private void StartDragRCubeFaceHandler() {
    rotationElapsedTime = 0;
    isDragging = true;
    targetRotation = CurrentRotation();
  }

  private void EndDragRCubeFaceHandler() {
    isDragging = false;
    targetRotation = GetNearestRotation();
  }

  protected override Quaternion CurrentRotation() => transform.localRotation;

  #endregion
}