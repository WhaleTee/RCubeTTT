using RCubeTTT.EventSystem.Event;

namespace RCubeTTT.EventSystem.EventInvoker.Impl
{
  public sealed class PlayerTurnEventInvokerImpl : PlayerTurnEventInvoker {
    private readonly PlayerTurnEvent playerTurnEvent = new PlayerTurnEvent();
    public PlayerTurnEvent GetEvent() => playerTurnEvent;
  }
}