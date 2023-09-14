public sealed class RCubeFaceDragEndEventInvokerImpl : RCubeFaceDragEndEventInvoker {
  private readonly RCubeFaceDragEndEvent rCubeDragEndEvent = new RCubeFaceDragEndEvent();
  public RCubeFaceDragEndEvent GetEvent() => rCubeDragEndEvent;
}