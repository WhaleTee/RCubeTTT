using RCubeTTT.Commons;
using RCubeTTT.EventSystem;
using RCubeTTT.EventSystem.EventContext;
using UnityEngine;

namespace RCubeTTT.Controller
{
  /// <summary>
  /// Controls the behavior of the face pieces on a Rubik's Cube face.
  /// </summary>
  [RequireComponent(typeof(BoxCastScanner))]
  public class RCubeFacePiecesController : MonoBehaviour {
    #region fields

    private BoxCastScanner boxCastScanner;
    private RCubeSideDragRotation sideDragRotation;
    private int cubePieceLayer;

    private RCubeFacePiecesAssigner rCubeFacePiecesAssigner;

    #endregion

    #region unity methods

    private void Awake() {
      RCubeFaceEventManager.AddRotationStartListener(OnRCubeFaceRotationStart);

      boxCastScanner = GetComponent<BoxCastScanner>();
      sideDragRotation = GetComponent<RCubeSideDragRotation>();
      cubePieceLayer = LayerMask.GetMask("CubePiece");

      rCubeFacePiecesAssigner = new RCubeFacePiecesAssigner(boxCastScanner, cubePieceLayer);
    }

    #endregion

    #region methods

    /// <summary>
    /// Handles the start of rotation for a specific Rubik's Cube face.
    /// If the face's global identifier matches the current global identifier,
    /// it finds and assigns the pieces to the current face.
    /// </summary>
    private void OnRCubeFaceRotationStart(RotationEventContext context) {
      if (context.instanceId == sideDragRotation.GetInstanceID()) {
        rCubeFacePiecesAssigner.FindAndAssignPiecesToFace(gameObject);
      }
    }

    #endregion
  }
}