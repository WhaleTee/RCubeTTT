using UnityEngine;

public class CubeSideController : RotationController {
  #region serializable fields

  [SerializeField]
  [RangeVector(new float[] { 0, 0, 0 }, new float[] { 1, 1, 1 })]
  private Vector3Int accessRotation;

  #endregion

  #region methods

  protected override void Rotate() { }

  #endregion
}