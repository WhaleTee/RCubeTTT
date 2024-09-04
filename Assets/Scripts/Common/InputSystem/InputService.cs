using Common.EventSystem.Context;
using Common.EventSystem.Invoker;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.InputSystem {
  public class InputService : MonoBehaviour {
    private Camera mainCamera;

    private InputActions inputActions;
    private InputActions.PlayerActions playerActions;

    private readonly IEventInvoker<PositionContext> pointerPositionInvoker = new PositionContextInvoker();
    private readonly IEventInvoker<DeltaContext> pointerDeltaInvoker = new DeltaContextInvoker();
    private readonly IEventInvoker pointerClickInvoker = new NoContextInvoker();
    private readonly IEventInvoker pointerClickUpInvoker = new NoContextInvoker();
    private readonly IEventInvoker pointerDoubleClickInvoker = new NoContextInvoker();
    private readonly IEventInvoker mouseRightClickInvoker = new NoContextInvoker();
    private readonly IEventInvoker mouseMiddleClickInvoker = new NoContextInvoker();
    private readonly IEventInvoker<DeltaContext> mouseWheelScrollInvoker = new DeltaContextInvoker();

    private void Awake() {
      mainCamera = Camera.main;

      inputActions = new InputActions();
      playerActions = inputActions.Player;

      playerActions.PointerPosition.performed += OnPointerPositionPerformed;
      playerActions.PointerDelta.performed += OnPointerDeltaPerformed;
      playerActions.Click.performed += OnPointerClickPerformed;
      playerActions.Click.canceled += OnPointerClickCanceled;
      playerActions.DoubleClick.performed += OnPointerDoubleClickPerformed;
      playerActions.RightClick.performed += OnMouseRightClickPerformed;
      playerActions.MiddleClick.performed += OnMouseMiddleClickPerformed;
      playerActions.WheelScroll.performed += OnMouseWheelScrollPerformed;

      PlayerInputEventManager.AddPointerPositionInvoker(pointerPositionInvoker);
      PlayerInputEventManager.AddPointerDeltaInvoker(pointerDeltaInvoker);
      PlayerInputEventManager.AddPointerClickInvoker(pointerClickInvoker);
      PlayerInputEventManager.AddPointerClickUpInvoker(pointerClickUpInvoker);
      PlayerInputEventManager.AddPointerDoubleClickInvoker(pointerDoubleClickInvoker);
      PlayerInputEventManager.AddMouseRightClickInvoker(mouseRightClickInvoker);
      PlayerInputEventManager.AddMouseMiddleClickInvoker(mouseMiddleClickInvoker);
      PlayerInputEventManager.AddMouseWheelScrollInvoker(mouseWheelScrollInvoker);
    }

    private void OnEnable() {
      inputActions.Enable();
    }

    private void OnDisable() {
      inputActions.Disable();
    }

    private void OnPointerPositionPerformed(InputAction.CallbackContext context) {
      pointerPositionInvoker.Invoke(new PositionContext(context.ReadValue<Vector2>()));
    }

    private void OnPointerDeltaPerformed(InputAction.CallbackContext context) {
      pointerDeltaInvoker.Invoke(new DeltaContext(context.ReadValue<Vector2>()));
    }

    private void OnPointerClickPerformed(InputAction.CallbackContext context) => pointerClickInvoker.Invoke();

    private void OnPointerClickCanceled(InputAction.CallbackContext context) => pointerClickUpInvoker.Invoke();

    private void OnPointerDoubleClickPerformed(InputAction.CallbackContext context) => pointerDoubleClickInvoker.Invoke();

    private void OnMouseRightClickPerformed(InputAction.CallbackContext context) => mouseRightClickInvoker.Invoke();

    private void OnMouseMiddleClickPerformed(InputAction.CallbackContext context) => mouseMiddleClickInvoker.Invoke();

    private void OnMouseWheelScrollPerformed(InputAction.CallbackContext context) {
      mouseWheelScrollInvoker.Invoke(new DeltaContext(context.ReadValue<Vector2>()));
    }
  }
}