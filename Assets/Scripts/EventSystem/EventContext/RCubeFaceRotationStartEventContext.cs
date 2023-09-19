using UnityEngine;

public sealed class RCubeFaceRotationStartEventContext : RCubeFaceRotationEventContext {
  public Quaternion localRotation { get; private set; }

  public RCubeFaceRotationStartEventContext(string faceGlobalId, RCubeFacePositionType facePositionType, Quaternion localRotation) : base(
    faceGlobalId,
    facePositionType
  ) {
    this.localRotation = localRotation;
  }
}