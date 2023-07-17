using UnityEngine;
using UnityEngine.InputSystem;

public class CubeRotationController : MonoBehaviour {
  #region serializable fields

  [SerializeField]
  private Vector3 rotationPoint;

  [SerializeField]
  [Range(10, 100)]
  private float rotationSpeed;

  [SerializeField]
  [Range(1, 3)]
  private float rotate90Duration;

  [SerializeField]
  private Camera targetCamera;

  #endregion

  #region fields

  private Pointer currentPointer;
  private bool dragging;
  private float timeElapsed;
  private Vector2 dragDeltaInput;

  #endregion

  #region properties

  private Transform mainCameraTransform => targetCamera.transform;

  private Quaternion rotation => transform.rotation;

  #endregion

  #region unity methods

  private void Awake() {
    currentPointer = Pointer.current;
    PlayerInputManager.mouse.RightClick.started += MouseRightDownHandler;
    PlayerInputManager.mouse.RightClick.canceled += MouseRightUpHandler;
  }

  private void Update() {
    RotateCube();
    StopDragging();
    RotateTo90Degrees();
  }

  #endregion

  #region methods

  private void MouseRightDownHandler(InputAction.CallbackContext context) {
    if (Physics.Raycast(targetCamera.ScreenPointToRay(currentPointer.position.ReadValue()), out var hit, float.PositiveInfinity, LayerMask.GetMask("Cube"))) {
      if (hit.collider.gameObject.GetComponent<CubeRotationController>() != null) {
        dragging = true;
        PlayerInputManager.mouse.Drag.performed += ReadDragContext;
      }
    }
  }

  private void MouseRightUpHandler(InputAction.CallbackContext context) {
    if (dragging) {
      timeElapsed = 0;
      dragging = false;
      PlayerInputManager.mouse.Drag.performed -= ReadDragContext;
    }
  }

  private void RotateTo90Degrees() {
    if (!dragging) {
      var startRotation = rotation;
      var targetRotation = GetClosest90DegreesRotation();

      transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / rotate90Duration);
      timeElapsed += Time.deltaTime;
    }
  }

  private Quaternion GetClosest90DegreesRotation() {
    var closestRotation = Quaternion.Euler(0, 0, 0);
    var closestAngle = Quaternion.Angle(rotation, closestRotation);

    for (var x = -180; x <= 180; x += 90) {
      for (var y = -180; y <= 180; y += 90) {
        for (var z = -180; z <= 180; z += 90) {
          var targetRotation = Quaternion.Euler(x, y, z);
          var angle = Quaternion.Angle(rotation, targetRotation);

          if (angle < closestAngle) {
            closestRotation = targetRotation;
            closestAngle = angle;
          }
        }
      }
    }

    return closestRotation;
  }

  private void RotateCube() {
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

  private void StopDragging() => dragDeltaInput = Vector2.zero;

  private void ReadDragContext(InputAction.CallbackContext context) => dragDeltaInput = context.ReadValue<Vector2>();

  #endregion
}