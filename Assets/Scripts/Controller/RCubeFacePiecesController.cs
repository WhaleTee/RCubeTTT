using UnityEngine;

/// <summary>
/// Controls the behavior of the face pieces on a Rubik's Cube face.
/// </summary>
[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeFacePiecesController : MonoBehaviour {
  #region fields

  private BoxCastScanner boxCastScanner;
  private GlobalIdentifier globalIdentifier;
  private int cubePieceLayer;

  private RCubeFacePiecesAssigner rCubeFacePiecesAssigner;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddRCubeFaceRotationStartListener(OnRCubeFaceRotationStart);

    boxCastScanner = GetComponent<BoxCastScanner>();
    globalIdentifier = GetComponent<GlobalIdentifier>();
    cubePieceLayer = LayerMask.GetMask("CubePiece");

    rCubeFacePiecesAssigner = new RCubeFacePiecesAssigner(boxCastScanner, cubePieceLayer);
  }

  #endregion

  #region methods

  /// <summary>
  /// Called when a face rotation starts.
  /// </summary>
  /// <param name="faceGlobalId">The context of the <see cref="RCubeFaceRotationStartEvent"/> that represents Rubik's Cube's face global UUID.</param>
  private void OnRCubeFaceRotationStart(string faceGlobalId) {
    if (faceGlobalId.Equals(globalIdentifier.id)) {
      rCubeFacePiecesAssigner.FindAndAssignPiecesToFace(gameObject);
    }
  }

  #endregion
}