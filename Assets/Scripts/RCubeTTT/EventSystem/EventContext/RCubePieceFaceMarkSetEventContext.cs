using RCubeTTT.Model;

namespace RCubeTTT.EventSystem.EventContext
{
  public sealed class RCubePieceFaceMarkSetEventContext {
    public RCubeFacePositionType facePositionType { get; private set; }

    public RCubePieceFaceMarkSetEventContext(RCubeFacePositionType facePositionType) {
      this.facePositionType = facePositionType;
    }
  }
}