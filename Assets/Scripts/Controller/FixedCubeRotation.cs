using UnityEngine;

public class FixedCubeRotation : FixedRotation {
  #region fields
  private bool dragging;
  private Quaternion targetRotation = Quaternion.identity;
  private float rotationElapsedTime;

  #endregion
  #region properties

  private Quaternion globalRotation => transform.rotation;

  #endregion
  #region unity methods

  private void Awake() {
    EventManager.AddStartDragRCubeListener(StartDragRCubeHandler);
    EventManager.AddEndDragRCubeListener(EndDragRCubeHandler);
  }

  private void Update() {
    if (!dragging) {
      transform.localRotation = Quaternion.Slerp(globalRotation, targetRotation, rotationElapsedTime / rotationDuration);
      rotationElapsedTime += Time.deltaTime;
    }
  }

  #endregion

  #region methods

  private void StartDragRCubeHandler() {
    rotationElapsedTime = 0;
    dragging = true;
    targetRotation = globalRotation;
  }

  private void EndDragRCubeHandler() {
    dragging = false;
    targetRotation = GetNearestRotation();
  }

  #endregion
}