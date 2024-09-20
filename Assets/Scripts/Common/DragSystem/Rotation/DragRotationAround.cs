
using UnityEngine;

namespace Common.DragSystem.Rotation {
  public class DragRotationAround : DragRotation {

    protected override void Rotate() {
      var relativePosition = relativeTransform.position;
      targetTransform.RotateAround(relativePosition, relativeTransform.up, pointerDelta.x * speed * Time.deltaTime);
      targetTransform.RotateAround(relativePosition, relativeTransform.right, -pointerDelta.y * speed * Time.deltaTime);
      targetTransform.LookAt(relativeTransform, Vector3.up);
    }
  }
}