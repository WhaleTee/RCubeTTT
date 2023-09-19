using UnityEngine.Events;

/// <summary>
/// Event invoked when a Rubik's Cube face rotation is started.
/// It contains the global UUID identifier as a string.
/// </summary>
public sealed class RCubeFaceRotationStartEvent : UnityEvent<RCubeFaceRotationStartEventContext> { }