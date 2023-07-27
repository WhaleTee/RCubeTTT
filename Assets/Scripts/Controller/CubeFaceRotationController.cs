using UnityEngine;
using UnityEngine.InputSystem;

public class CubeFaceRotationController : RotationController {
  #region serializable fields

  [SerializeField]
  protected Camera mainCamera;

  #endregion

  #region fields

  private readonly Invoker startDragRCubeFaceInvoker = new CubeFaceRotationStartDragRCubeFaceInvoker();
  private readonly Invoker dragRCubeFaceInvoker = new CubeFaceRotationDragRCubeFaceInvoker();
  private readonly Invoker endDragRCubeFaceInvoker = new CubeFaceRotationEndDragRCubeFaceInvoker();

  #endregion

  #region unity methods

  protected override void Awake() {
    base.Awake();
    PlayerInputManager.mouse.LeftClick.started += MouseLeftDownHandler;
    PlayerInputManager.mouse.LeftClick.canceled += MouseLeftUpHandler;
    EventManager.AddStartDragRCubeFaceInvoker(startDragRCubeFaceInvoker as StartDragRCubeFaceInvoker);
    EventManager.AddEndDragRCubeFaceInvoker(endDragRCubeFaceInvoker as EndDragRCubeFaceInvoker);
  }

  #endregion

  #region methods

  private void MouseLeftDownHandler(InputAction.CallbackContext context) {
    if (Physics.Raycast(
          mainCamera.ScreenPointToRay(currentPointer.position.ReadValue()),
          out var hit,
          float.PositiveInfinity,
          LayerMask.GetMask("CubeSide")
        )) {
      if (hit.collider.gameObject.GetComponent<CubeFaceRotationController>() != null) {
        startDragRCubeFaceInvoker.Invoke();
        PlayerInputManager.mouse.Drag.performed += ReadDragContext;
      }
    }
  }

  private void MouseLeftUpHandler(InputAction.CallbackContext context) {
    PlayerInputManager.mouse.Drag.performed -= ReadDragContext;
    endDragRCubeFaceInvoker.Invoke();
  }

  #endregion

  #region event invoker classes

  private sealed class CubeFaceRotationStartDragRCubeFaceInvoker : StartDragRCubeFaceInvoker {
    private readonly StartDragRCubeEvent startDragRCubeEvent = new StartDragRCubeEvent();
    public StartDragRCubeEvent GetInputEvent() => startDragRCubeEvent;
  }

  private sealed class CubeFaceRotationDragRCubeFaceInvoker : DragRCubeFaceInvoker {
    private readonly DragRCubeEvent dragRCubeEvent = new DragRCubeEvent();
    public DragRCubeEvent GetInputEvent() => dragRCubeEvent;
  }

  private sealed class CubeFaceRotationEndDragRCubeFaceInvoker : EndDragRCubeFaceInvoker {
    private readonly EndDragRCubeEvent endDragRCubeEvent = new EndDragRCubeEvent();
    public EndDragRCubeEvent GetInputEvent() => endDragRCubeEvent;
  }

  #endregion
}