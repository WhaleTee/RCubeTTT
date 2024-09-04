using RCubeTTT.EventSystem.Event;
using RCubeTTT.EventSystem.EventContext;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem.EventInvoker
{
  public interface PlayerWinConditionReachedEventInvoker : Invoker<PlayerWinConditionReachedEventContext>, EventProvider<WinConditionReachedEvent> {
    void Invoker<PlayerWinConditionReachedEventContext>.AddListener(UnityAction<PlayerWinConditionReachedEventContext> listener) =>
    GetEvent().AddListener(listener);

    void Invoker<PlayerWinConditionReachedEventContext>.Invoke(PlayerWinConditionReachedEventContext context) => GetEvent().Invoke(context);
  }
}