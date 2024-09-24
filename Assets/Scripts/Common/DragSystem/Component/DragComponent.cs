namespace Common.DragSystem.Component {
  public interface DragComponent : TargetInstanceIdProvider {
    public bool IsDragAllowed();
  }
}