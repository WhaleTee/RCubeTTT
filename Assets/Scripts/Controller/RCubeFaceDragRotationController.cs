using UnityEngine;

/// <summary>
/// Controls the behavior of an Rubik's Cube face.
/// </summary>
public class RCubeFaceDragRotationController : DragRotationController {
  [SerializeField]
  public GlobalIdentifier faceIdentifier;

  public string faceGlobalId => faceIdentifier.id;

  [field: SerializeField]
  public RCubeFacePositionType facePositionType { get; private set; }

  #region fields

  private readonly RCubeFaceRotationEventInvoker rCubeFaceRotationEventInvoker = new RCubeFaceRotationEventInvokerImpl();
  private readonly RCubeFaceDragEventInvoker rCubeFaceDragEventInvoker = new RCubeFaceDragEventInvokerImpl();

  private bool canBeDragged = true;
  private bool isDragging;
  private Vector2 faceHitPosition;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddRCubeFaceDragStartListener(OnRCubeFaceDragStart);
    EventManager.AddRCubeFaceDragEndListener(OnRCubeFaceDragEnd);
    EventManager.AddRCubeFaceRotationStartListener(faceGlobalId => canBeDragged = faceGlobalId.Equals(faceIdentifier.id));
    EventManager.AddRCubeFaceRotationEndListener(_ => canBeDragged = true);

    EventManager.AddRCubeFaceDragInvoker(rCubeFaceDragEventInvoker);
    EventManager.AddRCubeFaceRotationInvoker(rCubeFaceRotationEventInvoker);
  }

  private void Update() {
    if (isDragging) {
      Rotate();
      rCubeFaceDragEventInvoker.Invoke(faceIdentifier.id);
      rCubeFaceRotationEventInvoker.Invoke(new RCubeFaceRotationEventContext(faceIdentifier.id, facePositionType));
    }

    ResetInputDelta();
  }

  #endregion

  #region methods

  /// <summary> 
  /// Handles the Rubik's Cube face drag start event.
  /// It checks if the face can be dragged based on the provided context, and if so, it adds a drag input listener,
  /// saves the hit position of the face, and sets the isDragging flag to true. 
  /// </summary> 
  /// <param name="context">The <see cref="RCubeFaceDragStartEventContext"/> containing information about the Rubik's Cube face hit and global ID.</param>
  private void OnRCubeFaceDragStart(RCubeFaceDragStartEventContext context) {
    canBeDragged = context.faceGlobalId.Equals(faceGlobalId);

    if (canBeDragged) {
      PlayerInputManager.mouse.Drag.performed += ReadInputContext;
      faceHitPosition = context.faceHitPosition;
      isDragging = true;
    }
  }

  /// <summary> 
  /// Handles the Rubik's Cube face drag end event.
  /// It saves the position where the face was hit, removes the drag input listener, and sets the isDragging flag to false. 
  /// </summary> 
  /// <param name="faceGlobalId">Represents the Rubik's Cube face's global UUID.</param>
  private void OnRCubeFaceDragEnd(string faceGlobalId) {
    faceHitPosition = PlayerInputManager.mouse.Position.ReadValue<Vector2>();
    PlayerInputManager.mouse.Drag.performed -= ReadInputContext;
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
    var currentPointerPosition = Vector2Int.RoundToInt(faceHitPosition);
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

  private sealed class RCubeFaceDragEventInvokerImpl : RCubeFaceDragEventInvoker {
    private readonly RCubeFaceDragEvent rCubeDragEvent = new RCubeFaceDragEvent();
    public RCubeFaceDragEvent GetEvent() => rCubeDragEvent;
  }

  private sealed class RCubeFaceRotationEventInvokerImpl : RCubeFaceRotationEventInvoker {
    private readonly RCubeFaceRotationEvent rCubeRotationEvent = new RCubeFaceRotationEvent();
    public RCubeFaceRotationEvent GetEvent() => rCubeRotationEvent;
  }

  #endregion
}