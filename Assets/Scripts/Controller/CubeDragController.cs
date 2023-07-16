using UnityEngine;
using UnityEngine.InputSystem;

public class CubeDragController : MonoBehaviour {
  #region serializable fields

  [SerializeField]
  private Vector3 rotationPoint;

  [SerializeField]
  [Range(10, 100)]
  private float sensitivity;

  [SerializeField]
  private Camera targetCamera;

  #endregion

  #region fields

  private Vector2 dragDeltaInput;

  #endregion

  #region properties

  private Transform mainCameraTransform => targetCamera.transform;

  #endregion

  #region unity methods

  private void Awake() => EventManager.AddMouseDragCubeInputListener(ReadDragContext);

  private void Update() {
    RotateCube();
    StopDragging();
  }

  #endregion

  #region methods

  private void RotateCube() {
    transform.RotateAround(
      rotationPoint,
      mainCameraTransform.up,
      -Vector3.Dot(dragDeltaInput, mainCameraTransform.right) * sensitivity * Time.deltaTime
    );

    transform.RotateAround(
      rotationPoint,
      mainCameraTransform.right,
      Vector3.Dot(dragDeltaInput, mainCameraTransform.up) * sensitivity * Time.deltaTime
    );
  }

  private void StopDragging() => dragDeltaInput = Vector2.zero;

  private void ReadDragContext(InputAction.CallbackContext context) => dragDeltaInput = context.ReadValue<Vector2>();

  #endregion
}