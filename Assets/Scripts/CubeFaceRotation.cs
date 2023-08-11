using UnityEngine;

public sealed class CubeFaceRotation {
  private readonly PlayerInputManager.PointerPosition pointerPosition;

  public CubeFaceRotation(PlayerInputManager.PointerPosition pointerPosition) {
    this.pointerPosition = pointerPosition;
  }

  public float GetAngle(Vector3 deltaInput, GameObject faceObject, GameObject relativeObject) {
    var currentPointerPosition = Vector2Int.RoundToInt(pointerPosition.Invoke());
    var facePosition = Vector3Int.RoundToInt(faceObject.transform.position);
    var relativeUp = Vector3Int.RoundToInt(relativeObject.transform.up);
    var result = 0f;

    if (relativeUp == Vector3Int.up) {
      result = Vector3.Dot(deltaInput, Vector3.left);
    }

    if (relativeUp == Vector3Int.down) {
      result = Vector3.Dot(deltaInput, Vector3.right);
    }

    if (relativeUp == Vector3Int.left) {
      result = Vector3.Dot(deltaInput, Vector3.down);
    }

    if (relativeUp == Vector3Int.right) {
      result = Vector3.Dot(deltaInput, Vector3.up);
    }

    if (relativeUp == Vector3Int.forward) {
      result = currentPointerPosition.y > facePosition.y
               ? Vector3.Dot(deltaInput, Vector3.left)
               : Vector3.Dot(deltaInput, Vector3.up);
    }

    if (relativeUp == Vector3Int.back) {
      result = currentPointerPosition.y > facePosition.y
               ? Vector3.Dot(deltaInput, Vector3.right)
               : Vector3.Dot(deltaInput, Vector3.down);
    }

    return result;
  }
}