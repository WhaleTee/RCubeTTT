using System;
using UnityEngine;

public class RCubePieceSide : PrimitiveCube, TicTacSign {
  private TicTacSign.TicTac ticTac = TicTacSign.TicTac.NoSign;

  public RCubePieceSide(Side side, GameObject parent, Vector3 localPosition, Vector3 localScale, Quaternion rotation) :
  base(parent, localPosition, localScale, rotation) {
    var ttController = GameObject.AddComponent<TTController>();
    ttController.Side = side;
  }
  
  public TicTacSign.TicTac GetSign() {
    return ticTac;
  }

  public void SetSign(TicTacSign.TicTac sign) {
    if (ticTac == TicTacSign.TicTac.NoSign) {
      ticTac = sign;
    }
  }

  public void ClearSign() {
    ticTac = TicTacSign.TicTac.NoSign;
  }

  public void SetColor(Color color) {
    GameObject.GetComponent<Renderer>().material.color = color;
  }
}