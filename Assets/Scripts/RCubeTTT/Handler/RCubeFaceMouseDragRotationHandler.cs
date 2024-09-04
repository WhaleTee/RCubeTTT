using System;
using RCubeTTT.Controller;
using RCubeTTT.EventSystem;
using RCubeTTT.EventSystem.EventContext;
using RCubeTTT.EventSystem.EventInvoker;
using RCubeTTT.EventSystem.EventInvoker.Impl;
using RCubeTTT.Model;
using Support;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInputManager = RCubeTTT.InputSystem.PlayerInputManager;

namespace RCubeTTT.Handler
{
  public sealed class RCubeFaceMouseDragRotationHandler {
    private readonly InputHitObjectEventInvoker dragStartEventInvoker = new InputHitObjectEventInvokerImpl();
    private readonly IDragEventInvoker dragEndEventInvoker = new DragEventInvoker();
    private readonly IRotationEventInvoker rotationStartEventInvoker = new RotationEventInvoker();

    private readonly Camera rayCastCamera;
    private readonly Func<Vector2> pointerPosition;
    private readonly LayerMask cubeFaceLayerMask;

    private PlayerPlayData activePlayer;
    private int draggedFaceId;
    private bool isFaceRotating;

    public RCubeFaceMouseDragRotationHandler(Camera rayCastCamera, Func<Vector2> pointerPosition) {
      this.rayCastCamera = rayCastCamera;
      this.pointerPosition = pointerPosition;

      PlayerInputManager.mouse.Click.performed += OnMouseLeftButtonDown;
      PlayerInputManager.mouse.Click.canceled += OnMouseLeftButtonUp;

      EventManager.AddPlayerTurnStartListener(OnPlayerTurnStarted);

      RCubeFaceEventManager.AddDragStartInvoker(dragStartEventInvoker);
      RCubeFaceEventManager.AddDragEndInvoker(dragEndEventInvoker);
      RCubeFaceEventManager.AddRotationStartInvoker(rotationStartEventInvoker);

      cubeFaceLayerMask = LayerMask.GetMask("CubeFace");
    }

    private void OnPlayerTurnStarted(PlayerPlayData context) {
      activePlayer = context;
    }

    /// <summary>
    /// Handles the left mouse button down event.
    /// Performs a ray cast from the <see cref="rayCastCamera"/>'s position to the <see cref="pointerPosition"/>,
    /// and if it hits a Rubik's Cube face invokes Rubik's Cube face drag start event.
    /// </summary>
    /// <param name="context">The input action callback context.</param>
    private void OnMouseLeftButtonDown(InputAction.CallbackContext context) {
      if (activePlayer.canDragCubeFace) {
        if (Physics.Raycast(rayCastCamera.ScreenPointToRay(pointerPosition.Invoke()), out var hit, float.PositiveInfinity)) {
          if (LayerMaskUtils.EqualsMaskToLayer(cubeFaceLayerMask, hit.collider.gameObject.layer)) {
            var dragRotationController = hit.collider.gameObject.GetComponent<RCubeFaceDragRotationController>();
            var faceTransform = dragRotationController.transform;
            draggedFaceId = dragRotationController.GetInstanceID();
            dragStartEventInvoker.Invoke(new InputHitObjectEventContext(draggedFaceId, hit.point));

            if (!isFaceRotating) {
              rotationStartEventInvoker.Invoke(new RotationEventContext(draggedFaceId, faceTransform.rotation, faceTransform.localRotation));
            }
          }
        }
      }
    }

    /// <summary>
    /// Handles the left mouse button up event.
    /// If one of the cube faces is currently being dragged, invokes Rubik's Cube face dragging end event for it face. 
    /// </summary>
    /// <param name="context">The input action callback context.</param>
    private void OnMouseLeftButtonUp(InputAction.CallbackContext context) {
      if (draggedFaceId != 0) {
        dragEndEventInvoker.Invoke(new ObjectInstanceContext(draggedFaceId));
      }

      draggedFaceId = 0;
    }
  }
}