public class MouseUpInputInvoker : InputInvoker {
  #region methods

  private readonly MouseUpInputEvent mouseUpInputEvent = new MouseUpInputEvent();

  #endregion

  #region InputInvoker methods

  InputEvent InputInvoker.GetInputEvent() => mouseUpInputEvent;

  #endregion
}