using UnityEngine;

public class RCubeFaceRotationEndEventContext : RCubeFaceRotationEventContext {
  public Quaternion localRotation { get; private set; }

  public RCubeFaceRotationEndEventContext(string faceGlobalId, RCubeFacePositionType facePositionType, Quaternion localRotation) : base(
    faceGlobalId,
    facePositionType
  ) {
    this.localRotation = localRotation;
  }
}