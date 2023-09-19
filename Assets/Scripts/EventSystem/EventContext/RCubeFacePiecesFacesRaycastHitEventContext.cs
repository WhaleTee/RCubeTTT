public sealed class RCubeFacePiecesFacesRaycastHitEventContext {
  public MarkType[] scannedSigns { get; private set; }
  public RCubeFacePositionType facePositionType { get; private set; }

  public RCubeFacePiecesFacesRaycastHitEventContext(MarkType[] scannedSigns, RCubeFacePositionType facePositionType) {
    this.scannedSigns = scannedSigns;
    this.facePositionType = facePositionType;
  }
}