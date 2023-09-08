using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the drag rotation behavior of an Rubik's Cube face.
/// </summary>
[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeFaceDragRotationController : DragRotationController {
  #region serializable fields

  [SerializeField]
  protected Camera mainCamera;

  #endregion

  #region fields

  private readonly RCubeFaceDragStartEventInvoker rCubeFaceDragStartEventInvoker = new RCuDragStartEventInvokerImpl();
  private readonly RCubeFaceDragEventInvoker rCubeFaceDragEventInvoker = new RCubeDragEventInvokerImpl();
  private readonly RCubeFaceDragEndEventInvoker rCubeFaceDragEndEventInvoker = new RCubeDragEndEventInvokerImpl();
  private readonly RCubeFaceRotationStartEventInvoker rCubeFaceRotationStartEventInvoker = new RCubeRotationStartEventInvokerImpl();
  private readonly RCubeFaceRotationEventInvoker rCubeFaceRotationEventInvoker = new RCubeFaceRotationEventInvokerImpl();

  private bool isDragging;
  private bool canBeDragged = true;
  private Vector2 pointerPosition;

  private int cubeSideLayer;
  private GlobalIdentifier globalIdentifier;

  #endregion

  #region unity methods

  private void Awake() {
    PlayerInputManager.mouse.LeftClick.started += OnMouseLeftButtonDown;
    PlayerInputManager.mouse.LeftClick.canceled += OnMouseLeftButtonUp;

    EventManager.AddRCubeFaceDragStartInvoker(rCubeFaceDragStartEventInvoker);
    EventManager.AddRCubeFaceDragInvoker(rCubeFaceDragEventInvoker);
    EventManager.AddRCubeFaceDragEndInvoker(rCubeFaceDragEndEventInvoker);

    EventManager.AddRCubeFaceRotationStartInvoker(rCubeFaceRotationStartEventInvoker);
    EventManager.AddRCubeFaceRotationInvoker(rCubeFaceRotationEventInvoker);

    EventManager.AddRCubeFaceRotationStartListener(faceGlobalId => canBeDragged = faceGlobalId.Equals(globalIdentifier.id));
    EventManager.AddRCubeFaceRotationEndListener(_ => canBeDragged = true);

    globalIdentifier = GetComponent<GlobalIdentifier>();
    cubeSideLayer = LayerMask.GetMask("CubeSide");

    currentPointer = Pointer.current;
    pointerPosition = currentPointer.position.ReadValue();
  }

  private void FixedUpdate() {
    if (isDragging) {
      Rotate();
      rCubeFaceDragEventInvoker.Invoke(globalIdentifier.id);
      rCubeFaceRotationEventInvoker.Invoke(globalIdentifier.id);
    }

    ResetInputDelta();
  }

  #endregion

  #region methods

  /// <summary>
  /// Handles the left mouse button down event and checks if a specific cube face can be dragged.
  /// If it can be dragged, invokes Rubik's Cube face drag and rotation start events and starts reading input context to initiate dragging. 
  /// </summary>
  /// <param name="context">The input action callback context.</param>
  private void OnMouseLeftButtonDown(InputAction.CallbackContext context) {
    if (Physics.Raycast(
          mainCamera.ScreenPointToRay(currentPointer.position.ReadValue()),
          out var hit,
          float.PositiveInfinity,
          cubeSideLayer
        )) {
      if (hit.collider.gameObject.GetComponent<GlobalIdentifier>().id == globalIdentifier.id && canBeDragged) {
        rCubeFaceDragStartEventInvoker.Invoke(globalIdentifier.id);
        rCubeFaceRotationStartEventInvoker.Invoke(globalIdentifier.id);
        PlayerInputManager.mouse.Drag.performed += ReadInputContext;
        pointerPosition = hit.point;
        isDragging = true;
      }
    }
  }

  /// <summary>
  /// Handles the left mouse button up event.
  /// If the cube face is currently being dragged, invokes Rubik's Cube face dragging end event and stops reading input context to stop dragging. 
  /// </summary>
  /// <param name="context">The input action callback context.</param>
  private void OnMouseLeftButtonUp(InputAction.CallbackContext context) {
    if (isDragging) {
      rCubeFaceDragEndEventInvoker.Invoke(globalIdentifier.id);
    }

    PlayerInputManager.mouse.Drag.performed -= ReadInputContext;
    pointerPosition = currentPointer.position.ReadValue();
    isDragging = false;
  }
  
  /// <summary>
  /// Rotates the Rubik's Cube face based on the input delta.
  /// </summary>
  protected override void Rotate() {
    var deltaRotation = GetAngle() * rotationSpeed * Time.deltaTime;

    if (accessRotation.y > 0) {
      transform.Rotate(rotationRelativeObject ? rotationRelativeObject.transform.up : Vector3.up, deltaRotation, Space.World);
    }

    if (accessRotation.x > 0) {
      transform.Rotate(rotationRelativeObject ? rotationRelativeObject.transform.right : Vector3.right, deltaRotation, Space.World);
    }

    if (accessRotation.z > 0) {
      transform.Rotate(rotationRelativeObject ? rotationRelativeObject.transform.forward : Vector3.forward, deltaRotation, Space.World);
    }
  }
  
  /// <summary>
  /// Calculates the angle between the input delta vector and a reference vector based on the relative orientation of the rotationRelativeObject.
  /// </summary>
  /// <returns>The calculated angle.</returns>
  private float GetAngle() {
    var currentPointerPosition = Vector2Int.RoundToInt(pointerPosition);
    var facePosition = Vector3Int.RoundToInt(transform.position);
    var relativeUp = Vector3Int.RoundToInt(rotationRelativeObject.transform.up);
    var result = 0f;

    if (relativeUp == Vector3Int.up) {
      result = Vector3.Dot(inputDelta, Vector3.left);
    }

    if (relativeUp == Vector3Int.down) {
      result = Vector3.Dot(inputDelta, Vector3.right);
    }

    if (relativeUp == Vector3Int.left) {
      result = Vector3.Dot(inputDelta, Vector3.down);
    }

    if (relativeUp == Vector3Int.right) {
      result = Vector3.Dot(inputDelta, Vector3.up);
    }

    if (relativeUp == Vector3Int.forward) {
      result = currentPointerPosition.y > facePosition.y
               ? Vector3.Dot(inputDelta, Vector3.left)
               : Vector3.Dot(inputDelta, Vector3.up);
    }

    if (relativeUp == Vector3Int.back) {
      result = currentPointerPosition.y > facePosition.y
               ? Vector3.Dot(inputDelta, Vector3.right)
               : Vector3.Dot(inputDelta, Vector3.down);
    }

    return result;
  }

  #endregion

  #region event invoker classes

  private sealed class RCuDragStartEventInvokerImpl : RCubeFaceDragStartEventInvoker {
    private readonly RCubeFaceDragStartEvent rCubeDragStartEvent = new RCubeFaceDragStartEvent();
    public RCubeFaceDragStartEvent GetEvent() => rCubeDragStartEvent;
  }

  private sealed class RCubeDragEventInvokerImpl : RCubeFaceDragEventInvoker {
    private readonly RCubeFaceDragEvent rCubeDragEvent = new RCubeFaceDragEvent();
    public RCubeFaceDragEvent GetEvent() => rCubeDragEvent;
  }

  private sealed class RCubeDragEndEventInvokerImpl : RCubeFaceDragEndEventInvoker {
    private readonly RCubeFaceDragEndEvent rCubeDragEndEvent = new RCubeFaceDragEndEvent();
    public RCubeFaceDragEndEvent GetEvent() => rCubeDragEndEvent;
  }

  private sealed class RCubeRotationStartEventInvokerImpl : RCubeFaceRotationStartEventInvoker {
    private readonly RCubeFaceRotationStartEvent rCubeRotationStartEvent = new RCubeFaceRotationStartEvent();
    public RCubeFaceRotationStartEvent GetEvent() => rCubeRotationStartEvent;
  }

  private sealed class RCubeFaceRotationEventInvokerImpl : RCubeFaceRotationEventInvoker {
    private readonly RCubeFaceRotationEvent rCubeRotationEvent = new RCubeFaceRotationEvent();
    public RCubeFaceRotationEvent GetEvent() => rCubeRotationEvent;
  }

  #endregion
}