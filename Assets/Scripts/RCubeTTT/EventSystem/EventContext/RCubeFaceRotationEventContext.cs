using RCubeTTT.Model;

namespace RCubeTTT.EventSystem.EventContext
{
  public class RCubeFaceRotationEventContext {
    public string faceGlobalId { get; private set; }
    public RCubeFacePositionType facePositionType { get; private set; }

    public RCubeFaceRotationEventContext(string faceGlobalId, RCubeFacePositionType facePositionType) {
      this.faceGlobalId = faceGlobalId;
      this.facePositionType = facePositionType;
    }
  }
}