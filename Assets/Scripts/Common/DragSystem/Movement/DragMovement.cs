using Common.EventSystem.Bus;
using UnityEngine;

namespace Common.DragSystem.Movement {
  public class DragMovement : MonoBehaviour, DragComponent {
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

    protected void Awake() {
      mainCamera = Camera.main;

      EventBus<PointerPositionEvent>.Register(new EventBinding<PointerPositionEvent>(ctx => pointerPosition = ctx.screenPosition));

      EventBus<DragBeginEvent>.Register(
        new EventBinding<DragBeginEvent>(
          ctx => {
            if (ctx.instanceId == instanceId) pointerOffset = pointerPosition - (Vector2)mainCamera.WorldToScreenPoint(transform.position); 
          })
      );

      EventBus<DragEvent>.Register(
        new EventBinding<DragEvent>(
          ctx => {
            if (ctx.instanceId == gameObject.GetInstanceID()) Move(pointerPosition - pointerOffset);
          })
      );
    }

    protected virtual void Move(Vector2 targetPosition) {
      if (targetPosition == transformScreenPosition) return;

      var velocity = (Vector2.Lerp(transformScreenPosition, targetPosition, speed * Time.deltaTime) - transformScreenPosition) * Time.deltaTime;

      if (moveX) transform.Translate(velocity.x, 0, 0, cameraTransform);

      if (moveY) transform.Translate(0, velocity.y, 0, cameraTransform);
    }

    public int instanceId => gameObject.GetInstanceID();
    public bool IsDragAllowed() => true;
  }
}