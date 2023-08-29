using MyBox;
using UnityEngine;

public class RCubeFacePiecesAssigner {
  #region fields

  private const int MAX_EXPECTED_SIDE_PIECES_COUNT = 9;

  private readonly ObjectScanner objectScanner;
  private readonly LayerMask cubePieceLayer;

  #endregion

  #region constructors

  public RCubeFacePiecesAssigner(ObjectScanner objectScanner, LayerMask cubePieceLayer) {
    this.objectScanner = objectScanner;
    this.cubePieceLayer = cubePieceLayer;
  }

  #endregion

  #region methods

  public void FindAndAssignPiecesToFace(GameObject parent) =>
  objectScanner.ScanForLayer(MAX_EXPECTED_SIDE_PIECES_COUNT, cubePieceLayer).ForEach(p => p.transform.SetParent(parent.transform));

  #endregion
}