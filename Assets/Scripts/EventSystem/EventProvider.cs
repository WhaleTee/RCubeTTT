/// <summary>
/// Interface that provides an event instance.
/// </summary>
/// <typeparam name="T">The type of the event.</typeparam>
public interface EventProvider<out T> {
  /// <summary>
  /// Gets the event instance.
  /// </summary>
  /// <returns>The event instance.</returns>
  T GetEvent();
}