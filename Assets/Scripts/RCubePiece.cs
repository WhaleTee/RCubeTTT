using UnityEngine;

public class RCubePiece {
  public GameObject pieceObject { get; private set; }
  public TicTac ticTac { get; set; }

  public RCubePiece(GameObject pieceObject) {
    this.pieceObject = pieceObject;
  }
}