using UnityEngine.Events;

/// <summary>
/// Event invoked when the user dragging a Rubik's Cube face.
/// It contains the global UUID identifier as a string.
/// </summary>
public sealed class RCubeFaceDragEvent : UnityEvent<string> { }