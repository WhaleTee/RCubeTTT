public sealed class PlayerTurnStartEventInvokerImpl : PlayerTurnStartEventInvoker {
  private readonly PlayerTurnStartEvent playerTurnStartEvent = new PlayerTurnStartEvent();
  public PlayerTurnStartEvent GetEvent() => playerTurnStartEvent;
}