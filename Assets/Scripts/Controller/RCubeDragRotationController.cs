using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeDragRotationController : DragRotationController {
  #region serializable fields

  [SerializeField]
  protected Camera mainCamera;

  #endregion

  #region fields

  private readonly Invoker startDragRCubeInvoker = new RCubeRotationStartDragRCubeInvoker();
  private readonly Invoker dragRCubeInvoker = new RCubeRotationDragRCubeInvoker();
  private readonly Invoker endDragRCubeInvoker = new RCubeRotationEndDragRCubeInvoker();

  private bool isDragging;
  private int cubeLayer;

  private GlobalIdentifier globalIdentifier;

  #endregion

  #region unity methods

  private void Awake() {
    PlayerInputManager.mouse.RightClick.started += MouseRightDownHandler;
    PlayerInputManager.mouse.RightClick.canceled += MouseRightUpHandler;
    EventManager.AddEndDragRCubeInvoker(endDragRCubeInvoker as EndDragRCubeInvoker);
    EventManager.AddStartDragRCubeInvoker(startDragRCubeInvoker as StartDragRCubeInvoker);

    globalIdentifier = GetComponent<GlobalIdentifier>();

    currentPointer = Pointer.current;
    cubeLayer = LayerMask.GetMask("Cube");
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
          cubeLayer
        )) {
      if (hit.collider.gameObject.GetComponent<GlobalIdentifier>().id == globalIdentifier.id) {
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

  private sealed class RCubeRotationStartDragRCubeInvoker : StartDragRCubeInvoker {
    private readonly StartRCubeDragEvent startRCubeDragEvent = new StartRCubeDragEvent();
    public StartRCubeDragEvent GetInputEvent() => startRCubeDragEvent;
  }

  private sealed class RCubeRotationDragRCubeInvoker : DragRCubeInvoker {
    private readonly RCubeDragEvent rCubeDragEvent = new RCubeDragEvent();
    public RCubeDragEvent GetInputEvent() => rCubeDragEvent;
  }

  private sealed class RCubeRotationEndDragRCubeInvoker : EndDragRCubeInvoker {
    private readonly EndRCubeDragEvent endRCubeDragEvent = new EndRCubeDragEvent();
    public EndRCubeDragEvent GetInputEvent() => endRCubeDragEvent;
  }

  #endregion
}