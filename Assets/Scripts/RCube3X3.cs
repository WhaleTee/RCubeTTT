using System;
using UnityEngine;

public class RCube3X3 {
  #region fields

  private const int CUBE_SIZE = 3;

  private RCubePiece[,,] cube;
  public GameObject rCube { get; private set; }
  public RCubeSide upSide { get; private set; }
  public RCubeSide downSide { get; private set; }
  public RCubeSide leftSide { get; private set; }
  public RCubeSide rightSide { get; private set; }
  public RCubeSide frontSide { get; private set; }
  public RCubeSide backSide { get; private set; }
  public RCubePiece CenterPiece { get; private set; }

  #endregion

  #region properties

  #endregion

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

          cube[x, y, z] = new RCubePiece(
            rCube,
            cubePosition,
            Vector3.one * cubePieceSize,
            Quaternion.identity
          ) { GameObject = { name = $"[{x},{y},{z}]" } };
        }
      }
    }

    InitSides();

    CalculateSides();

    foreach (var piece in upSide.Pieces) {
      piece.EnableSide(PrimitiveCube.Side.Up).SetColor(topSideColor);
    }

    foreach (var piece in downSide.Pieces) {
      piece.EnableSide(PrimitiveCube.Side.Down).SetColor(downSideColor);
    }

    foreach (var piece in leftSide.Pieces) {
      piece.EnableSide(PrimitiveCube.Side.Left).SetColor(leftSideColor);
    }

    foreach (var piece in rightSide.Pieces) {
      piece.EnableSide(PrimitiveCube.Side.Right).SetColor(rightSideColor);
    }

    foreach (var piece in frontSide.Pieces) {
      piece.EnableSide(PrimitiveCube.Side.Front).SetColor(frontSideColor);
    }

    foreach (var piece in backSide.Pieces) {
      piece.EnableSide(PrimitiveCube.Side.Back).SetColor(backSideColor);
    }
  }

  private void InitSides() {
    upSide = new RCubeSide(PrimitiveCube.Side.Up);
    downSide = new RCubeSide(PrimitiveCube.Side.Down);
    leftSide = new RCubeSide(PrimitiveCube.Side.Left);
    rightSide = new RCubeSide(PrimitiveCube.Side.Right);
    frontSide = new RCubeSide(PrimitiveCube.Side.Front);
    backSide = new RCubeSide(PrimitiveCube.Side.Back);
  }

  public void CalculateSides() {
    upSide.Pieces = new[] {
      cube[0, 2, 0],
      cube[1, 2, 0],
      cube[2, 2, 0],
      cube[0, 2, 1],
      cube[1, 2, 1],
      cube[2, 2, 1],
      cube[0, 2, 2],
      cube[1, 2, 2],
      cube[2, 2, 2]
    };

    downSide.Pieces = new[] {
      cube[0, 0, 0],
      cube[1, 0, 0],
      cube[2, 0, 0],
      cube[0, 0, 1],
      cube[1, 0, 1],
      cube[2, 0, 1],
      cube[0, 0, 2],
      cube[1, 0, 2],
      cube[2, 0, 2]
    };

    leftSide.Pieces = new[] {
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

    rightSide.Pieces = new[] {
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

    frontSide.Pieces = new[] {
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

    backSide.Pieces = new[] {
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
    var cloneCube = (RCubePiece[,,])cube.Clone();

    switch (side) {
      case PrimitiveCube.Side.Up:
        for (var z = 0; z < CUBE_SIZE; z++) {
          for (var x = 0; x < CUBE_SIZE; x++) {
            cloneCube[clockwise ? 2 - z : z, 2, clockwise ? x : 2 - x] = cube[x, 2, z];
          }
        }

        break;

      case PrimitiveCube.Side.Down:
        for (var z = 0; z < CUBE_SIZE; z++) {
          for (var x = 0; x < CUBE_SIZE; x++) {
            cloneCube[clockwise ? 2 - z : z, 0, clockwise ? x : 2 - x] = cube[x, 0, z];
          }
        }

        break;

      case PrimitiveCube.Side.Left:
        for (var y = 0; y < CUBE_SIZE; y++) {
          for (var z = 0; z < CUBE_SIZE; z++) {
            cloneCube[0, clockwise ? z : 2 - z, clockwise ? 2 - y : y] = cube[0, y, z];
          }
        }

        break;

      case PrimitiveCube.Side.Right:
        for (var y = 0; y < CUBE_SIZE; y++) {
          for (var z = 0; z < CUBE_SIZE; z++) {
            cloneCube[2, clockwise ? z : 2 - z, clockwise ? 2 - y : y] = cube[2, y, z];
          }
        }

        break;

      case PrimitiveCube.Side.Front:
        for (var x = 0; x < CUBE_SIZE; x++) {
          for (var y = 0; y < CUBE_SIZE; y++) {
            cloneCube[clockwise ? y : 2 - y, clockwise ? 2 - x : x, 2] = cube[x, y, 2];
          }
        }

        break;

      case PrimitiveCube.Side.Back:
        for (var x = 0; x < CUBE_SIZE; x++) {
          for (var y = 0; y < CUBE_SIZE; y++) {
            cloneCube[clockwise ? y : 2 - y, clockwise ? 2 - x : x, 0] = cube[x, y, 0];
          }
        }

        break;
      default: throw new ArgumentOutOfRangeException(nameof(side), side, null);
    }

    cube = cloneCube;

    CalculateSides();
  }
}