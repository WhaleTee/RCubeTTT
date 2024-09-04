using RCubeTTT.EventSystem.EventContext;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem.Event
{
  public sealed class WinConditionReachedEvent : UnityEvent<PlayerWinConditionReachedEventContext> { }
}