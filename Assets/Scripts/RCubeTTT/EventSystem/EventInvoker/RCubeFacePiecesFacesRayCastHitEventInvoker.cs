using RCubeTTT.EventSystem.Event.RCubeEvent;
using RCubeTTT.EventSystem.EventContext;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem.EventInvoker
{
  /// <summary>
  /// Interface for invoking the <see cref="RCubeFacePiecesFaceRaycastHitEvent"/>.
  /// </summary>
  public interface RCubeFacePiecesFacesRayCastHitEventInvoker : Invoker<RCubeFacePiecesFacesRaycastHitEventContext>,
                                                                EventProvider<RCubeFacePiecesFaceRaycastHitEvent> {
    /// <summary>
    /// Adds a listener to the <see cref="RCubeFacePiecesFaceRaycastHitEvent"/>.
    /// </summary>
    /// <param name="listener">The UnityAction listener to add.</param>
    void Invoker<RCubeFacePiecesFacesRaycastHitEventContext>.AddListener(UnityAction<RCubeFacePiecesFacesRaycastHitEventContext> listener) =>
    GetEvent().AddListener(listener);

    /// <summary>
    /// Invokes the <see cref="RCubeFaceRotationStartEvent"/>.
    /// </summary>
    /// <param name="context">The <see cref="RCubeFacePiecesFacesRaycastHitEventContext">context</see> that represents the context of the <see cref="RCubeFacePiecesFaceRaycastHitEvent"/>.</param>
    void Invoker<RCubeFacePiecesFacesRaycastHitEventContext>.Invoke(RCubeFacePiecesFacesRaycastHitEventContext context) => GetEvent().Invoke(context);
  }
}