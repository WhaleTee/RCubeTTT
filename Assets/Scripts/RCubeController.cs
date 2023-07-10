using System;
using UnityEngine;

public class RCubeController : MonoBehaviour {
  [SerializeField]
  private float cubePieceSize;

  [SerializeField]
  private Color[] colors;

  private RCube3X3 rCube3X3;

  private int lastSide;
  private PrimitiveCube.Side sideToRotate;

  #region unity methods

  private void Start() {
    gameObject.transform.position = new Vector3(1, 1, 1);

    rCube3X3 = new RCube3X3(
      gameObject,
      cubePieceSize,
      colors[0],
      colors[1],
      colors[2],
      colors[3],
      colors[4],
      colors[5]
    );
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.W)) {
      NextSide();
    }

    if (Input.GetKeyDown(KeyCode.Space)) {
      RotateSide(sideToRotate, true);
    }
  }

  private void FixedUpdate() { }

  private void OnMouseDrag() {
    
  }

  #endregion

  private void NextSide() {
    var i = lastSide >= 5 ? lastSide = 0 : ++lastSide;
    lastSide = i;
    sideToRotate = PrimitiveCube.SideExtension.FindByOrdinal(lastSide);
    Debug.Log(sideToRotate);
  }

  private void RotateSide(PrimitiveCube.Side side, bool clockwise) {
    Vector3 rotationAxis;
    RCubePiece[] cubeSide;

    switch (side) {
      case PrimitiveCube.Side.Up:
        rotationAxis = clockwise ? Vector3.down : Vector3.up;
        cubeSide = rCube3X3.upSide.Pieces;
        break;
      case PrimitiveCube.Side.Down:
        rotationAxis = clockwise ? Vector3.down : Vector3.up;
        cubeSide = rCube3X3.downSide.Pieces;
        break;

      case PrimitiveCube.Side.Left:
        rotationAxis = clockwise ? Vector3.left : Vector3.right;
        cubeSide = rCube3X3.leftSide.Pieces;
        break;

      case PrimitiveCube.Side.Right:
        rotationAxis = clockwise ? Vector3.left : Vector3.right;
        cubeSide = rCube3X3.rightSide.Pieces;
        break;

      case PrimitiveCube.Side.Front:
        rotationAxis = clockwise ? Vector3.back : Vector3.forward;
        cubeSide = rCube3X3.frontSide.Pieces;
        break;

      case PrimitiveCube.Side.Back:
        rotationAxis = clockwise ? Vector3.back : Vector3.forward;
        cubeSide = rCube3X3.backSide.Pieces;
        break;

      default: throw new ArgumentOutOfRangeException(nameof(side), side, null);
    }

    foreach (var cubePiece in cubeSide) {
      cubePiece.GameObject.transform.RotateAround(
        cubeSide[4].GameObject.transform.position,
        rotationAxis,
        90f
      );
    }

    rCube3X3.RotateSides(side, clockwise);
  }
}