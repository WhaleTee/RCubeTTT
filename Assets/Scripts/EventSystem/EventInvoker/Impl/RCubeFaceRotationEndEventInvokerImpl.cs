public sealed class RCubeFaceRotationEndEventInvokerImpl : RCubeFaceRotationEndEventInvoker {
  private readonly RCubeFaceRotationEndEvent rCubeRotationStartEvent = new RCubeFaceRotationEndEvent();
  public RCubeFaceRotationEndEvent GetEvent() => rCubeRotationStartEvent;
}