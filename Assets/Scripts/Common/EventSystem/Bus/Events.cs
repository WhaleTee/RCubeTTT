using JetBrains.Annotations;
using RCubeTTT.Model;
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

  public struct PointerDoubleClickBeginEvent : IEvent { }
  
  public struct PointerDoubleClickEvent : IEvent { }

  public struct MouseRightDownEvent : IEvent { }

  public struct MouseMiddleDownEvent : IEvent { }

  public struct MouseWheelScrollEvent : IEvent {
    public Vector2 delta;
  }

  #endregion

  #region Drag Events

  public struct NoObjectDragBeginEvent : IEvent { }

  public struct RaycastBeforeDragBeginEvent : IEvent {
    /// <summary>
    /// sorted by distance
    /// </summary>
    [CanBeNull]
    public int[] hitObjects;

    /// <summary>
    /// screen position
    /// </summary>
    public Vector2 pointerPosition;
  }

  public struct DragBeginEvent : IEvent {
    public int instanceId;

    /// <summary>
    /// screen position
    /// </summary>
    public Vector2 pointerPosition;

    public Vector3 hitPoint;
    public Vector3 hitNormal;
  }

  public struct DragEvent : IEvent {
    public int instanceId;
  }

  public struct DragEndEvent : IEvent {
    public int instanceId;
  }
  
  public struct AxisDragAllowedEvent : IEvent {
    public int instanceId;
    /// <summary>
    /// screen direction
    /// </summary>
    public Vector2 directionDragAllowed;
  }

  #endregion

  #region Rubik's Cube Events

  public struct RCubeSideDragBeginEvent : IEvent {
    public int instanceId;
    /// <summary>
    /// normalized screen direction
    /// </summary>
    public Vector2 screenDirection;
    public Quaternion localRotation;
  }

  #endregion

  #region Crop Rotation Events

  public struct ObjectCropRotationBeginEvent : IEvent {
    public int instanceId;
    public Quaternion currentRotation;
    public Quaternion targetRotation;
  }

  public struct ObjectCropRotationEndEvent : IEvent {
    public int instanceId;
    public Quaternion currentRotation;
    public Quaternion targetRotation;
  }

  #endregion

  #region Player Turn Events

  public struct PlayerTurnEvent : IEvent {
    public PlayerPlayData player;
  }
  
  public struct PlayerTurnEndEvent : IEvent {
    public PlayerPlayData player;
  }
  
  public struct PlayerPutMarkEvent : IEvent {
    public PlayerPlayData player;
  }

  #endregion

  #region Win Condition Events

  public struct PlayerWinConditionReachedEvent : IEvent {
    public PlayerPlayData player;
  }

  #endregion

  #region Scan Marks Events

  public struct ScanMarksEvent : IEvent {
    public MarkType[] marks;
    public FaceType faceType;
  }

  #endregion
}