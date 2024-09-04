using Common.EventSystem.Context;
using UnityEngine.Events;

namespace Common.EventSystem.Invoker {
  public sealed class GameObjectContextInvoker : IEventInvoker<GameObjectContext>, EventProvider<UnityEvent<GameObjectContext>> {
    private readonly UnityEvent<GameObjectContext> pointerInstanceIdEvent = new UnityEvent<GameObjectContext>();

    public UnityEvent<GameObjectContext> GetEvent() => pointerInstanceIdEvent;
    void IEventInvoker<GameObjectContext>.AddListener(UnityAction<GameObjectContext> listener) => GetEvent().AddListener(listener);
    void IEventInvoker<GameObjectContext>.Invoke(GameObjectContext context) => GetEvent().Invoke(context);
  }
}