using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeFaceDragRotationController : DragRotationController {
  #region serializable fields

  [SerializeField]
  protected Camera mainCamera;

  #endregion

  #region fields

  private readonly Invoker startDragRCubeFaceInvoker = new CubeFaceRotationStartDragRCubeFaceInvoker();
  private readonly Invoker dragRCubeFaceInvoker = new CubeFaceRotationDragRCubeFaceInvoker();
  private readonly Invoker endDragRCubeFaceInvoker = new CubeFaceRotationEndDragRCubeFaceInvoker();
  private int cubeSideLayer;
  private int cubePieceLayer;

  private ObjectScanner objectScanner;

  private bool isDragging;
  private Vector2 pointerPosition = Vector2.negativeInfinity;
  private CubePiece[] cubePieces;

  #endregion

  #region unity methods

  private void Awake() {
    currentPointer = Pointer.current;

    PlayerInputManager.mouse.LeftClick.started += MouseLeftDownHandler;
    PlayerInputManager.mouse.LeftClick.canceled += MouseLeftUpHandler;
    
    EventManager.AddStartDragRCubeFaceInvoker(startDragRCubeFaceInvoker as StartDragRCubeFaceInvoker);
    EventManager.AddEndDragRCubeFaceInvoker(endDragRCubeFaceInvoker as EndDragRCubeFaceInvoker);

    cubeSideLayer = LayerMask.GetMask("CubeSide");
    cubePieceLayer = LayerMask.GetMask("CubePiece");
    
    objectScanner = GetComponent<ObjectScanner>();
  }

  private void Update() {
    if (isDragging) {
      Rotate();
      RotateCubePieces();
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
      if (hit.collider.gameObject.GetComponent<CubeFaceDragRotationController>().GetId() == GetId()) {
        startDragRCubeFaceInvoker.Invoke();
        PlayerInputManager.mouse.Drag.performed += ReadDragContext;
        isDragging = true;
        pointerPosition = hit.point;
        cubePieces = FindCubePieces();
      }
    }
  }

  private CubePiece[] FindCubePieces() => objectScanner.ScanForLayer(9, cubePieceLayer).Select(go => go.GetComponent<CubePiece>()).ToArray();

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

  private void RotateCubePieces() {
    var deltaRotation = GetAngle() * rotationSpeed * Time.deltaTime;

    foreach (var cubePiece in cubePieces) {
      if (accessRotation.y > 0) {
        cubePiece.transform.RotateAround(
          transform.position,
          rotationRelativeObject ? rotationRelativeObject.transform.up : Vector3.up,
          deltaRotation
        );
      }

      if (accessRotation.x > 0) {
        cubePiece.transform.RotateAround(
          transform.position,
          rotationRelativeObject ? rotationRelativeObject.transform.right : Vector3.right,
          deltaRotation
        );
      }

      if (accessRotation.z > 0) {
        cubePiece.transform.RotateAround(
          transform.position,
          rotationRelativeObject ? rotationRelativeObject.transform.forward : Vector3.forward,
          deltaRotation
        );
      }
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