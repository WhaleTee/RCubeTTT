using RCubeTTT.EventSystem.Event.RCubeEvent;

namespace RCubeTTT.EventSystem.EventInvoker.Impl
{
  public sealed class RCubeFacePiecesFacesRayCastHitEventInvokerImpl : RCubeFacePiecesFacesRayCastHitEventInvoker {
    private readonly RCubeFacePiecesFaceRaycastHitEvent rCubeFacePiecesFaceRayCastHitEvent = new RCubeFacePiecesFaceRaycastHitEvent();
    public RCubeFacePiecesFaceRaycastHitEvent GetEvent() => rCubeFacePiecesFaceRayCastHitEvent;
  }
}