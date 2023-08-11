using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeFaceDragRotationController : DragRotationController {
  #region serializable fields

  [SerializeField]
  protected Camera mainCamera;

  [SerializeField]
  private CubeFaceSide side;

  #endregion

  #region fields

  private readonly Invoker startDragRCubeFaceInvoker = new CubeFaceRotationStartDragRCubeFaceInvoker();
  private readonly Invoker dragRCubeFaceInvoker = new CubeFaceRotationDragRCubeFaceInvoker();
  private readonly Invoker endDragRCubeFaceInvoker = new CubeFaceRotationEndDragRCubeFaceInvoker();
  private bool isDragging;
  private Vector2 pointerPosition = Vector2.negativeInfinity;
  private CubeFaceRotation cubeFaceRotation;

  #endregion

  #region unity methods

  private void Awake() {
    currentPointer = Pointer.current;
    
    PlayerInputManager.mouse.LeftClick.started += MouseLeftDownHandler;
    PlayerInputManager.mouse.LeftClick.canceled += MouseLeftUpHandler;
    EventManager.AddStartDragRCubeFaceInvoker(startDragRCubeFaceInvoker as StartDragRCubeFaceInvoker);
    EventManager.AddEndDragRCubeFaceInvoker(endDragRCubeFaceInvoker as EndDragRCubeFaceInvoker);
    
    cubeFaceRotation = new CubeFaceRotation(() => pointerPosition);
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
          LayerMask.GetMask("CubeSide")
        )) {
      if (hit.collider.gameObject.GetComponent<CubeFaceDragRotationController>().GetId() == GetId()) {
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
    if (accessRotation.y > 0) {
      var deltaRotation = cubeFaceRotation.GetAngle(dragDeltaInput, gameObject, rotationRelativeObject) * rotationSpeed * Time.deltaTime;
      transform.Rotate(rotationRelativeObject ? rotationRelativeObject.transform.up : Vector3.up, deltaRotation, Space.World);
    }

    if (accessRotation.x > 0) {
      var deltaRotation = cubeFaceRotation.GetAngle(dragDeltaInput, gameObject, rotationRelativeObject) * rotationSpeed * Time.deltaTime;
      transform.Rotate(rotationRelativeObject ? rotationRelativeObject.transform.right : Vector3.right, deltaRotation, Space.World);
    }

    if (accessRotation.z > 0) {
      var deltaRotation = cubeFaceRotation.GetAngle(dragDeltaInput, gameObject, rotationRelativeObject) * rotationSpeed * Time.deltaTime;
      transform.Rotate(rotationRelativeObject ? rotationRelativeObject.transform.forward : Vector3.forward, deltaRotation, Space.World);
    }
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