public class MouseDragCubeInputInvoker : InputInvoker {
  #region methods

  private readonly MouseDragCubeInputEvent mouseDragInputEvent = new MouseDragCubeInputEvent();

  #endregion

  #region InputInvoker methods

  InputEvent InputInvoker.GetInputEvent() => mouseDragInputEvent;

  #endregion
}