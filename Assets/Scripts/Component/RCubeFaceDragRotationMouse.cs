using System;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class RCubeFaceDragRotationMouse {
  private readonly RCubeFaceDragStartEventInvoker rCubeFaceDragStartEventInvoker = new RCubeFaceDragStartEventInvokerImpl();
  private readonly RCubeFaceDragEndEventInvoker rCubeFaceDragEndEventInvoker = new RCubeFaceDragEndEventInvokerImpl();
  private readonly RCubeFaceRotationStartEventInvoker rCubeFaceRotationStartEventInvoker = new RCubeFaceRotationStartEventInvokerImpl();

  private readonly Camera raycastCamera;
  private readonly Func<Vector2> pointerPosition;
  private readonly LayerMask cubeFaceLayerMask;

  private string draggedFaceId;

  public RCubeFaceDragRotationMouse(Camera raycastCamera, Func<Vector2> pointerPosition) {
    this.raycastCamera = raycastCamera;
    this.pointerPosition = pointerPosition;

    PlayerInputManager.mouse.LeftClick.performed += OnMouseLeftButtonDown;
    PlayerInputManager.mouse.LeftClick.canceled += OnMouseLeftButtonUp;

    EventManager.AddRCubeFaceDragStartInvoker(rCubeFaceDragStartEventInvoker);
    EventManager.AddRCubeFaceDragEndInvoker(rCubeFaceDragEndEventInvoker);
    EventManager.AddRCubeFaceRotationStartInvoker(rCubeFaceRotationStartEventInvoker);

    cubeFaceLayerMask = LayerMask.GetMask("CubeFace");
  }

  /// <summary>
  /// Handles the left mouse button down event.
  /// Performs a raycast from the <see cref="raycastCamera"/>'s position to the <see cref="pointerPosition"/>,
  /// and if it hits a Rubik's Cube face invokes Rubik's Cube face drag start event.
  /// </summary>
  /// <param name="context">The input action callback context.</param>
  private void OnMouseLeftButtonDown(InputAction.CallbackContext context) {
    if (Physics.Raycast(raycastCamera.ScreenPointToRay(pointerPosition.Invoke()), out var hit, float.PositiveInfinity)) {
      if (LayerMaskUtils.EqualsMaskToLayer(cubeFaceLayerMask, hit.collider.gameObject.layer)) {
        draggedFaceId = hit.collider.gameObject.GetComponent<RCubeFaceDragRotationController>().faceGlobalId;
        rCubeFaceDragStartEventInvoker.Invoke(new RCubeFaceRaycastHitEventContext(draggedFaceId, hit.point));
        rCubeFaceRotationStartEventInvoker.Invoke(draggedFaceId);
      }
    }
  }

  /// <summary>
  /// Handles the left mouse button up event.
  /// If one of the cube faces is currently being dragged, invokes Rubik's Cube face dragging end event for it face. 
  /// </summary>
  /// <param name="context">The input action callback context.</param>
  private void OnMouseLeftButtonUp(InputAction.CallbackContext context) {
    if (draggedFaceId != null) {
      rCubeFaceDragEndEventInvoker.Invoke(draggedFaceId);
    }

    draggedFaceId = null;
  }
}