public class MouseDragInputInvoker : InputInvoker {
  #region methods

  private readonly MouseDragInputEvent mouseDragInputEvent = new MouseDragInputEvent();

  #endregion

  #region InputInvoker methods

  InputEvent InputInvoker.GetInputEvent() => mouseDragInputEvent;

  #endregion
}