using System;
using UnityEngine;

public class RCubeController : MonoBehaviour {
  [SerializeField]
  private float cubePieceSize;

  [SerializeField]
  private Color[] colors;

  private RCube3X3 rCube3X3;

  private void Start() {
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
    if (Input.GetKeyDown(KeyCode.Space)) {
      RotateSide(PrimitiveCube.Side.Back, true);
    }
  }

  private void RotateSide(PrimitiveCube.Side side, bool clockwise) {
    Vector3 rotationAxis;
    RCubePiece[] cubeSide;

    switch (side) {
      case PrimitiveCube.Side.Top:
        rotationAxis = clockwise ? Vector3.right : Vector3.left;
        cubeSide = rCube3X3.topSide;
        break;
      case PrimitiveCube.Side.Down: 
        rotationAxis = clockwise ? Vector3.right : Vector3.left;
        cubeSide = rCube3X3.downSide;
        break;;
      case PrimitiveCube.Side.Left: 
        rotationAxis = clockwise ? Vector3.forward : Vector3.back;
        cubeSide = rCube3X3.leftSide;
        break;;
      case PrimitiveCube.Side.Right: 
        rotationAxis = clockwise ? Vector3.forward : Vector3.back;
        cubeSide = rCube3X3.rightSide;
        break;;
      case PrimitiveCube.Side.Front: 
        rotationAxis = clockwise ? Vector3.up : Vector3.down;
        cubeSide = rCube3X3.frontSide;
        break;;
      case PrimitiveCube.Side.Back: 
        rotationAxis = clockwise ? Vector3.up : Vector3.down;
        cubeSide = rCube3X3.backSide;
        break;;
      default: throw new ArgumentOutOfRangeException(nameof(side), side, null);
    }

    foreach (var cubePiece in cubeSide) {
      cubePiece.gameObject.transform.RotateAround(
        cubeSide[4].gameObject.transform.position,
        rotationAxis,
        90f
      );
    }
  }
}