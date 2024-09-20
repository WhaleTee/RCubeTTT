using System;
using Common.EventSystem.Bus;
using Common.Extensions;
using UnityEngine;

namespace Common.DragSystem {
  [Serializable]
  internal enum Axis {
    X, Y, Z
  }

  internal static class AxisExtensions {
    public static Vector3 ToVector(this Axis axis, Transform relative) {
      return axis switch { Axis.X => relative.right, Axis.Y => relative.up, Axis.Z => relative.forward, var _ => Vector3.zero };
    }
  }

  public class AlongAxisDrag : MonoBehaviour, DragComponent {
    [SerializeField] private Transform target;
    [SerializeField] private Transform relative;
    [SerializeField] private Axis axis;

    private Camera mainCamera;
    private Vector2 pointerDelta;

    public int targetInstanceId => targetTransform.gameObject.GetInstanceID();
    private Transform targetTransform => target ? target : transform;
    private Transform relativeTransform => relative ? relative : transform;

    private void Awake() {
      mainCamera = Camera.main;

      EventBus<PointerPositionDeltaEvent>.Register(new EventBinding<PointerPositionDeltaEvent>(ctx => pointerDelta = ctx.delta));
    }

    public bool IsDragAllowed() {
      var screenDirection = axis.ToVector(relativeTransform).ToScreenDirection(relativeTransform.position, mainCamera);
      var isDragAllowed = Mathf.Abs(Vector2.Dot(screenDirection, pointerDelta.normalized)) > 0.999f;
      if (isDragAllowed) {
        EventBus<AxisDragAllowedEvent>.Raise(new AxisDragAllowedEvent { instanceId = targetInstanceId, directionDragAllowed = screenDirection });
      }
      return isDragAllowed;
    }
  }
}