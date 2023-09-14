using UnityEngine;

/// <summary>
/// Represents the context of a raycast hit event on a Rubik's Cube Face.
/// </summary>
public sealed class RCubeFaceRaycastHitEventContext {
  public string faceGlobalId { get; private set; }
  public Vector2 faceHitPosition { get; private set; }
  
  public RCubeFaceRaycastHitEventContext(string faceGlobalId, Vector2 faceHitPosition) {
    this.faceGlobalId = faceGlobalId;
    this.faceHitPosition = faceHitPosition;
  }
}