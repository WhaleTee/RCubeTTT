using RCubeTTT.Model;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem.Event {
  public sealed class PlayerTurnEvent : UnityEvent<PlayerPlayData> { }
}