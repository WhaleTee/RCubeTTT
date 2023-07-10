using UnityEngine;

public interface TicTacSign {
  TicTac GetSign();
  void SetSign(TicTac sign);
  void ClearSign();
  
  public enum TicTac {
    X,O, NoSign
  }
}