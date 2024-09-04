using Common.InputSystem;
using JetBrains.Annotations;
using UnityEngine;

namespace Common.DragSystem.Movement {
  public class DragMovementComponent : DragComponent {
    [Header("Drag Movement Settings")]
    [SerializeField]
    private bool moveX;

    [SerializeField]
    private bool moveY;

    [SerializeField]
    private uint speed;

    protected Camera mainCamera;
    protected Vector2 pointerPosition;
    protected Vector2 pointerOffset;

    private Transform cameraTransform => mainCamera.transform;
    private Vector2 transformScreenPosition => mainCamera.WorldToScreenPoint(transform.position);

    private void Awake() {
      mainCamera = Camera.main;

      PlayerInputEventManager.AddPointerPositionListener(ctx => pointerPosition = ctx.screenPosition);

      DragEventManager.AddDragStartListener(
        ctx => {
          if (ctx.hit.collider.gameObject == gameObject) pointerOffset = pointerPosition - (Vector2)mainCamera.WorldToScreenPoint(transform.position);
        }
      );

      DragEventManager.AddDragListener(ctx => { if (ctx.gameObject == gameObject) Move(pointerPosition - pointerOffset); });
    }

    protected virtual void Move(Vector2 targetPosition) {
      if (targetPosition == transformScreenPosition) return;

      var velocity = (Vector2.Lerp(transformScreenPosition, targetPosition, speed * Time.deltaTime) - transformScreenPosition) * Time.deltaTime;

      if (moveX) transform.Translate(velocity.x, 0, 0, cameraTransform);

      if (moveY) transform.Translate(0, velocity.y, 0, cameraTransform);
    }
  }
}