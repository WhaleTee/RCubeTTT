public sealed class PlayerWinConditionReachedEventInvokerImpl : PlayerWinConditionReachedEventInvoker {
  private readonly PlayerWinConditionReachedEvent playerWinConditionReachedEvent = new PlayerWinConditionReachedEvent();
  public PlayerWinConditionReachedEvent GetEvent() => playerWinConditionReachedEvent;
}