public sealed class RCubeDragStartEventInvokerImpl : RCubeDragStartEventInvoker {
  private readonly RCubeDragStartEvent rCubeDragStartEvent = new RCubeDragStartEvent();
  public RCubeDragStartEvent GetEvent() => rCubeDragStartEvent;
}