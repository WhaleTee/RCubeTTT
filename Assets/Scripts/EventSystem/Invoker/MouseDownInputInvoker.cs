public class MouseDownInputInvoker : InputInvoker {
  #region methods

  private readonly MouseDownInputEvent mouseDownInputEvent = new MouseDownInputEvent();

  #endregion

  #region InputInvoker methods

  InputEvent InputInvoker.GetInputEvent() => mouseDownInputEvent;

  #endregion
}