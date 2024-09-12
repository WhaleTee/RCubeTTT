using JetBrains.Annotations;
using UnityEngine;

namespace Common.EventSystem.Bus {
  public interface IEvent { }

  #region Input Events

  public struct PointerPositionEvent : IEvent {
    public Vector3 screenPosition;
  }

  public struct PointerPositionDeltaEvent : IEvent {
    public Vector3 delta;
  }

  public struct PointerDownEvent : IEvent { }

  public struct PointerUpEvent : IEvent { }

  public struct PointerDoubleClickEvent : IEvent { }

  public struct MouseRightDownEvent : IEvent { }

  public struct MouseMiddleDownEvent : IEvent { }

  public struct MouseWheelScrollEvent : IEvent {
    public Vector2 delta;
  }

  #endregion

  #region Drag Events
  
  public struct RaycastBeforeDragBeginEvent : IEvent {
    [NotNull]public int[] hitObjects; // sorted by distance
  }

  public struct DragBeginEvent : IEvent {
    public int instanceId;
    public Vector2 pointerScreenPosition;
    public Vector3 hitPoint;
    public Vector3 hitNormal;
  }

  public struct DragEvent : IEvent {
    public int instanceId;
  }

  public struct DragEndEvent : IEvent {
    public int instanceId;
  }

  #endregion

  #region Idle Rotation Events

  public struct ObjectIdleRotationBeginEvent : IEvent {
    public int instanceId;
    public Quaternion currentRotation;
    public Quaternion targetRotation;
  }

  public struct ObjectIdleRotationEndEvent : IEvent {
    public int instanceId;
    public Quaternion currentRotation;
    public Quaternion targetRotation;
  }

  #endregion
}