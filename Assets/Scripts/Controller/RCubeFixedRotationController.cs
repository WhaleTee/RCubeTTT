using System.Collections;
using UnityEngine;

public class RCubeFixedRotationController : FixedRotationController {
  #region fields

  private Quaternion targetRotation;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddRCubeDragStartListener(StartDragRCubeHandler);
    EventManager.AddRCubeDragEndListener(EndDragRCubeHandler);

    targetRotation = GetCurrentRotation();
  }

  #endregion

  #region methods

  private IEnumerator Rotate() {
    while (!GetCurrentRotation().Equals(targetRotation)) {
      transform.localRotation = Quaternion.RotateTowards(GetCurrentRotation(), targetRotation, speed);
      yield return null;
    }
  }

  private void StartDragRCubeHandler() {
    StopCoroutine(Rotate());
    targetRotation = GetCurrentRotation();
  }

  private void EndDragRCubeHandler() {
    StartCoroutine(Rotate());
    targetRotation = GetNearestRotation();
  }

  protected override Quaternion GetCurrentRotation() => transform.localRotation;

  #endregion
}