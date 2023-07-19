using UnityEngine;
using UnityEngine.InputSystem;

public class CubeRotationController : RotationController {
  #region properties

  private Transform mainCameraTransform => targetCamera.transform;

  #endregion

  #region unity methods

  protected override void Awake() {
    base.Awake();
    PlayerInputManager.mouse.RightClick.started += MouseRightDownHandler;
    PlayerInputManager.mouse.RightClick.canceled += MouseRightUpHandler;
  }

  #endregion

  #region methods

  private void MouseRightDownHandler(InputAction.CallbackContext context) {
    if (Physics.Raycast(
          targetCamera.ScreenPointToRay(currentPointer.position.ReadValue()),
          out var hit,
          float.PositiveInfinity,
          LayerMask.GetMask("Cube")
        )) {
      if (hit.collider.gameObject.GetComponent<CubeRotationController>() != null) {
        dragging = true;
        PlayerInputManager.mouse.Drag.performed += ReadDragContext;
      }
    }
  }

  private void MouseRightUpHandler(InputAction.CallbackContext context) {
    PlayerInputManager.mouse.Drag.performed -= ReadDragContext;
  }

  protected override void Rotate() {
    transform.RotateAround(
      rotationPoint,
      mainCameraTransform.up,
      -Vector3.Dot(dragDeltaInput, mainCameraTransform.right) * rotationSpeed * Time.deltaTime
    );

    transform.RotateAround(
      rotationPoint,
      mainCameraTransform.right,
      Vector3.Dot(dragDeltaInput, mainCameraTransform.up) * rotationSpeed * Time.deltaTime
    );
  }

  #endregion
}