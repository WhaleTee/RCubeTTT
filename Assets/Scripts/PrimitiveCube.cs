using System;
using UnityEngine;

public class PrimitiveCube : GameObjectHolder {
  public GameObject gameObject { get; private set; }

  public PrimitiveCube(GameObject parent, Vector3 localPosition, Vector3 localScale, Quaternion rotation) {
    gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
    gameObject.transform.parent = parent.transform;
    gameObject.transform.localPosition = localPosition;
    gameObject.transform.localScale = localScale;
    gameObject.transform.rotation = rotation;
  }

  public GameObject GetGameObject() {
    return gameObject;
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