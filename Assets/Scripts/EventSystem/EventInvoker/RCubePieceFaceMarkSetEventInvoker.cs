using UnityEngine.Events;

public interface RCubePieceFaceMarkSetEventInvoker : Invoker<RCubePieceFaceMarkSetEventContext>, EventProvider<RCubePieceFaceSignSetEvent> {
  void Invoker<RCubePieceFaceMarkSetEventContext>.AddListener(UnityAction<RCubePieceFaceMarkSetEventContext> listener) => GetEvent().AddListener(listener);

  void Invoker<RCubePieceFaceMarkSetEventContext>.Invoke(RCubePieceFaceMarkSetEventContext context) => GetEvent().Invoke(context);
}