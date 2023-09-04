using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeFaceFixedRotationController : FixedRotationController {
  #region fields
  
  private Quaternion targetRotation;

  private GlobalIdentifier globalIdentifier;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddRCubeFaceDragStartListener(StartDragRCubeFaceHandler);
    EventManager.AddRCubeFaceDragEndListener(EndDragRCubeFaceHandler);

    targetRotation = GetCurrentRotation();

    globalIdentifier = GetComponent<GlobalIdentifier>();
  }

  #endregion

  #region methods

  private IEnumerator Rotate() {
    while (!GetCurrentRotation().Equals(targetRotation)) {
      transform.localRotation = Quaternion.RotateTowards(GetCurrentRotation(), targetRotation, speed);
      yield return null;
    }
  }

  private void StartDragRCubeFaceHandler(string faceGlobalId) {
    if (IsThisFaceId(faceGlobalId)) {
      targetRotation = GetCurrentRotation();
      StopCoroutine(Rotate());
    }
  }

  private void EndDragRCubeFaceHandler(string faceGlobalId) {
    if (IsThisFaceId(faceGlobalId)) {
      targetRotation = GetNearestRotation();
      StartCoroutine(Rotate());
    }
  }

  protected override Quaternion GetCurrentRotation() => transform.localRotation;

  private bool IsThisFaceId(string faceGlobalId) => faceGlobalId.Equals(globalIdentifier.id);

  #endregion
}