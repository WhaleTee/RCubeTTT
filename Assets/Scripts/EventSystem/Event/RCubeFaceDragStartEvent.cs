using UnityEngine.Events;

/// <summary>
/// Event invoked when the user starts dragging a Rubik's Cube face.
/// It contains the global UUID identifier as a string.
/// </summary>
public sealed class RCubeFaceDragStartEvent : UnityEvent<RCubeFaceDragStartEventContext> { }