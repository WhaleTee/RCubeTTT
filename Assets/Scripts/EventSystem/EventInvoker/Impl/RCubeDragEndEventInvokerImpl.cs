public sealed class RCubeDragEndEventInvokerImpl : RCubeDragEndEventInvoker {
  private readonly RCubeDragEndEvent rCubeDragEndEvent = new RCubeDragEndEvent();
  public RCubeDragEndEvent GetEvent() => rCubeDragEndEvent;
}