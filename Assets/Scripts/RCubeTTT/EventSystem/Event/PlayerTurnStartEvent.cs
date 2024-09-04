using RCubeTTT.Model;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem.Event {
  /// <summary>
  /// Event invoked when a player turn starts.
  /// </summary>
  public sealed class PlayerTurnStartEvent : UnityEvent<PlayerPlayData> { }
}