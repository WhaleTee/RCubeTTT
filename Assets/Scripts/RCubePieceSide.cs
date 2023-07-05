using System;
using UnityEngine;

public class RCubePieceSide : PrimitiveCube, TicTacSign {
  private readonly bool active;
  private TicTacSign.TicTac ticTac;

  public RCubePieceSide(GameObject parent, Vector3 localPosition, Vector3 localScale, Quaternion rotation) :
  base(parent, localPosition, localScale, rotation) {
    active = true;
  }

  public bool IsActive() {
    return active;
  }

  public TicTacSign.TicTac GetSign() {
    return ticTac;
  }

  public void SetSign(TicTacSign.TicTac sign) {
    if (ticTac == TicTacSign.TicTac.NoSign) {
      ticTac = sign;
    }
  }
  
  public void SetColor(Color color) {
    gameObject.GetComponent<Renderer>().material.color = color;
  }
}