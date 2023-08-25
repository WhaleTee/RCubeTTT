using System;
using UnityEngine;

public class CubePiece: MonoBehaviour, Identifiable {
  
  private readonly string id = Guid.NewGuid().ToString();

  public string GetId() => id;

  public override bool Equals(object obj) {
    if (obj == null || GetType() != obj.GetType()) {
      return false;
    }

    return id == (obj as CubePiece)?.id;
  }

  public override int GetHashCode() {
    return id.GetHashCode();
  }
}