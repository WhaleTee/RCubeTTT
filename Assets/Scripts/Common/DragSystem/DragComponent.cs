namespace Common.DragSystem {
  public interface DragComponent : InstanceIdProvider {
    public bool IsDragAllowed();
  }
}