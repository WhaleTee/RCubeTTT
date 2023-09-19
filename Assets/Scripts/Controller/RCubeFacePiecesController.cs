using UnityEngine;

/// <summary>
/// Controls the behavior of the face pieces on a Rubik's Cube face.
/// </summary>
[RequireComponent(typeof(BoxCastScanner))]
public class RCubeFacePiecesController : MonoBehaviour {
  #region serializable fields

  [SerializeField]
  private GlobalIdentifier faceIdentifier;

  #endregion

  #region fields

  private BoxCastScanner boxCastScanner;
  private int cubePieceLayer;

  private RCubeFacePiecesAssigner rCubeFacePiecesAssigner;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddRCubeFaceRotationStartListener(OnRCubeFaceRotationStart);

    boxCastScanner = GetComponent<BoxCastScanner>();
    cubePieceLayer = LayerMask.GetMask("CubePiece");

    rCubeFacePiecesAssigner = new RCubeFacePiecesAssigner(boxCastScanner, cubePieceLayer);
  }

  #endregion

  #region methods

  /// <summary>
  /// Handles the start of rotation for a specific Rubik's Cube face.
  /// If the face's global identifier matches the current global identifier,
  /// it finds and assigns the pieces pieces to the current face.
  /// </summary>
  /// <param name="context">The context of the <see cref="RCubeFaceRotationStartEvent"/>.</param>
  private void OnRCubeFaceRotationStart(RCubeFaceRotationStartEventContext context) {
    if (context.faceGlobalId.Equals(faceIdentifier.id)) {
      rCubeFacePiecesAssigner.FindAndAssignPiecesToFace(gameObject);
    }
  }

  #endregion
}