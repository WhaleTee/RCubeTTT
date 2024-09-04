using UnityEngine;

namespace RCubeTTT.Commons
{
  /// <summary>
  /// Assigns pieces to a Rubik's Cube face.
  /// </summary>
  public class RCubeFacePiecesAssigner {
    #region fields

    private const int MAX_EXPECTED_SIDE_PIECES_COUNT = 9;

    private readonly BoxCastScanner boxCastScanner;
    private readonly LayerMask cubePieceLayer;

    #endregion

    #region constructors

    /// <summary>
    /// Initializes a new instance of the RCubeFacePiecesAssigner class.
    /// </summary>
    /// <param name="boxCastScanner">The BoxColliderObjectScanner used to scan for pieces.</param>
    /// <param name="cubePieceLayer">The layer mask representing the cube pieces.</param>
    public RCubeFacePiecesAssigner(BoxCastScanner boxCastScanner, LayerMask cubePieceLayer) {
      this.boxCastScanner = boxCastScanner;
      this.cubePieceLayer = cubePieceLayer;
    }

    #endregion

    #region methods

    /// <summary>
    /// Finds and assigns pieces to the specified face.
    /// </summary>
    /// <param name="face">The GameObject representing the Rubik's Cube face.</param>
    public void FindAndAssignPiecesToFace(GameObject face) {
      foreach (var p in boxCastScanner.ScanForLayer(MAX_EXPECTED_SIDE_PIECES_COUNT, cubePieceLayer)) {
        p.transform.SetParent(face.transform);
      }
    }

    #endregion
  }
}