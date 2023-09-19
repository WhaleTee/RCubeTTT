public sealed class RCubeFacePiecesFacesRaycastHitEventInvokerImpl : RCubeFacePiecesFacesRaycastHitEventInvoker {
  private readonly RCubeFacePiecesFaceRaycastHitEvent rCubeFacePiecesFaceRaycastHitEvent = new RCubeFacePiecesFaceRaycastHitEvent();
  public RCubeFacePiecesFaceRaycastHitEvent GetEvent() => rCubeFacePiecesFaceRaycastHitEvent;
}