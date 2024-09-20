using UnityEngine;

namespace Common.DragSystem.Rotation {
  public class XDragRotation : DragRotation {
    protected override void Rotate() {
      targetTransform.Rotate(relativeTransform.up, Vector3.Dot(pointerDelta * (speed * Time.deltaTime), Vector3.left), Space.World);
    }
  }
}