using RCubeTTT.EventSystem.Event;

namespace RCubeTTT.EventSystem.EventInvoker.Impl
{
  public sealed class PlayerWinConditionReachedEventInvokerImpl : PlayerWinConditionReachedEventInvoker {
    private readonly WinConditionReachedEvent winConditionReachedEvent = new WinConditionReachedEvent();
    public WinConditionReachedEvent GetEvent() => winConditionReachedEvent;
  }
}