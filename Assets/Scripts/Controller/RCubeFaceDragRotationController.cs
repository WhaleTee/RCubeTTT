using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GlobalIdentifier))]
public class RCubeFaceDragRotationController : DragRotationController {
  #region serializable fields

  [SerializeField]
  protected Camera mainCamera;

  #endregion

  #region fields

  private readonly RCubeFaceDragStartEventInvoker rCubeFaceDragStartEventInvoker = new RCubeFaceDragRotationStartEventInvoker();
  private readonly RCubeFaceDragEventInvoker rCubeFaceDragEventInvoker = new RCubeFaceDragRotationEventInvoker();
  private readonly RCubeFaceDragEndEventInvoker rCubeFaceDragEndEventInvoker = new RCubeFaceDragRotationEndEventInvoker();
  private readonly HashSet<string> cubeFacesInRotationState = new HashSet<string>();

  private int cubeSideLayer;
  private bool isDragging;
  private Vector2 pointerPosition = Vector2.negativeInfinity;

  private GlobalIdentifier globalIdentifier;

  #endregion

  #region unity methods

  private void Awake() {
    PlayerInputManager.mouse.LeftClick.started += MouseLeftDownHandler;
    PlayerInputManager.mouse.LeftClick.canceled += MouseLeftUpHandler;

    EventManager.AddRCubeFaceDragStartInvoker(rCubeFaceDragStartEventInvoker);
    EventManager.AddRCubeFaceDragEndInvoker(rCubeFaceDragEndEventInvoker);

    EventManager.AddRCubeFaceRotationStartListener(faceGlobalId => cubeFacesInRotationState.Add(faceGlobalId));
    EventManager.AddRCubeFaceRotationEndListener(faceGlobalId => cubeFacesInRotationState.Remove(faceGlobalId));

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
      if (
        hit.collider.gameObject.GetComponent<GlobalIdentifier>().id == globalIdentifier.id
        && cubeFacesInRotationState.Count == 0
        || cubeFacesInRotationState.Contains(globalIdentifier.id)
      ) {
        rCubeFaceDragStartEventInvoker.Invoke(globalIdentifier.id);
        PlayerInputManager.mouse.Drag.performed += ReadDragContext;
        isDragging = true;
        pointerPosition = hit.point;
      }
    }
  }

  private void MouseLeftUpHandler(InputAction.CallbackContext context) {
    PlayerInputManager.mouse.Drag.performed -= ReadDragContext;
    rCubeFaceDragEndEventInvoker.Invoke(globalIdentifier.id);
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

  private sealed class RCubeFaceDragRotationStartEventInvoker : RCubeFaceDragStartEventInvoker {
    private readonly RCubeFaceDragStartEvent rCubeDragStartEvent = new RCubeFaceDragStartEvent();
    public RCubeFaceDragStartEvent GetEvent() => rCubeDragStartEvent;
  }

  private sealed class RCubeFaceDragRotationEventInvoker : RCubeFaceDragEventInvoker {
    private readonly RCubeFaceDragEvent rCubeDragEvent = new RCubeFaceDragEvent();
    public RCubeFaceDragEvent GetEvent() => rCubeDragEvent;
  }

  private sealed class RCubeFaceDragRotationEndEventInvoker : RCubeFaceDragEndEventInvoker {
    private readonly RCubeFaceDragEndEvent rCubeDragEndEvent = new RCubeFaceDragEndEvent();
    public RCubeFaceDragEndEvent GetEvent() => rCubeDragEndEvent;
  }

  #endregion
}