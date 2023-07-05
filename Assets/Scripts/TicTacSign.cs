using UnityEngine;

public interface TicTacSign {
  TicTac GetSign();
  void SetSign(TicTac sign);
  
  public enum TicTac {
    X,O, NoSign
  }
}