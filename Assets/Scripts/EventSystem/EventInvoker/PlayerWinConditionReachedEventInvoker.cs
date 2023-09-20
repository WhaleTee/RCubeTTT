using UnityEngine.Events;

public interface
PlayerWinConditionReachedEventInvoker : Invoker<PlayerWinConditionReachedEventContext>, EventProvider<PlayerWinConditionReachedEvent> {
  void Invoker<PlayerWinConditionReachedEventContext>.AddListener(UnityAction<PlayerWinConditionReachedEventContext> listener) =>
  GetEvent().AddListener(listener);

  void Invoker<PlayerWinConditionReachedEventContext>.Invoke(PlayerWinConditionReachedEventContext context) => GetEvent().Invoke(context);
}