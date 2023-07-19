using UnityEngine;
using UnityEngine.InputSystem;

public class CubeSideRotationController : RotationController {
  #region serializable fields

  [SerializeField]
  [RangeVector(new float[] { }, new float[] { 1, 1, 1 })]
  private Vector2Int accessRotation;

  #endregion

  #region properties

  private Transform parentTransform => gameObject.transform.parent.transform;

  #endregion

  #region unity methods

  protected override void Awake() {
    base.Awake();
    PlayerInputManager.mouse.LeftClick.started += MouseLeftDownHandler;
    PlayerInputManager.mouse.LeftClick.canceled += MouseLeftUpHandler;
  }

  #endregion

  #region methods

  private void MouseLeftDownHandler(InputAction.CallbackContext context) {
    if (Physics.Raycast(
          targetCamera.ScreenPointToRay(currentPointer.position.ReadValue()),
          out var hit,
          float.PositiveInfinity,
          LayerMask.GetMask("CubeSide")
        )) {
      if (hit.collider.gameObject.GetComponent<CubeSideRotationController>() != null) {
        dragging = true;
        PlayerInputManager.mouse.Drag.performed += ReadDragContext;
      }
    }
  }

  private void MouseLeftUpHandler(InputAction.CallbackContext context) {
    PlayerInputManager.mouse.Drag.performed -= ReadDragContext;
  }

  protected override void Rotate() {
    if (accessRotation.x > 0) {
      transform.RotateAround(
        rotationPoint,
        parentTransform.right,
        Vector3.Dot(dragDeltaInput, parentTransform.up) * rotationSpeed * Time.deltaTime
      );
    }

    if (accessRotation.y > 0) {
      transform.RotateAround(
        rotationPoint,
        parentTransform.up,
        Vector3.Dot(dragDeltaInput, parentTransform.right) * rotationSpeed * Time.deltaTime
      );
    }
  }

  #endregion
}