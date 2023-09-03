using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Interface for invoking <see cref="InputEvent"/>.
/// </summary>
public interface InputInvoker : Invoker<InputAction.CallbackContext>, EventProvider<InputEvent> {
  /// <summary>
  /// Adds the listener to the <see cref="InputEvent"/>.
  /// </summary>
  /// <param name="listener">The listener to add.</param>
  void Invoker<InputAction.CallbackContext>.AddListener(UnityAction<InputAction.CallbackContext> listener) => GetEvent().AddListener(listener);

  /// <summary>
  /// Invokes the <see cref="InputEvent"/>.
  /// </summary>
  /// <param name="context">The context of the <see cref="InputEvent"/> that represents the context of an input action.</param>
  void Invoker<InputAction.CallbackContext>.Invoke(InputAction.CallbackContext context) => GetEvent().Invoke(context);
}