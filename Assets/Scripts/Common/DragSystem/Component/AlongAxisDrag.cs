using System;
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
    private readonly int targetInstanceId;
    private readonly Transform relative;
    private readonly Axis axis;
    private readonly Camera raycastCamera;
    private Vector2 pointerDelta;

    public AlongAxisDrag(int targetInstanceId, Transform relative, Axis axis, Camera raycastCamera) {
      this.targetInstanceId = targetInstanceId;
      this.relative = relative;
      this.axis = axis;
      this.raycastCamera = raycastCamera;

      EventBus<PointerPositionDeltaEvent>.Register(new EventBinding<PointerPositionDeltaEvent>(ctx => pointerDelta = ctx.delta));
    }

    public int GetTargetInstanceId() => targetInstanceId;

    public bool IsDragAllowed() {
      var screenDirection = axis.ToVector(relative).ToScreenDirection(relative.position, raycastCamera);
      var isDragAllowed = Mathf.Abs(Vector2.Dot(screenDirection, pointerDelta.normalized)) > 0.999f;

      if (isDragAllowed) {
        EventBus<AxisDragAllowedEvent>.Raise(new AxisDragAllowedEvent { instanceId = GetTargetInstanceId(), directionDragAllowed = screenDirection });
      }

      return isDragAllowed;
    }
  }
}