using UnityEngine;
using UnityEngine.InputSystem;

public class CubeDragRotationController : DragRotationController {
  #region serializable fields

  [SerializeField]
  protected Camera mainCamera;

  #endregion

  #region fields

  private readonly Invoker startDragRCubeInvoker = new CubeRotationStartDragRCubeInvoker();
  private readonly Invoker dragRCubeInvoker = new CubeRotationDragRCubeInvoker();
  private readonly Invoker endDragRCubeInvoker = new CubeRotationEndDragRCubeInvoker();

  private bool isDragging;

  #endregion

  #region unity methods

  private void Awake() {
    currentPointer = Pointer.current;
    PlayerInputManager.mouse.RightClick.started += MouseRightDownHandler;
    PlayerInputManager.mouse.RightClick.canceled += MouseRightUpHandler;
    EventManager.AddEndDragRCubeInvoker(endDragRCubeInvoker as EndDragRCubeInvoker);
    EventManager.AddStartDragRCubeInvoker(startDragRCubeInvoker as StartDragRCubeInvoker);
  }

  private void Update() {
    if (isDragging) {
      Rotate();
    }

    StopDragging();
  }

  #endregion

  #region methods

  private void MouseRightDownHandler(InputAction.CallbackContext context) {
    if (Physics.Raycast(
          mainCamera.ScreenPointToRay(currentPointer.position.ReadValue()),
          out var hit,
          Mathf.Infinity,
          LayerMask.GetMask("Cube")
        )) {
      if (hit.collider.gameObject.GetComponent<CubeDragRotationController>().GetId() == GetId()) {
        PlayerInputManager.mouse.Drag.performed += ReadDragContext;
        startDragRCubeInvoker.Invoke();
        isDragging = true;
      }
    }
  }

  private void MouseRightUpHandler(InputAction.CallbackContext context) {
    if (isDragging) {
      PlayerInputManager.mouse.Drag.performed -= ReadDragContext;
      endDragRCubeInvoker.Invoke();
      isDragging = false;
    }
  }

  #endregion

  #region event invoker classes

  private sealed class CubeRotationStartDragRCubeInvoker : StartDragRCubeInvoker {
    private readonly StartDragRCubeEvent startDragRCubeEvent = new StartDragRCubeEvent();
    public StartDragRCubeEvent GetInputEvent() => startDragRCubeEvent;
  }

  private sealed class CubeRotationDragRCubeInvoker : DragRCubeInvoker {
    private readonly DragRCubeEvent dragRCubeEvent = new DragRCubeEvent();
    public DragRCubeEvent GetInputEvent() => dragRCubeEvent;
  }

  private sealed class CubeRotationEndDragRCubeInvoker : EndDragRCubeInvoker {
    private readonly EndDragRCubeEvent endDragRCubeEvent = new EndDragRCubeEvent();
    public EndDragRCubeEvent GetInputEvent() => endDragRCubeEvent;
  }

  #endregion
}