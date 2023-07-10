using System;
using UnityEngine;

public class PrimitiveCube : GameObjectHolder {
  public GameObject GameObject { get; private set; }

  public PrimitiveCube(GameObject parent, Vector3 localPosition, Vector3 localScale, Quaternion rotation) {
    GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
    GameObject.transform.parent = parent.transform;
    GameObject.transform.localPosition = localPosition;
    GameObject.transform.localScale = localScale;
    GameObject.transform.rotation = rotation;
  }

  public GameObject GetGameObject() {
    return GameObject;
  }
  
  public enum Side {
    Up,
    Down,
    Left,
    Right,
    Front,
    Back
    
  }

  public static class SideExtension {
    public static Side FindByOrdinal(int ordinal) {
      return ordinal switch {
               0 => Side.Up,
               1 => Side.Down,
               2 => Side.Left,
               3 => Side.Right,
               4 => Side.Front,
               5 => Side.Back,
               _ => throw new ArgumentOutOfRangeException(nameof(ordinal), ordinal, null)
             };
    }
  }
}