using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeDragRotationController : DragRotationController {
  #region serializable fields

  [SerializeField]
  protected Camera mainCamera;

  #endregion

  #region fields

  private readonly RCubeDragStartEventInvoker rCubeDragStartEventInvoker = new RCubeDragRotationStartEventInvoker();
  private readonly RCubeDragEventInvoker rCubeDragEventInvoker = new RCubeDragRotationEventInvoker();
  private readonly RCubeDragEndEventInvoker rCubeDragEndEventInvoker = new RCubeDragRotationEndEventInvoker();

  private bool isDragging;
  private int cubeLayer;

  #endregion

  #region unity methods

  private void Awake() {
    PlayerInputManager.mouse.RightClick.started += MouseRightDownHandler;
    PlayerInputManager.mouse.RightClick.canceled += MouseRightUpHandler;
    EventManager.AddRCubeDragStartInvoker(rCubeDragStartEventInvoker);
    EventManager.AddRCubeDragEndInvoker(rCubeDragEndEventInvoker);

    currentPointer = Pointer.current;
    cubeLayer = LayerMask.GetMask("Cube");
  }

  private void Update() {
    if (isDragging) {
      Rotate();
    }

    ResetInputDelta();
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
      PlayerInputManager.mouse.Drag.performed += ReadInputContext;
      rCubeDragStartEventInvoker.Invoke();
      isDragging = true;
    }
  }

  private void MouseRightUpHandler(InputAction.CallbackContext context) {
    if (isDragging) {
      PlayerInputManager.mouse.Drag.performed -= ReadInputContext;
      rCubeDragEndEventInvoker.Invoke();
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