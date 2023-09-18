using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles mouse input for dragging a Rubik's Cube.
/// </summary>
public sealed class RCubeDragRotationMouseHandler {
  private readonly RCubeDragStartEventInvoker rCubeDragStartEventInvoker = new RCubeDragStartEventInvokerImpl();
  private readonly RCubeDragEndEventInvoker rCubeDragEndEventInvoker = new RCubeDragEndEventInvokerImpl();

  private readonly Camera raycastCamera;
  private readonly Func<Vector2> pointerPosition;
  private readonly LayerMask cubeLayerMask;

  public RCubeDragRotationMouseHandler(Camera raycastCamera, Func<Vector2> pointerPosition) {
    this.raycastCamera = raycastCamera;
    this.pointerPosition = pointerPosition;

    PlayerInputManager.mouse.RightClick.performed += OnMouseRightButtonDown;
    PlayerInputManager.mouse.RightClick.canceled += OnMouseRightButtonUp;

    EventManager.AddRCubeDragStartInvoker(rCubeDragStartEventInvoker);
    EventManager.AddRCubeDragEndInvoker(rCubeDragEndEventInvoker);

    cubeLayerMask = LayerMask.GetMask("Cube");
  }

  /// <summary>
  /// Handles the right mouse button down event.
  /// Performs a raycast from the <see cref="raycastCamera"/>'s position to the <see cref="pointerPosition"/>,
  /// and if it hits a Rubik's Cube invokes Rubik's Cube drag start event. 
  /// </summary>
  /// <param name="context">The input action callback context.</param>
  private void OnMouseRightButtonDown(InputAction.CallbackContext context) {
    if (Physics.Raycast(raycastCamera.ScreenPointToRay(pointerPosition.Invoke()), out var _, Mathf.Infinity, cubeLayerMask)) {
      rCubeDragStartEventInvoker.Invoke();
    }
  }

  /// <summary>
  /// Handles the right mouse button up event.
  /// If the Rubik's Cube is currently being dragged, invokes Rubik's Cube drag end event. 
  /// </summary>
  /// <param name="context">The input action callback context.</param>
  private void OnMouseRightButtonUp(InputAction.CallbackContext context) {
    rCubeDragEndEventInvoker.Invoke();
  }
}