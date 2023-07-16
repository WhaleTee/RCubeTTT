public class MouseRightClickUpInputInvoker : InputInvoker {
  #region methods

  private readonly MouseRightClickUpInputEvent mouseRightClickUpInputEvent = new MouseRightClickUpInputEvent();

  #endregion

  #region InputInvoker methods

  InputEvent InputInvoker.GetInputEvent() => mouseRightClickUpInputEvent;

  #endregion
}