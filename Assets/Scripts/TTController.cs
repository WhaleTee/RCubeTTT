using System;
using UnityEngine;

public class TTController : MonoBehaviour {
  private GameObject x;
  private GameObject o;

  private GameObject currentX;
  private GameObject currentO;

  private TicTacSign.TicTac Sign { get; set; } = TicTacSign.TicTac.NoSign;
  public PrimitiveCube.Side Side { get; set; }

  private void Awake() {
    x = Resources.Load("Prefabs/x") as GameObject;
    o = Resources.Load("Prefabs/o") as GameObject;
  }

  private void OnMouseUpAsButton() {
    switch (Sign) {
      case TicTacSign.TicTac.NoSign:
        Sign = TicTacSign.TicTac.X;
        currentX = Instantiate(x, gameObject.transform, false);
        currentX.transform.localPosition = CalculatePosition();
        RotateToSide();
        // currentX.transform.localScale = new Vector3(1, 1, 1);
        break;
      case TicTacSign.TicTac.X:
        Destroy(currentX);
        Sign = TicTacSign.TicTac.O;
        currentO = Instantiate(o, gameObject.transform, false);
        currentO.transform.localPosition = CalculatePosition();
        RotateToSide();
        // currentO.transform.localScale = new Vector3(33, 33, 1);
        break;
      case TicTacSign.TicTac.O:
        Destroy(currentO);
        Sign = TicTacSign.TicTac.X;
        currentX = Instantiate(x, gameObject.transform, false);
        currentX.transform.localPosition = CalculatePosition();
        RotateToSide();
        // currentX.transform.localScale = new Vector3(1, 1, 1);
        break;
      default: throw new ArgumentOutOfRangeException();
    }
  }

  private Vector3 CalculatePosition() {
    return Side switch {
             PrimitiveCube.Side.Up => new Vector3(0, 0.5f, 0),
             PrimitiveCube.Side.Down => new Vector3(0, -0.5f, 0),
             PrimitiveCube.Side.Left => new Vector3(-0.5f, 0, 0),
             PrimitiveCube.Side.Right => new Vector3(0.5f, 0, 0),
             PrimitiveCube.Side.Front => new Vector3(0, 0, 0.5f),
             PrimitiveCube.Side.Back => new Vector3(0, 0, -0.5f),
             _ => throw new ArgumentOutOfRangeException()
           };
  }

  private void RotateToSide() {
    switch (Side) {
      case PrimitiveCube.Side.Up:
      case PrimitiveCube.Side.Down:
        switch (Sign) {
          case TicTacSign.TicTac.X:
            currentX.transform.Rotate(Vector3.right, 90);
            break;
          case TicTacSign.TicTac.O:
            currentO.transform.Rotate(Vector3.right, 90);
            break;
        }

        break;

      case PrimitiveCube.Side.Left:
      case PrimitiveCube.Side.Right:
        switch (Sign) {
          case TicTacSign.TicTac.X:
            currentX.transform.Rotate(Vector3.up, 90);
            break;
          case TicTacSign.TicTac.O:
            currentO.transform.Rotate(Vector3.up, 90);
            break;
        }

        break;
    }
  }
}