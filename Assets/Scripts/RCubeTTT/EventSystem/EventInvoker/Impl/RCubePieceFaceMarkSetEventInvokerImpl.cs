using RCubeTTT.EventSystem.Event.RCubeEvent;

namespace RCubeTTT.EventSystem.EventInvoker.Impl
{
  public sealed class RCubePieceFaceMarkSetEventInvokerImpl : RCubePieceFaceMarkSetEventInvoker {
    private readonly RCubePieceFaceSignSetEvent rCubePieceFaceSignSetEvent = new RCubePieceFaceSignSetEvent();
    public RCubePieceFaceSignSetEvent GetEvent() => rCubePieceFaceSignSetEvent;
  }
}