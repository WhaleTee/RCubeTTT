using RCubeTTT.EventSystem.Event.RCubeEvent;
using RCubeTTT.EventSystem.EventContext;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem.EventInvoker
{
  public interface RCubePieceFaceMarkSetEventInvoker : Invoker<RCubePieceFaceMarkSetEventContext>, EventProvider<RCubePieceFaceSignSetEvent> {
    void Invoker<RCubePieceFaceMarkSetEventContext>.AddListener(UnityAction<RCubePieceFaceMarkSetEventContext> listener) =>
    GetEvent().AddListener(listener);

    void Invoker<RCubePieceFaceMarkSetEventContext>.Invoke(RCubePieceFaceMarkSetEventContext context) => GetEvent().Invoke(context);
  }
}