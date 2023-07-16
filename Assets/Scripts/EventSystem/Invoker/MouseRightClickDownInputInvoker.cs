public class MouseRightClickDownInputInvoker : InputInvoker {
  #region methods

  private readonly MouseRightClickDownOnCubeInputEvent mouseRightClickDownOnCubeInputEvent = new MouseRightClickDownOnCubeInputEvent();

  #endregion

  #region InputInvoker methods

  InputEvent InputInvoker.GetInputEvent() => mouseRightClickDownOnCubeInputEvent;

  #endregion
}