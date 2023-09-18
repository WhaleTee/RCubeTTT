public sealed class PlayerTurnEventInvokerImpl : PlayerTurnEventInvoker {
  private readonly PlayerTurnEvent playerTurnEvent = new PlayerTurnEvent();
  public PlayerTurnEvent GetEvent() => playerTurnEvent;
}