using UnityEngine;

public class RCubeSide : GameObjectHolder {
  #region fields

  private readonly GameObject gameObject;

  private RCubePiece[] pieces;

  private RCubePiece centerPiece;

  #endregion

  #region properties

  public RCubePiece[] Pieces {
    get => pieces;
    set {
      pieces = value;

      centerPiece = value[value.Length / 2];
    }
  }

  public RCubePiece CenterPiece => centerPiece;

  #endregion

  #region contructors

  public RCubeSide(PrimitiveCube.Side side) {
  }

  #endregion

  #region methods

  public GameObject GetGameObject() {
    return gameObject;
  }

  #endregion
}