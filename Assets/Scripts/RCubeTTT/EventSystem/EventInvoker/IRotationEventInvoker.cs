using RCubeTTT.EventSystem.Event.RCubeEvent;
using RCubeTTT.EventSystem.EventContext;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem.EventInvoker
{
  /// <summary>
  /// Interface for invoking <see cref="RCubeFaceRotationEvent"/>
  /// </summary>
  public interface IRotationEventInvoker : Invoker<RotationEventContext>, EventProvider<UnityEvent<RotationEventContext>> {
    /// <summary>
    /// Adds a listener to the <see cref="RCubeFaceRotationEvent"/>
    /// </summary>
    /// <param name="listener">The listener to add.</param>
    void Invoker<RotationEventContext>.AddListener(UnityAction<RotationEventContext> listener) => GetEvent().AddListener(listener);

    /// <summary>
    /// Invokes the <see cref="RCubeFaceRotationEvent"/>
    /// </summary>
    /// <param name="context">The context of the <see cref="RCubeFaceRotationEvent"/>.</param>
    void Invoker<RotationEventContext>.Invoke(RotationEventContext context) => GetEvent().Invoke(context);
  }
}