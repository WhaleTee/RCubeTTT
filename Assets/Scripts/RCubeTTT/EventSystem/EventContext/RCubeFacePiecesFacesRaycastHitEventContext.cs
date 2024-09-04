using RCubeTTT.Model;

namespace RCubeTTT.EventSystem.EventContext
{
  public sealed class RCubeFacePiecesFacesRaycastHitEventContext {
    public MarkType[] scannedMarks { get; private set; }
    public RCubeFacePositionType facePositionType { get; private set; }

    public RCubeFacePiecesFacesRaycastHitEventContext(MarkType[] scannedMarks, RCubeFacePositionType facePositionType) {
      this.scannedMarks = scannedMarks;
      this.facePositionType = facePositionType;
    }
  }
}