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

  public struct MouseRightClickEvent : IEvent { }

  public struct MouseMiddleClickEvent : IEvent { }

  public struct MouseWheelScrollEvent : IEvent {
    public Vector2 delta;
  }

  #endregion

  #region Drag Events

  public struct ObjectDragBeginEvent : IEvent {
    public int instanceId;
  }

  public struct ObjectDragEvent : IEvent {
    public int instanceId;
  }

  public struct ObjectDragEndEvent : IEvent {
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