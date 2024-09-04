using RCubeTTT.EventSystem.Event;

namespace RCubeTTT.EventSystem.EventInvoker.Impl
{
  public sealed class PlayerTurnStartEventInvokerImpl : PlayerTurnStartEventInvoker {
    private readonly PlayerTurnStartEvent playerTurnStartEvent = new PlayerTurnStartEvent();
    public PlayerTurnStartEvent GetEvent() => playerTurnStartEvent;
  }
}