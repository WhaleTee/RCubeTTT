public sealed class RCubeFaceDragEventInvokerImpl : RCubeFaceDragEventInvoker {
  private readonly RCubeFaceDragEvent rCubeDragEvent = new RCubeFaceDragEvent();
  public RCubeFaceDragEvent GetEvent() => rCubeDragEvent;
}