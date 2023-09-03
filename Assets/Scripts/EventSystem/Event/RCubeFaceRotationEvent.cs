using UnityEngine.Events;

/// <summary>
/// Event invoked when a Rubik's Cube face is rotating.
/// It contains the global UUID identifier as a string.
/// </summary>
public sealed class RCubeFaceRotationEvent : UnityEvent<string> { }