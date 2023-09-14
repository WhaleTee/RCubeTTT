using UnityEngine;
using UnityEngine.InputSystem;

public class PointerRaycastController : MonoBehaviour {
  #region serializable fields

  [SerializeField]
  private Camera raycastCamera;

  #endregion

  #region fields

  private Vector2 pointerPosition;

  #endregion

  #region unity methods

  private void Awake() {
    PlayerInputManager.mouse.Position.performed += OnMousePositionPerformed;
    _ = new RCubeDragRotationMouse(raycastCamera, () => pointerPosition);
    _ = new RCubeFaceDragRotationMouse(raycastCamera, () => pointerPosition);
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