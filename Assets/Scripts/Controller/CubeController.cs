using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : MonoBehaviour {
  #region serializable fields

  [SerializeField]
  private Vector3 center;

  [SerializeField]
  [Range(10, 100)]
  private float rotationSpeed;

  #endregion

  #region fields

  private Camera mainCamera;
  private Vector2 dragDeltaInput;
  

  #endregion

  #region properties

  private Transform mainCameraTransform => mainCamera.transform;
  

  #endregion

  #region unity methods

  private void Awake() {
    mainCamera = Camera.main;
    EventManager.AddMouseDragInputListener(ReadMouseDragContext);
  }

  private void Update() {
    transform.RotateAround(center, mainCameraTransform.up, -Vector3.Dot(dragDeltaInput, mainCameraTransform.right) * rotationSpeed * Time.deltaTime);
    transform.RotateAround(center, mainCameraTransform.right, Vector3.Dot(dragDeltaInput, mainCameraTransform.up) * rotationSpeed * Time.deltaTime);
    FreezePosition();
    dragDeltaInput = Vector2.zero;
  }

  #endregion

  #region methods

  private void ReadMouseDragContext(InputAction.CallbackContext context) => dragDeltaInput = context.ReadValue<Vector2>();

  private void FreezePosition() {
    transform.position = Vector3.zero;
  }

  #endregion
}