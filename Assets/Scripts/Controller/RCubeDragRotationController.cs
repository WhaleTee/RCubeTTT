using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeDragRotationController : DragRotationController {
  #region serializable fields

  [SerializeField]
  protected Camera mainCamera;

  #endregion

  #region fields

  private readonly Invoker startDragRCubeInvoker = new RCubeDragRotationStartEventInvoker();
  private readonly Invoker dragRCubeInvoker = new RCubeDragRotationEventInvoker();
  private readonly Invoker endDragRCubeInvoker = new RCubeDragRotationEndEventInvoker();

  private bool isDragging;
  private int cubeLayer;

  private GlobalIdentifier globalIdentifier;

  #endregion

  #region unity methods

  private void Awake() {
    PlayerInputManager.mouse.RightClick.started += MouseRightDownHandler;
    PlayerInputManager.mouse.RightClick.canceled += MouseRightUpHandler;
    EventManager.AddRCubeDragEndInvoker(endDragRCubeInvoker as RCubeDragEndEventInvoker);
    EventManager.AddRCubeDragStartInvoker(startDragRCubeInvoker as RCubeDragStartEventInvoker);

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

  private sealed class RCubeDragRotationStartEventInvoker : RCubeDragStartEventInvoker {
    private readonly RCubeDragStartEvent rCubeDragStartEvent = new RCubeDragStartEvent();
    public RCubeDragStartEvent GetEvent() => rCubeDragStartEvent;
  }

  private sealed class RCubeDragRotationEventInvoker : RCubeDragEventInvoker {
    private readonly RCubeDragEvent rCubeDragEvent = new RCubeDragEvent();
    public RCubeDragEvent GetEvent() => rCubeDragEvent;
  }

  private sealed class RCubeDragRotationEndEventInvoker : RCubeDragEndEventInvoker {
    private readonly RCubeDragEndEvent rCubeDragEndEvent = new RCubeDragEndEvent();
    public RCubeDragEndEvent GetEvent() => rCubeDragEndEvent;
  }

  #endregion
}