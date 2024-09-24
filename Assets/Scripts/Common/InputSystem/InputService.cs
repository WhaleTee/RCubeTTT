using System;
using Common.EventBus;
using UnityEngine;

namespace Common.InputSystem {
  public sealed class InputService : IDisposable {
    private readonly InputActions inputActions;

    public InputService() {
      inputActions = new InputActions();
      var playerActions = inputActions.Player;

      playerActions.Click.performed += _ => EventBus<PointerDownEvent>.Raise(new PointerDownEvent());
      playerActions.Click.canceled += _ => EventBus<PointerUpEvent>.Raise(new PointerUpEvent());
      playerActions.DoubleClick.started += _ => EventBus<PointerDoubleClickBeginEvent>.Raise(new PointerDoubleClickBeginEvent());
      playerActions.DoubleClick.performed += _ => EventBus<PointerDoubleClickEvent>.Raise(new PointerDoubleClickEvent());
      playerActions.RightClick.performed += _ => EventBus<MouseRightDownEvent>.Raise(new MouseRightDownEvent());
      playerActions.RightClick.canceled += _ => EventBus<PointerUpEvent>.Raise(new PointerUpEvent());
      playerActions.MiddleClick.performed += _ => EventBus<MouseMiddleDownEvent>.Raise(new MouseMiddleDownEvent());
      playerActions.MiddleClick.canceled += _ => EventBus<PointerUpEvent>.Raise(new PointerUpEvent());

      playerActions.PointerPosition.performed += ctx => EventBus<PointerPositionEvent>.Raise(
                                                   new PointerPositionEvent { screenPosition = ctx.ReadValue<Vector2>() }
                                                 );

      playerActions.PointerDelta.performed += ctx => EventBus<PointerPositionDeltaEvent>.Raise(
                                                new PointerPositionDeltaEvent { delta = ctx.ReadValue<Vector2>() }
                                              );

      playerActions.WheelScroll.performed += ctx => EventBus<MouseWheelScrollEvent>.Raise(
                                               new MouseWheelScrollEvent { delta = ctx.ReadValue<Vector2>() }
                                             );

      inputActions.Enable();
    }

    public void Dispose() {
      inputActions.Disable();
      inputActions.Dispose();
    }
  }
}