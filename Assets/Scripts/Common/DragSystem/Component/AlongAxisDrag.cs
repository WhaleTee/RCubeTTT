using System;
using System.Linq;
using Common.EventBus;
using Common.Extensions;
using UnityEngine;

namespace Common.DragSystem.Component {
  [Serializable]
  public enum Axis {
    X, Y, Z
  }

  internal static class AxisExtensions {
    public static Vector3 ToVector(this Axis axis, Transform relative) {
      return axis switch { Axis.X => relative.right, Axis.Y => relative.up, Axis.Z => relative.forward, var _ => Vector3.zero };
    }
  }

  public class AlongAxisDrag : DragComponent {
    private const int MAGNITUDE_THRESHOLD = 8;
    private readonly int targetInstanceId;
    private readonly Transform relative;
    private readonly Axis axis;
    private readonly Camera raycastCamera;
    private Vector2 pointerHitPosition;
    private Vector2 pointerPosition;

    public AlongAxisDrag(int targetInstanceId, Transform relative, Axis axis, Camera raycastCamera) {
      this.targetInstanceId = targetInstanceId;
      this.relative = relative;
      this.axis = axis;
      this.raycastCamera = raycastCamera;
      
      EventBus<PointerPositionEvent>.Register(new EventBinding<PointerPositionEvent>(ctx => pointerPosition = ctx.screenPosition));
      EventBus<RaycastBeforeDragBeginEvent>.Register(
        new EventBinding<RaycastBeforeDragBeginEvent>(
          ctx => {
            if (ctx.hitObjects?.Length > 0) {
              if (ctx.hitObjects.FirstOrDefault(hit => hit == GetTargetInstanceId()) != 0) {
                pointerHitPosition = ctx.pointerPosition;
              }
            }
          }
        )
      );
    }

    public int GetTargetInstanceId() => targetInstanceId;

    public bool IsDragAllowed() {
      var pointerDistance = pointerPosition - pointerHitPosition;
      if (Vector2.Distance(pointerPosition, pointerHitPosition) < MAGNITUDE_THRESHOLD) return false;
      
      var screenDirection = axis.ToVector(relative).ToScreenDirection(relative.position, raycastCamera);
      var isDragAllowed = Mathf.Abs(Vector2.Dot(screenDirection, pointerDistance.normalized)) > 0.85f;

      if (isDragAllowed) {
        EventBus<AxisDragAllowedEvent>.Raise(new AxisDragAllowedEvent { instanceId = GetTargetInstanceId(), directionDragAllowed = screenDirection });
      }

      return isDragAllowed;
    }
  }
}