public sealed class RCubePieceFaceMarkSetEventInvokerImpl : RCubePieceFaceMarkSetEventInvoker {
  private readonly RCubePieceFaceSignSetEvent rCubePieceFaceSignSetEvent = new RCubePieceFaceSignSetEvent();
  public RCubePieceFaceSignSetEvent GetEvent() => rCubePieceFaceSignSetEvent;
}