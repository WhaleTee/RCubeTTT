public sealed class SignSetEventInvokerImpl : SignSetEventInvoker {
  private readonly SignSetEvent signSetEvent = new SignSetEvent();
  public SignSetEvent GetEvent() => signSetEvent;
}