using UnityEngine;

[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeFacePiecesController : MonoBehaviour {
  #region fields

  private RCubeFacePiecesAssigner rCubeFacePiecesAssigner;

  private ObjectScanner objectScanner;
  private GlobalIdentifier globalIdentifier;
  private int cubePieceLayer;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddRCubeFaceRotationStartListener(OnFaceRotationStarted);

    objectScanner = GetComponent<ObjectScanner>();
    globalIdentifier = GetComponent<GlobalIdentifier>();

    cubePieceLayer = LayerMask.GetMask("CubePiece");
    rCubeFacePiecesAssigner = new RCubeFacePiecesAssigner(objectScanner, cubePieceLayer);
  }

  #endregion

  #region methods

  private void OnFaceRotationStarted(string faceGlobalId) {
    if (IsThisFaceId(faceGlobalId)) {
      rCubeFacePiecesAssigner.FindAndAssignPiecesToFace(gameObject);
    }
  }

  private bool IsThisFaceId(string faceGlobalId) => faceGlobalId.Equals(globalIdentifier.id);

  #endregion
}