using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour {
  #region seiralizable fields

  [SerializeField]
  private InputAction leftClick;

  [SerializeField]
  private InputAction rightClick;

  [SerializeField]
  private InputAction drag;

  [SerializeField]
  private LayerMask cubeLayer;

  #endregion

  #region fields

  private Mouse mouse;
  private Camera mainCamera;

  #endregion

  #region event fields

  private readonly InputInvoker mouseDownInputInvoker = new MouseDownInputInvoker();
  private readonly InputInvoker mouseDragInputInvoker = new MouseDragInputInvoker();
  private readonly InputInvoker mouseUpInputInvoker = new MouseUpInputInvoker();

  #endregion

  #region uinty methods

  private void Awake() {
    mainCamera = Camera.main;
    mouse = Mouse.current;
    EventManager.AddMouseDownInputInvoker(mouseDownInputInvoker as MouseDownInputInvoker);
    EventManager.AddMouseDragInputInvoker(mouseDragInputInvoker as MouseDragInputInvoker);
    EventManager.AddMouseUpInputInvoker(mouseUpInputInvoker as MouseUpInputInvoker);
  }

  private void Start() {
    rightClick.started += PickUpCube;
    rightClick.canceled += DropDownCube;
    rightClick.started += DragCube;
    rightClick.canceled += CancelDrag;
  }

  private void OnEnable() {
    rightClick.Enable();
    leftClick.Enable();
    drag.Enable();
  }

  private void OnDisable() {
    rightClick.Disable();
    leftClick.Disable();
    drag.Disable();
  }

  #endregion

  #region methods

  private void PickUpCube(InputAction.CallbackContext context) {
    if (Physics.Raycast(mainCamera.ScreenPointToRay(mouse.position.ReadValue()), out var hit, cubeLayer)) {
      if (hit.collider.gameObject.GetComponent<CubeRotationController>() != null) {
        mouseDownInputInvoker.Invoke(context);
      }
    }
  }

  private void DragCube(InputAction.CallbackContext context) {
    if (Physics.Raycast(mainCamera.ScreenPointToRay(mouse.position.ReadValue()), out var hit, cubeLayer)) {
      if (hit.collider.gameObject.GetComponent<CubeController>() != null) {
        drag.performed += mouseDragInputInvoker.Invoke;
      }
    }
  }

  private void DropDownCube(InputAction.CallbackContext context) {
    mouseUpInputInvoker.Invoke(context);
  }

  private void CancelDrag(InputAction.CallbackContext context) {
    drag.performed -= mouseDragInputInvoker.Invoke;
  }

  #endregion
}