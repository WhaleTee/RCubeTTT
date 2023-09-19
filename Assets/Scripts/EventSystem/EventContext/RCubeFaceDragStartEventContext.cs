using UnityEngine;

/// <summary>
/// Represents the context of a raycast hit event on a Rubik's Cube Face.
/// </summary>
public sealed class RCubeFaceDragStartEventContext : RCubeFaceRotationEventContext {
  public Vector2 faceHitPosition { get; private set; }

  public RCubeFaceDragStartEventContext(string faceGlobalId, RCubeFacePositionType facePositionType, Vector2 faceHitPosition) : base(faceGlobalId, facePositionType) {
    this.faceHitPosition = faceHitPosition;
  }
}