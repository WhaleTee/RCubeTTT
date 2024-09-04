namespace Common.EventSystem {
  public interface EventProvider<out T> {
    public T GetEvent();
  }
}