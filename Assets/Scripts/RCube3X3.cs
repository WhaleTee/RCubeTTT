using UnityEngine;

public class RCube3X3 {
  private const int CUBE_SIZE = 3;

  private RCubePiece[,,] pieces;
  public GameObject rCube { get; private set; }

  public RCube3X3(
    GameObject rCube,
    GameObject cubePiecePrefab,
    float cubePieceSize,
    Color topSideColor,
    Color downSideColor,
    Color leftSideColor,
    Color rightSideColor,
    Color frontSideColor,
    Color backSideColor
  ) {
    this.rCube = rCube;

    pieces = new RCubePiece[CUBE_SIZE, CUBE_SIZE, CUBE_SIZE];

    for (var x = 0; x < CUBE_SIZE; x++) {
      for (var y = 0; y < CUBE_SIZE; y++) {
        for (var z = 0; z < CUBE_SIZE; z++) {
          var cubePosition = new Vector3(
            x * cubePieceSize,
            y * cubePieceSize,
            z * cubePieceSize
          );

          pieces[x, y, z] = new RCubePiece(Object.Instantiate(cubePiecePrefab, cubePosition, Quaternion.identity));
          pieces[x, y, z].pieceObject.transform.parent = rCube.transform;
        }
      }
    }
    
    ColorTopSide(pieces, topSideColor);
    ColorDownSide(pieces, downSideColor);
    ColorLeftSide(pieces, leftSideColor);
    ColorRightSide(pieces, rightSideColor);
    ColorFrontSide(pieces, frontSideColor);
    ColorBackSide(pieces, backSideColor);
  }

  private void ColorTopSide(RCubePiece[,,] cubePieces, Color color) {
    var topSidePieces = new[] {
      cubePieces[0, 2, 0],
      cubePieces[0, 2, 1],
      cubePieces[0, 2, 2],
      cubePieces[1, 2, 0],
      cubePieces[1, 2, 1],
      cubePieces[1, 2, 2],
      cubePieces[2, 2, 0],
      cubePieces[2, 2, 1],
      cubePieces[2, 2, 2]
    };

    foreach (var piece in topSidePieces) {
      piece.pieceObject.transform.Find("top-side").GetComponent<Renderer>().material.color = color;
    }
  }

  private void ColorDownSide(RCubePiece[,,] cubePieces, Color color) {
    var downSidePieces = new[] {
      cubePieces[0, 0, 0],
      cubePieces[0, 0, 1],
      cubePieces[0, 0, 2],
      cubePieces[1, 0, 0],
      cubePieces[1, 0, 1],
      cubePieces[1, 0, 2],
      cubePieces[2, 0, 0],
      cubePieces[2, 0, 1],
      cubePieces[2, 0, 2]
    };

    foreach (var piece in downSidePieces) {
      piece.pieceObject.transform.Find("down-side").GetComponent<Renderer>().material.color = color;
    }
  }

  private void ColorLeftSide(RCubePiece[,,] cubePieces, Color color) {
    var leftSidePieces = new[] {
      cubePieces[0, 0, 0],
      cubePieces[0, 0, 1],
      cubePieces[0, 0, 2],
      cubePieces[0, 1, 0],
      cubePieces[0, 1, 1],
      cubePieces[0, 1, 2],
      cubePieces[0, 2, 0],
      cubePieces[0, 2, 1],
      cubePieces[0, 2, 2]
    };

    foreach (var piece in leftSidePieces) {
      piece.pieceObject.transform.Find("left-side").GetComponent<Renderer>().material.color = color;
    }
  }

  private void ColorRightSide(RCubePiece[,,] cubePieces, Color color) {
    var rightSidePieces = new[] {
      cubePieces[2, 0, 0],
      cubePieces[2, 0, 1],
      cubePieces[2, 0, 2],
      cubePieces[2, 1, 0],
      cubePieces[2, 1, 1],
      cubePieces[2, 1, 2],
      cubePieces[2, 2, 0],
      cubePieces[2, 2, 1],
      cubePieces[2, 2, 2]
    };

    foreach (var piece in rightSidePieces) {
      piece.pieceObject.transform.Find("right-side").GetComponent<Renderer>().material.color = color;
    }
  }

  private void ColorFrontSide(RCubePiece[,,] cubePieces, Color color) {
    var frontSidePieces = new[] {
      cubePieces[0, 0, 2],
      cubePieces[0, 1, 2],
      cubePieces[0, 2, 2],
      cubePieces[1, 0, 2],
      cubePieces[1, 1, 2],
      cubePieces[1, 2, 2],
      cubePieces[2, 0, 2],
      cubePieces[2, 1, 2],
      cubePieces[2, 2, 2]
    };

    foreach (var piece in frontSidePieces) {
      piece.pieceObject.transform.Find("front-side").GetComponent<Renderer>().material.color = color;
    }
  }

  private void ColorBackSide(RCubePiece[,,] cubePieces, Color color) {
    var backSidePieces = new[] {
      cubePieces[0, 0, 0],
      cubePieces[0, 1, 0],
      cubePieces[0, 2, 0],
      cubePieces[1, 0, 0],
      cubePieces[1, 1, 0],
      cubePieces[1, 2, 0],
      cubePieces[2, 0, 0],
      cubePieces[2, 1, 0],
      cubePieces[2, 2, 0]
    };

    foreach (var piece in backSidePieces) {
      piece.pieceObject.transform.Find("back-side").GetComponent<Renderer>().material.color = color;
    }
  }
  
  
}