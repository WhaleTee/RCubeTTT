using RCubeTTT.EventSystem.EventContext;
using RCubeTTT.Model;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem.EventInvoker {
  public interface IDragEventInvoker : Invoker<ObjectInstanceContext>, EventProvider<UnityEvent<ObjectInstanceContext>> {
    void Invoker<ObjectInstanceContext>.AddListener(UnityAction<ObjectInstanceContext> listener) => GetEvent().AddListener(listener);
    void Invoker<ObjectInstanceContext>.Invoke(ObjectInstanceContext faceGlobalId) => GetEvent().Invoke(faceGlobalId);
  }
}