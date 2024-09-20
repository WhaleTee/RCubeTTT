using System.Collections.Generic;
using System.Linq;
using Common.EventSystem.Bus;
using UnityEngine;

namespace Common.DragSystem {
  public class DragService : MonoBehaviour {
    private const int MAX_HIT_BUFFER = 8;

    [SerializeField] private LayerMask draggableLayer;

    [SerializeField]
    [Range(0, MAX_HIT_BUFFER)]
    private int maxHits = MAX_HIT_BUFFER;

    private Camera mainCamera;

    /// <summary>
    /// screen position
    /// </summary>
    private Vector3 pointerPosition;

    /// <summary>
    /// hit position on the screen
    /// </summary>
    private Vector3 pointerHitPosition;

    private readonly Dictionary<DragComponent, RaycastHit> rayHits = new Dictionary<DragComponent, RaycastHit>();
    private DragComponent currentDrag;

    private void Awake() {
      mainCamera = Camera.main;

      EventBus<PointerDownEvent>.Register(new EventBinding<PointerDownEvent>(RaycastDraggables));
      EventBus<PointerUpEvent>.Register(new EventBinding<PointerUpEvent>(InvokeDragEnd));
      EventBus<PointerPositionEvent>.Register(new EventBinding<PointerPositionEvent>(OnPointerPosition));
    }

    private void RaycastDraggables() {
      var hits = new RaycastHit[MAX_HIT_BUFFER];
      Physics.RaycastNonAlloc(mainCamera.ScreenPointToRay(pointerPosition), hits, float.MaxValue, draggableLayer);

      foreach (var hit in hits.Where(hit => hit.collider).OrderBy(hit => hit.distance).Take(maxHits)) {
        if (hit.collider.gameObject.GetComponent(typeof(DragComponent)) is DragComponent component) rayHits.TryAdd(component, hit);
      }

      if (rayHits.Count > 0) pointerHitPosition = pointerPosition;
      
      EventBus<RaycastBeforeDragBeginEvent>.Raise(
        new RaycastBeforeDragBeginEvent {
          hitObjects = rayHits.Keys.Select(component => component.targetInstanceId).ToArray(), pointerPosition = pointerHitPosition
        }
      );
    }

    private void InvokeDragStart() {
      var (component, hit) = rayHits.FirstOrDefault(pair => pair.Key.IsDragAllowed());

      if (component != null) {
        currentDrag = component;

        EventBus<DragBeginEvent>.Raise(
          new DragBeginEvent {
            instanceId = component.targetInstanceId, pointerPosition = pointerHitPosition, hitPoint = hit.point, hitNormal = hit.normal
          }
        );

        FlushRayHits();
      }
    }

    private void InvokeDrag() {
      if (currentDrag != null) EventBus<DragEvent>.Raise(new DragEvent { instanceId = currentDrag.targetInstanceId });
    }

    private void InvokeDragEnd() {
      if (currentDrag != null) EventBus<DragEndEvent>.Raise(new DragEndEvent { instanceId = currentDrag.targetInstanceId });

      currentDrag = null;
      FlushRayHits();
    }

    private void FlushRayHits() => rayHits.Clear();

    private void OnPointerPosition(PointerPositionEvent ctx) {
      pointerPosition = ctx.screenPosition;
      InvokeDragStart();
      InvokeDrag();
    }
  }
}