using RCubeTTT.EventSystem.Event;
using RCubeTTT.Model;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem.EventInvoker
{
  /// <summary>
  /// Interface for invoking <see cref="PlayerTurnEventContext"/>
  /// </summary>
  public interface PlayerTurnEventInvoker : Invoker<PlayerPlayData>, EventProvider<PlayerTurnEvent> {
    /// <summary>
    /// Adds a listener to the <see cref="PlayerPlayData"/>
    /// </summary>
    /// <param name="listener">The listener to add.</param>
    void Invoker<PlayerPlayData>.AddListener(UnityAction<PlayerPlayData> listener) => GetEvent().AddListener(listener);

    /// <summary>
    /// Invokes the <see cref="PlayerPlayData"/>
    /// </summary>
    /// <param name="context">The player turn callback context.</param>
    void Invoker<PlayerPlayData>.Invoke(PlayerPlayData context) => GetEvent().Invoke(context);
  }
}