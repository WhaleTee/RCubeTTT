public sealed class RCubeFaceRotationStartEventInvokerImpl : RCubeFaceRotationStartEventInvoker {
  private readonly RCubeFaceRotationStartEvent rCubeRotationStartEvent = new RCubeFaceRotationStartEvent();
  public RCubeFaceRotationStartEvent GetEvent() => rCubeRotationStartEvent;
}