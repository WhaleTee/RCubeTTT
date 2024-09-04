using System;
using RCubeTTT.EventSystem;
using RCubeTTT.EventSystem.EventInvoker;
using RCubeTTT.EventSystem.EventInvoker.Impl;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInputManager = RCubeTTT.InputSystem.PlayerInputManager;

namespace RCubeTTT.Handler
{
  /// <summary>
  /// Handles mouse input for dragging a Rubik's Cube.
  /// </summary>
  public sealed class RCubeMouseDragRotationHandler {
    private readonly IUnityEventInvoker rCubeDragStartEventInvoker = new UnityEventInvoker();
    private readonly IUnityEventInvoker rCubeDragEndEventInvoker = new UnityEventInvoker();

    private readonly Camera rayCastCamera;
    private readonly Func<Vector2> pointerPosition;
    private readonly LayerMask cubeLayerMask;

    public RCubeMouseDragRotationHandler(Camera rayCastCamera, Func<Vector2> pointerPosition) {
      this.rayCastCamera = rayCastCamera;
      this.pointerPosition = pointerPosition;

      PlayerInputManager.mouse.Click.performed += OnMouseRightButtonDown;
      PlayerInputManager.mouse.Click.canceled += OnMouseRightButtonUp;

      RCubeEventManager.AddDragStartInvoker(rCubeDragStartEventInvoker);
      RCubeEventManager.AddDragEndInvoker(rCubeDragEndEventInvoker);

      cubeLayerMask = LayerMask.GetMask("Cube");
    }

    /// <summary>
    /// Handles the right mouse button down event.
    /// Performs a ray cast from the <see cref="rayCastCamera"/>'s position to the <see cref="pointerPosition"/>,
    /// and if it hits a Rubik's Cube invokes Rubik's Cube drag start event. 
    /// </summary>
    /// <param name="context">The input action callback context.</param>
    private void OnMouseRightButtonDown(InputAction.CallbackContext context) {
      if (Physics.Raycast(rayCastCamera.ScreenPointToRay(pointerPosition.Invoke()), out var _, Mathf.Infinity, cubeLayerMask)) {
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
}