public sealed class RCubeDragEventInvokerImpl : RCubeDragEventInvoker {
  private readonly RCubeDragEvent rCubeDragEvent = new RCubeDragEvent();
  public RCubeDragEvent GetEvent() => rCubeDragEvent;
}