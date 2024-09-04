using RCubeTTT.EventSystem.EventContext;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem.EventInvoker
{
  public interface InputHitObjectEventInvoker : Invoker<InputHitObjectEventContext>, EventProvider<UnityEvent<InputHitObjectEventContext>> {
    void Invoker<InputHitObjectEventContext>.AddListener(UnityAction<InputHitObjectEventContext> listener) => GetEvent().AddListener(listener);

    void Invoker<InputHitObjectEventContext>.Invoke(InputHitObjectEventContext context) => GetEvent().Invoke(context);
  }
}