using System;
using UnityEngine;

[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeFacePiecesController : MonoBehaviour {
  #region fields

  private RCubeFacePiecesAssigner rCubeFacePiecesAssigner;
  private bool isDragging;
  private bool needToAssignPiecesToFace;
  private bool needToReassignPiecesToFace;

  private ObjectScanner objectScanner;
  private GlobalIdentifier globalIdentifier;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddStartRCubeFaceRotationListener(OnFaceRotationStarted);
    EventManager.AddEndRCubeFaceRotationListener(OnFaceRotationEnded);
    EventManager.AddStartDragRCubeFaceListener(() => isDragging = true);
    EventManager.AddEndDragRCubeFaceListener(() => isDragging = false);

    objectScanner = GetComponent<ObjectScanner>();
    globalIdentifier = GetComponent<GlobalIdentifier>();

    rCubeFacePiecesAssigner = new RCubeFacePiecesAssigner(objectScanner, LayerMask.GetMask("CubePiece"));
  }

  // private void LateUpdate() {
  //   if (needToAssignPiecesToFace) {
  //     throw new NotImplementedException();
  //   }
  // }

  #endregion

  #region public methods

  private void OnFaceRotationStarted(string faceGlobalId) {
    if (faceGlobalId.Equals(globalIdentifier.id)) {
      rCubeFacePiecesAssigner.FindAndAssignPiecesToFace(gameObject);
    }
  }

  private void OnFaceRotationEnded(string faceGlobalId) {
    if (faceGlobalId.Equals(globalIdentifier.id) && !isDragging) {
      rCubeFacePiecesAssigner.FindAndAssignPiecesToFace(transform.parent.gameObject);
    }
  }

  #endregion
}