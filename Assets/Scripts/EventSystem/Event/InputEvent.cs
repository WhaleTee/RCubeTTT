using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Represents an event that is triggered when an input action is invoked.
/// It contains <see cref="InputAction.CallbackContext">input context</see> - information about the state of the input action, such as whether it was triggered, its value, and the time it occurred.
/// </summary>
public sealed class InputEvent : UnityEvent<InputAction.CallbackContext> { }