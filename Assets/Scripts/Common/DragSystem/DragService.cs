using System.Collections.Generic;
using System.Linq;
using Common.EventSystem.Bus;
using UnityEngine;

namespace Common.DragSystem {
  public class DragService : MonoBehaviour {
    private const int MAX_HITS = 8;

    [SerializeField]
    private LayerMask draggableLayer;

    private Camera mainCamera;
    private Vector3 pointerScreenPosition;
    private readonly Dictionary<DragComponent, RaycastHit> rayHits = new Dictionary<DragComponent, RaycastHit>();
    private DragComponent currentDragComponent;

    private void Awake() {
      mainCamera = Camera.main;

      EventBus<PointerPositionEvent>.Register(new EventBinding<PointerPositionEvent>(OnPointerPosition));
      EventBus<PointerDownEvent>.Register(new EventBinding<PointerDownEvent>(RaycastDraggables));
      EventBus<PointerUpEvent>.Register(new EventBinding<PointerUpEvent>(InvokeDragEnd));
    }

    private void RaycastDraggables() {
      var hits = new RaycastHit[MAX_HITS];
      Physics.RaycastNonAlloc(mainCamera.ScreenPointToRay(pointerScreenPosition), hits, float.MaxValue, draggableLayer);

      foreach (var hit in hits.Where(hit => hit.collider).OrderBy(hit => hit.distance)) {
        if (hit.collider.gameObject.GetComponent(typeof(DragComponent)) as DragComponent is { } component) rayHits.Add(component, hit);
      }
    }

    private void InvokeDragStart() {
      if (rayHits.Count == 0) return;

      foreach (var (component, hit) in rayHits) {
        if (component.IsDragAllowed()) {
          currentDragComponent = component;
          EventBus<ObjectDragBeginEvent>.Raise(new ObjectDragBeginEvent { instanceId = hit.collider.gameObject.GetInstanceID() });
          break;
        }
      }

      if (currentDragComponent) FlushRayHits();
    }

    private void InvokeDrag() {
      if (currentDragComponent) EventBus<ObjectDragEvent>.Raise(new ObjectDragEvent { instanceId = currentDragComponent.gameObject.GetInstanceID() });
    }

    private void InvokeDragEnd() {
      if (currentDragComponent)
        EventBus<ObjectDragEndEvent>.Raise(new ObjectDragEndEvent { instanceId = currentDragComponent.gameObject.GetInstanceID() });

      currentDragComponent = null;
      FlushRayHits();
    }

    private void FlushRayHits() => rayHits.Clear();

    private void OnPointerPosition(PointerPositionEvent ctx) {
      pointerScreenPosition = ctx.screenPosition;
      InvokeDragStart();
      InvokeDrag();
    }
  }
}