using UnityEngine;
using UnityEngine.InputSystem;

public class CubeRotationController : RotationController {
  #region serializable fields

  [SerializeField]
  protected Camera mainCamera;

  #endregion

  #region fields

  private readonly Invoker startDragRCubeInvoker = new CubeRotationStartDragRCubeInvoker();
  private readonly Invoker dragRCubeInvoker = new CubeRotationDragRCubeInvoker();
  private readonly Invoker endDragRCubeInvoker = new CubeRotationEndDragRCubeInvoker();

  private bool rCubeDragging;

  #endregion

  #region unity methods

  protected override void Awake() {
    base.Awake();
    PlayerInputManager.mouse.RightClick.started += MouseRightDownHandler;
    PlayerInputManager.mouse.RightClick.canceled += MouseRightUpHandler;
    EventManager.AddEndDragRCubeInvoker(endDragRCubeInvoker as EndDragRCubeInvoker);
    EventManager.AddStartDragRCubeInvoker(startDragRCubeInvoker as StartDragRCubeInvoker);
  }

  #endregion

  #region methods

  private void MouseRightDownHandler(InputAction.CallbackContext context) {
    if (Physics.Raycast(
          mainCamera.ScreenPointToRay(currentPointer.position.ReadValue()),
          out var hit,
          float.PositiveInfinity,
          LayerMask.GetMask("Cube")
        )) {
      if (hit.collider.gameObject.GetComponent<CubeRotationController>() != null) {
        PlayerInputManager.mouse.Drag.performed += ReadDragContext;
        startDragRCubeInvoker.Invoke();
        rCubeDragging = true;
      }
    }
  }

  private void MouseRightUpHandler(InputAction.CallbackContext context) {
    if (rCubeDragging) {
      PlayerInputManager.mouse.Drag.performed -= ReadDragContext;
      endDragRCubeInvoker.Invoke();
      rCubeDragging = false;
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