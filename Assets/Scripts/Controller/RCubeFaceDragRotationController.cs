using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeFaceDragRotationController : DragRotationController {
  #region serializable fields

  [SerializeField]
  protected Camera mainCamera;

  #endregion

  #region fields

  private readonly Invoker startDragRCubeFaceInvoker = new CubeFaceRotationStartDragRCubeFaceInvoker();
  private readonly Invoker dragRCubeFaceInvoker = new CubeFaceRotationDragRCubeFaceInvoker();
  private readonly Invoker endDragRCubeFaceInvoker = new CubeFaceRotationEndDragRCubeFaceInvoker();

  private int cubeSideLayer;
  private bool isDragging;
  private Vector2 pointerPosition = Vector2.negativeInfinity;
  
  private GlobalIdentifier globalIdentifier;

  #endregion

  #region unity methods

  private void Awake() {
    PlayerInputManager.mouse.LeftClick.started += MouseLeftDownHandler;
    PlayerInputManager.mouse.LeftClick.canceled += MouseLeftUpHandler;
    EventManager.AddStartDragRCubeFaceInvoker(startDragRCubeFaceInvoker as StartDragRCubeFaceInvoker);
    EventManager.AddEndDragRCubeFaceInvoker(endDragRCubeFaceInvoker as EndDragRCubeFaceInvoker);

    globalIdentifier = GetComponent<GlobalIdentifier>();

    currentPointer = Pointer.current;
    cubeSideLayer = LayerMask.GetMask("CubeSide");
  }

  private void Update() {
    if (isDragging) {
      Rotate();
    }

    StopDragging();
  }

  #endregion

  #region methods

  private void MouseLeftDownHandler(InputAction.CallbackContext context) {

    if (Physics.Raycast(
          mainCamera.ScreenPointToRay(currentPointer.position.ReadValue()),
          out var hit,
          float.PositiveInfinity,
          cubeSideLayer
        )) {
      if (hit.collider.gameObject.GetComponent<GlobalIdentifier>().id == globalIdentifier.id) {
        startDragRCubeFaceInvoker.Invoke();
        PlayerInputManager.mouse.Drag.performed += ReadDragContext;
        isDragging = true;
        pointerPosition = hit.point;
      }
    }
  }

  private void MouseLeftUpHandler(InputAction.CallbackContext context) {
    PlayerInputManager.mouse.Drag.performed -= ReadDragContext;
    endDragRCubeFaceInvoker.Invoke();
    isDragging = false;
    pointerPosition = Vector2.negativeInfinity;
  }

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

  private float GetAngle() {
    var currentPointerPosition = Vector2Int.RoundToInt(pointerPosition);
    var facePosition = Vector3Int.RoundToInt(transform.position);
    var relativeUp = Vector3Int.RoundToInt(rotationRelativeObject.transform.up);
    var result = 0f;

    if (relativeUp == Vector3Int.up) {
      result = Vector3.Dot(dragDeltaInput, Vector3.left);
    }

    if (relativeUp == Vector3Int.down) {
      result = Vector3.Dot(dragDeltaInput, Vector3.right);
    }

    if (relativeUp == Vector3Int.left) {
      result = Vector3.Dot(dragDeltaInput, Vector3.down);
    }

    if (relativeUp == Vector3Int.right) {
      result = Vector3.Dot(dragDeltaInput, Vector3.up);
    }

    if (relativeUp == Vector3Int.forward) {
      result = currentPointerPosition.y > facePosition.y
               ? Vector3.Dot(dragDeltaInput, Vector3.left)
               : Vector3.Dot(dragDeltaInput, Vector3.up);
    }

    if (relativeUp == Vector3Int.back) {
      result = currentPointerPosition.y > facePosition.y
               ? Vector3.Dot(dragDeltaInput, Vector3.right)
               : Vector3.Dot(dragDeltaInput, Vector3.down);
    }

    return result;
  }

  #endregion

  #region event invoker classes

  private sealed class CubeFaceRotationStartDragRCubeFaceInvoker : StartDragRCubeFaceInvoker {
    private readonly StartRCubeDragEvent startRCubeDragEvent = new StartRCubeDragEvent();
    public StartRCubeDragEvent GetInputEvent() => startRCubeDragEvent;
  }

  private sealed class CubeFaceRotationDragRCubeFaceInvoker : DragRCubeFaceInvoker {
    private readonly RCubeDragEvent rCubeDragEvent = new RCubeDragEvent();
    public RCubeDragEvent GetInputEvent() => rCubeDragEvent;
  }

  private sealed class CubeFaceRotationEndDragRCubeFaceInvoker : EndDragRCubeFaceInvoker {
    private readonly EndRCubeDragEvent endRCubeDragEvent = new EndRCubeDragEvent();
    public EndRCubeDragEvent GetInputEvent() => endRCubeDragEvent;
  }

  #endregion
}