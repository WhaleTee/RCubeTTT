using System;
using UnityEngine;

public class RCube3X3 {
  private const int CUBE_SIZE = 3;

  private RCubePiece[,,] cube;
  public GameObject rCube { get; private set; }
  public RCubePiece[] topSide { get; private set; }
  public RCubePiece[] downSide { get; private set; }
  public RCubePiece[] leftSide { get; private set; }
  public RCubePiece[] rightSide { get; private set; }
  public RCubePiece[] frontSide { get; private set; }
  public RCubePiece[] backSide { get; private set; }

  public RCube3X3(
    GameObject rCube,
    float cubePieceSize,
    Color topSideColor,
    Color downSideColor,
    Color leftSideColor,
    Color rightSideColor,
    Color frontSideColor,
    Color backSideColor
  ) {
    this.rCube = rCube;

    cube = new RCubePiece[CUBE_SIZE, CUBE_SIZE, CUBE_SIZE];

    for (var x = 0; x < CUBE_SIZE; x++) {
      for (var y = 0; y < CUBE_SIZE; y++) {
        for (var z = 0; z < CUBE_SIZE; z++) {
          var cubePosition = new Vector3(
            x * cubePieceSize,
            y * cubePieceSize,
            z * cubePieceSize
          );

          cube[x, y, z] = new RCubePiece(rCube, cubePosition, Vector3.one * cubePieceSize, Quaternion.identity);
        }
      }
    }

    CalculateSides();

    foreach (var piece in topSide) {
      piece.InitSide(PrimitiveCube.Side.Top).SetColor(topSideColor);
    }

    foreach (var piece in downSide) {
      piece.InitSide(PrimitiveCube.Side.Down).SetColor(downSideColor);
    }

    foreach (var piece in leftSide) {
      piece.InitSide(PrimitiveCube.Side.Left).SetColor(leftSideColor);
    }

    foreach (var piece in rightSide) {
      piece.InitSide(PrimitiveCube.Side.Right).SetColor(rightSideColor);
    }

    foreach (var piece in frontSide) {
      piece.InitSide(PrimitiveCube.Side.Front).SetColor(frontSideColor);
    }

    foreach (var piece in backSide) {
      piece.InitSide(PrimitiveCube.Side.Back).SetColor(backSideColor);
    }
  }

  public void CalculateSides() {
    topSide = new[] {
      cube[0, 2, 0],
      cube[0, 2, 1],
      cube[0, 2, 2],
      cube[1, 2, 0],
      cube[1, 2, 1],
      cube[1, 2, 2],
      cube[2, 2, 0],
      cube[2, 2, 1],
      cube[2, 2, 2]
    };

    downSide = new[] {
      cube[0, 0, 0],
      cube[0, 0, 1],
      cube[0, 0, 2],
      cube[1, 0, 0],
      cube[1, 0, 1],
      cube[1, 0, 2],
      cube[2, 0, 0],
      cube[2, 0, 1],
      cube[2, 0, 2]
    };

    leftSide = new[] {
      cube[0, 0, 0],
      cube[0, 0, 1],
      cube[0, 0, 2],
      cube[0, 1, 0],
      cube[0, 1, 1],
      cube[0, 1, 2],
      cube[0, 2, 0],
      cube[0, 2, 1],
      cube[0, 2, 2]
    };

    rightSide = new[] {
      cube[2, 0, 0],
      cube[2, 0, 1],
      cube[2, 0, 2],
      cube[2, 1, 0],
      cube[2, 1, 1],
      cube[2, 1, 2],
      cube[2, 2, 0],
      cube[2, 2, 1],
      cube[2, 2, 2]
    };

    frontSide = new[] {
      cube[0, 0, 2],
      cube[0, 1, 2],
      cube[0, 2, 2],
      cube[1, 0, 2],
      cube[1, 1, 2],
      cube[1, 2, 2],
      cube[2, 0, 2],
      cube[2, 1, 2],
      cube[2, 2, 2]
    };

    backSide = new[] {
      cube[0, 0, 0],
      cube[0, 1, 0],
      cube[0, 2, 0],
      cube[1, 0, 0],
      cube[1, 1, 0],
      cube[1, 2, 0],
      cube[2, 0, 0],
      cube[2, 1, 0],
      cube[2, 2, 0]
    };
  }

  public void RotateSides(PrimitiveCube.Side side, bool clockwise) {
    switch (side) {
      case PrimitiveCube.Side.Top:
      case PrimitiveCube.Side.Down:
        break;

      case PrimitiveCube.Side.Left:
      case PrimitiveCube.Side.Right:
        var cloneCube = (RCubePiece[,,])cube.Clone();

        for (var y = 0; y < CUBE_SIZE; y++) {
          for (var z = 0; z < CUBE_SIZE; z++) {
            cloneCube[0, 2 - z, y] = cube[0, y, z];
          }
        }

        cube = cloneCube;
        CalculateSides();
        
        break;
      case PrimitiveCube.Side.Front:
      case PrimitiveCube.Side.Back:
        break;
      default: throw new ArgumentOutOfRangeException(nameof(side), side, null);
    }
  }
}