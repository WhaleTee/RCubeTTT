namespace Common.DragSystem {
  public interface DragComponent : TargetInstanceIdProvider {
    public bool IsDragAllowed();
  }
}