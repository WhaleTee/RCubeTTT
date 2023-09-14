public sealed class RCubeFaceRotationEventInvokerImpl : RCubeFaceRotationEventInvoker {
  private readonly RCubeFaceRotationEvent rCubeRotationEvent = new RCubeFaceRotationEvent();
  public RCubeFaceRotationEvent GetEvent() => rCubeRotationEvent;
}