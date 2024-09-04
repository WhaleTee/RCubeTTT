using RCubeTTT.Commons;
using RCubeTTT.EventSystem;
using RCubeTTT.EventSystem.EventContext;
using RCubeTTT.EventSystem.EventInvoker;
using RCubeTTT.EventSystem.EventInvoker.Impl;
using RCubeTTT.InputSystem;
using RCubeTTT.Model;
using UnityEngine;

namespace RCubeTTT.Controller
{
  /// <summary>
  /// Controls the behavior of a Rubik's Cube face.
  /// </summary>
  public class RCubeFaceDragRotationController : DragRotationController {
    [field: SerializeField]
    public RCubeFacePositionType facePositionType { get; private set; }

    #region fields

    private readonly IRotationEventInvoker rotationEventInvoker = new RotationEventInvoker();
    private readonly IDragEventInvoker dragEventInvoker = new DragEventInvoker();

    private bool canBeDragged = true;
    private bool isDragging;
    private Vector2 faceHitPosition;

    #endregion

    #region unity methods

    private void Awake() {
      RCubeFaceEventManager.AddDragStartListener(OnDragStart);
      RCubeFaceEventManager.AddDragEndListener(OnDragEnd);
      
      RCubeFaceEventManager.AddRotationStartListener(OnRotationStart);
      RCubeFaceEventManager.AddRotationEndListener(OnRotationEnd);

      RCubeFaceEventManager.AddDragPerformedInvoker(dragEventInvoker);
      RCubeFaceEventManager.AddRotationPerformedInvoker(rotationEventInvoker);
    }

    private void OnRotationEnd(RotationEventContext context) {
      canBeDragged = true;
    }

    private void OnRotationStart(RotationEventContext context) {
      canBeDragged = context.instanceId == GetInstanceID();
    }

    private void Update() {
      if (isDragging) {
        Rotate();

        var instanceID = GetInstanceID();
        var currentTransform = transform;
        var rotation = currentTransform.rotation;
        var localRotation = currentTransform.localRotation;

        dragEventInvoker.Invoke(new ObjectInstanceContext(instanceID));
        rotationEventInvoker.Invoke(new RotationEventContext(instanceID, rotation, localRotation));
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
    /// <param name="context">The <see cref="InputHitObjectEventContext"/> containing information about the Rubik's Cube face hit and global ID.</param>
    private void OnDragStart(InputHitObjectEventContext context) {
      canBeDragged = context.instanceId == GetInstanceID();

      if (canBeDragged) {
        PlayerInputManager.mouse.PointerDelta.performed += ReadInputContext;
        faceHitPosition = context.hitPosition;
        isDragging = true;
      }
    }

    /// <summary> 
    /// Handles the Rubik's Cube face drag end event.
    /// It saves the position where the face was hit, removes the drag input listener, and sets the isDragging flag to false. 
    /// </summary> 
    private void OnDragEnd(ObjectInstanceContext context) {
      PlayerInputManager.mouse.PointerDelta.performed -= ReadInputContext;
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
        result = currentPointerPosition.y > facePosition.y ? Vector3.Dot(inputDelta, Vector3.left) : Vector3.Dot(inputDelta, Vector3.up);
      }

      if (relativeUp == Vector3Int.back) {
        result = currentPointerPosition.y > facePosition.y ? Vector3.Dot(inputDelta, Vector3.right)  : Vector3.Dot(inputDelta, Vector3.down);
      }

      return result;
    }

    #endregion
  }
}