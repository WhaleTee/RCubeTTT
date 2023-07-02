using System;
using UnityEngine;

public class RCubeSide3X3 {
  public GameObject[] cubePieces { get; private set; }
  public GameObject centerSideCubePiece { get; private set; }
  public Side side { get; private set; }

  public RCubeSide3X3(GameObject[] cubePieces, Side side) {
    if (cubePieces.Length is > 9 or < 9) {
      throw new ArgumentException("Accepted only 3x3 Rubik's cube.");
    }

    this.cubePieces = cubePieces;
    centerSideCubePiece = cubePieces[cubePieces.Length / 2];
    this.side = side;
  }

  public void RotateSideClockwise() {
    
  }
  
  public void RotateSideCounterclockwise() {
    
  }

  public enum Side {
    Top,
    Down,
    Left,
    Right,
    Front,
    Back
  }
}