using UnityEngine;

namespace Common.DragSystem.Rotation {
  public class DragRotationY : DragRotation {
    protected override void Rotate() {
      targetTransform.Rotate(relativeTransform.right, Vector3.Dot(pointerDelta * (speed * Time.deltaTime), Vector3.up), Space.World);
    }
  }
}