using Common.InputSystem;
using RCubeTTT.Handler;
using RCubeTTT.Manager;
using RCubeTTT.Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RCubeTTT.InputSystem
{
  /// <summary>
  /// Controls the input behavior of the player.
  /// </summary>
  public class PlayerInputController : MonoBehaviour {
    #region seiralizable fields

    [SerializeField]
    private bool enableMouseInput;

    [SerializeField]
    private bool enableScreenInput;

    [SerializeField]
    private Camera raycastCamera;

    [SerializeField]
    private PlayerPlayData playerXData;

    [SerializeField]
    private PlayerPlayData playerOData;

    #endregion

    #region fields

    private Vector2 pointerPosition;

    #endregion

    #region unity methods

    private void Awake() {
      _ = new PlayerTurnManager(playerXData, playerOData);
      _ = new PlayerWinConditionHandler(playerXData, playerOData);

      if (enableMouseInput) {
        PlayerInputManager.mouse.PointerPosition.performed += OnMousePositionPerformed;
        _ = new RCubeMouseDragRotationHandler(raycastCamera, () => pointerPosition);
        _ = new RCubeFaceMouseDragRotationHandler(raycastCamera, () => pointerPosition);
        new InputActions().Enable();
      }
    }

    private void OnEnable() {
      if (enableMouseInput) {
        PlayerInputManager.EnableMouseInput();
      }

      if (enableScreenInput) { }
    }

    private void OnDisable() {
      if (enableMouseInput) {
        PlayerInputManager.DisableMouseInput();
      }

      if (enableScreenInput) { }
    }

    #endregion

    #region methods

    /// <summary>
    /// Handles the mouse position change event.
    /// Updates the pointer position based on the mouse position.
    /// </summary>
    /// <param name="context">The input action callback context.</param>
    private void OnMousePositionPerformed(InputAction.CallbackContext context) => pointerPosition = context.ReadValue<Vector2>();

    #endregion
  }
}