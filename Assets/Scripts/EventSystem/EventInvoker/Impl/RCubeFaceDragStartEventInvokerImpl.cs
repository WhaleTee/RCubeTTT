public sealed class RCubeFaceDragStartEventInvokerImpl : RCubeFaceDragStartEventInvoker {
  private readonly RCubeFaceDragStartEvent rCubeDragStartEvent = new RCubeFaceDragStartEvent();
  public RCubeFaceDragStartEvent GetEvent() => rCubeDragStartEvent;
}