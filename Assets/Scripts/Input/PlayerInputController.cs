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

  private readonly InputInvoker mouseRightClickDownInputInvoker = new MouseRightClickDownInputInvoker();
  private readonly InputInvoker mouseRightClickUpInputInvoker = new MouseRightClickUpInputInvoker();
  private readonly InputInvoker mouseDragInputInvoker = new MouseDragCubeInputInvoker();

  #endregion

  #region uinty methods

  private void Awake() {
    mainCamera = Camera.main;
    mouse = Mouse.current;
    RegisterEventInvokers();
  }

  private void Start() {
    rightClick.started += PickUpCube;
    rightClick.canceled += OnMouseUpEvent;
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

  private void RegisterEventInvokers() {
    EventManager.AddMouseRightClickDownInputInvoker(mouseRightClickDownInputInvoker as MouseRightClickDownInputInvoker);
    EventManager.AddMouseRightClickUpInputInvoker(mouseRightClickUpInputInvoker as MouseRightClickUpInputInvoker);
    EventManager.AddMouseDragCubeInputInvoker(mouseDragInputInvoker as MouseDragCubeInputInvoker);
  }

  private void PickUpCube(InputAction.CallbackContext context) {
    if (Physics.Raycast(mainCamera.ScreenPointToRay(mouse.position.ReadValue()), out var hit, float.PositiveInfinity, cubeLayer)) {
      if (hit.collider.gameObject.GetComponent<CubeRotationController>() != null) {
        mouseRightClickDownInputInvoker.Invoke(context);
      }
    }
  }

  private void DragCube(InputAction.CallbackContext context) {
    if (Physics.Raycast(mainCamera.ScreenPointToRay(mouse.position.ReadValue()), out var hit, float.PositiveInfinity, cubeLayer)) {
      if (hit.collider.gameObject.GetComponent<CubeDragController>() != null) {
        drag.performed += mouseDragInputInvoker.Invoke;
      }
    }
  }

  private void OnMouseUpEvent(InputAction.CallbackContext context) {
    mouseRightClickUpInputInvoker.Invoke(context);
  }

  private void CancelDrag(InputAction.CallbackContext context) {
    drag.performed -= mouseDragInputInvoker.Invoke;
  }

  #endregion
}