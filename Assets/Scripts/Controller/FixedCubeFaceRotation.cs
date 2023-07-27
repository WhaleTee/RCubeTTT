using UnityEngine;

public class FixedCubeFaceRotation : FixedRotation {
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
    EventManager.AddStartDragRCubeFaceListener(StartDragRCubeFaceHandler);
    EventManager.AddEndDragRCubeFaceListener(EndDragRCubeFaceHandler);
  }

  private void Update() {
    if (!dragging) {
      transform.localRotation = Quaternion.Slerp(globalRotation, targetRotation, rotationElapsedTime / rotationDuration);
      rotationElapsedTime += Time.deltaTime;
    }
  }

  #endregion

  #region methods

  private void StartDragRCubeFaceHandler() {
    rotationElapsedTime = 0;
    dragging = true;
    targetRotation = globalRotation;
  }

  private void EndDragRCubeFaceHandler() {
    dragging = false;
    targetRotation = GetNearestRotation();
  }

  #endregion
}