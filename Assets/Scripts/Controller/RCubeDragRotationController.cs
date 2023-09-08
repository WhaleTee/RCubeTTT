using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the drag rotation behavior of the Rubik's Cube.
/// </summary>
[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeDragRotationController : DragRotationController {
  #region serializable fields

  [SerializeField]
  protected Camera mainCamera;

  #endregion

  #region fields

  private readonly RCubeDragStartEventInvoker rCubeDragStartEventInvoker = new RCubeDragStartEventInvokerImpl();
  private readonly RCubeDragEventInvoker rCubeDragEventInvoker = new RCubeDragEventInvokerImpl();
  private readonly RCubeDragEndEventInvoker rCubeDragEndEventInvoker = new RCubeDragEndEventInvokerImpl();

  private bool isDragging;
  private int cubeLayer;

  #endregion

  #region unity methods

  private void Awake() {
    PlayerInputManager.mouse.RightClick.started += OnMouseRightButtonDown;
    PlayerInputManager.mouse.RightClick.canceled += OnMouseRightButtonUp;
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

  /// <summary>
  /// Handles the right mouse button down event.
  /// Invokes Rubik's Cube drag start event and starts reading input context to initiate dragging. 
  /// </summary>
  /// <param name="context">The input action callback context.</param>
  private void OnMouseRightButtonDown(InputAction.CallbackContext context) {
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

  /// <summary>
  /// Handles the right mouse button up event.
  /// If the Rubik's Cube is currently being dragged, invokes Rubik's Cube drag end event and stops reading input context to stop dragging. 
  /// </summary>
  /// <param name="context">The input action callback context.</param>
  private void OnMouseRightButtonUp(InputAction.CallbackContext context) {
    if (isDragging) {
      PlayerInputManager.mouse.Drag.performed -= ReadInputContext;
      rCubeDragEndEventInvoker.Invoke();
      isDragging = false;
    }
  }

  #endregion

  #region event invoker classes

  private sealed class RCubeDragStartEventInvokerImpl : RCubeDragStartEventInvoker {
    private readonly RCubeDragStartEvent rCubeDragStartEvent = new RCubeDragStartEvent();
    public RCubeDragStartEvent GetEvent() => rCubeDragStartEvent;
  }

  private sealed class RCubeDragEventInvokerImpl : RCubeDragEventInvoker {
    private readonly RCubeDragEvent rCubeDragEvent = new RCubeDragEvent();
    public RCubeDragEvent GetEvent() => rCubeDragEvent;
  }

  private sealed class RCubeDragEndEventInvokerImpl : RCubeDragEndEventInvoker {
    private readonly RCubeDragEndEvent rCubeDragEndEvent = new RCubeDragEndEvent();
    public RCubeDragEndEvent GetEvent() => rCubeDragEndEvent;
  }

  #endregion
}