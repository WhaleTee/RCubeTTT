using UnityEngine;

namespace Common.DragSystem.Component.Rotation {
  public class YDragRotation : DragRotation {
    private readonly Transform target;
    private readonly Transform relative;
    private readonly float speed;
    
    public YDragRotation(
      Transform target, Transform relative,
      float speed, int targetInstanceId
    ) : base(targetInstanceId) {
      this.target = target;
      this.relative = relative;
      this.speed = speed;
    }
    
    protected override void Rotate() {
      target.Rotate(relative.right, Vector3.Dot(pointerDelta * (speed * Time.deltaTime), Vector3.up), Space.World);
    }
  }
}