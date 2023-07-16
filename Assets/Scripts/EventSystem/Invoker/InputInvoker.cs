using UnityEngine.Events;
using UnityEngine.InputSystem;

public interface InputInvoker : Invoker<InputAction.CallbackContext> {
  #region methods

  public InputEvent GetInputEvent();

  #endregion

  #region Invoker methods

  void Invoker<InputAction.CallbackContext>.AddListener(UnityAction<InputAction.CallbackContext> listener) => GetInputEvent().AddListener(listener);

  void Invoker<InputAction.CallbackContext>.Invoke(InputAction.CallbackContext context) {
    GetInputEvent().Invoke(context);
  }

  #endregion
}