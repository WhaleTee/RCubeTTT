using Common.EventSystem.Bus;
using UnityEngine;

namespace Common.InputSystem {
  public class InputService : MonoBehaviour {
    private InputActions inputActions;
    private InputActions.PlayerActions playerActions;

    private void Awake() {
      inputActions = new InputActions();
      playerActions = inputActions.Player;

      playerActions.Click.performed += _ => EventBus<PointerDownEvent>.Raise(new PointerDownEvent());
      playerActions.Click.canceled += _ => EventBus<PointerUpEvent>.Raise(new PointerUpEvent());
      playerActions.DoubleClick.performed += _ => EventBus<PointerDoubleClickEvent>.Raise(new PointerDoubleClickEvent());
      playerActions.RightClick.performed += _ => EventBus<MouseRightClickEvent>.Raise(new MouseRightClickEvent());
      playerActions.MiddleClick.performed += _ => EventBus<MouseMiddleClickEvent>.Raise(new MouseMiddleClickEvent());

      playerActions.PointerPosition.performed += ctx => EventBus<PointerPositionEvent>.Raise(
                                                   new PointerPositionEvent { screenPosition = ctx.ReadValue<Vector2>() }
                                                 );

      playerActions.PointerDelta.performed += ctx => EventBus<PointerPositionDeltaEvent>.Raise(
                                                new PointerPositionDeltaEvent { delta = ctx.ReadValue<Vector2>() }
                                              );

      playerActions.WheelScroll.performed += ctx => EventBus<MouseWheelScrollEvent>.Raise(
                                               new MouseWheelScrollEvent { delta = ctx.ReadValue<Vector2>() }
                                             );
    }

    private void OnEnable() {
      inputActions.Enable();
    }

    private void OnDisable() {
      inputActions.Disable();
    }
  }
}