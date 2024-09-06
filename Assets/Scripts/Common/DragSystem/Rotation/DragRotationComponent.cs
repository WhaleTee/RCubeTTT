using Common.EventSystem.Bus;
using Common.InputSystem;
using UnityEngine;

namespace Common.DragSystem.Rotation {
  public class DragRotationComponent : DragComponent {
    [Header("Drag Rotation Settings")]
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Transform relative;

    [SerializeField]
    private bool rotateX;

    [SerializeField]
    private bool rotateY;

    [SerializeField]
    private uint speed;

    protected Camera mainCamera;
    protected Vector2 pointerDelta;

    private Transform targetTransform => target ? target : transform;
    private Transform relativeTransform => relative ? relative : mainCamera.transform;

    private void Awake() {
      mainCamera = Camera.main;

      EventBus<PointerPositionDeltaEvent>.Register(new EventBinding<PointerPositionDeltaEvent>(ctx => pointerDelta = ctx.delta));

      EventBus<ObjectDragEvent>.Register(
        new EventBinding<ObjectDragEvent>(
          ctx => {
            if (ctx.instanceId == gameObject.GetInstanceID()) Rotate(pointerDelta);
          }
        )
      );
    }

    protected virtual void Rotate(Vector2 pointerDelta) {
      var rotationAngle = pointerDelta * (speed * Time.deltaTime);

      if (rotateX) {
        targetTransform.Rotate(relativeTransform.up, Vector3.Dot(rotationAngle, Vector3.left), Space.World);
      }

      if (rotateY) {
        targetTransform.Rotate(relativeTransform.right, Vector3.Dot(rotationAngle, Vector3.up), Space.World);
      }
    }

    private float GetRotation(Vector2 pointerDelta, Vector3 hitPosition, Vector3Int facePosition, Vector3Int relativeUp) {
      return relativeUp switch {
               var v when v.Equals(Vector3Int.up) => Vector3.Dot(pointerDelta, Vector3.left),
               var v when v.Equals(Vector3Int.down) => Vector3.Dot(pointerDelta, Vector3.right),
               var v when v.Equals(Vector3Int.left) => Vector3.Dot(pointerDelta, Vector3.down),
               var v when v.Equals(Vector3Int.right) => Vector3.Dot(pointerDelta, Vector3.up),
               var v when v.Equals(Vector3Int.forward) => hitPosition.y > facePosition.y
                                                          ? Vector3.Dot(pointerDelta, Vector3.left)
                                                          : Vector3.Dot(pointerDelta, Vector3.up),
               var v when v.Equals(Vector3Int.back) => hitPosition.y > facePosition.y
                                                       ? Vector3.Dot(pointerDelta, Vector3.right)
                                                       : Vector3.Dot(pointerDelta, Vector3.down),
               var _ => 0
             };
    }
  }
}