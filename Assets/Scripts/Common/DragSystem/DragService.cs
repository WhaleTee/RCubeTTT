using System.Collections.Generic;
using System.Linq;
using Common.EventSystem.Bus;
using UnityEngine;

namespace Common.DragSystem {
  public class DragService : MonoBehaviour {
    [SerializeField]
    private LayerMask draggableLayer;

    [SerializeField]
    private ushort maxHits = 8;

    private Camera mainCamera;
    private Vector3 pointerScreenPosition;
    private readonly Dictionary<DragComponent, RaycastHit> rayHits = new Dictionary<DragComponent, RaycastHit>();
    private DragComponent currentDrag;

    private void Awake() {
      mainCamera = Camera.main;

      EventBus<PointerDownEvent>.Register(new EventBinding<PointerDownEvent>(RaycastDraggables));
      EventBus<PointerUpEvent>.Register(new EventBinding<PointerUpEvent>(InvokeDragEnd));
      EventBus<PointerPositionEvent>.Register(new EventBinding<PointerPositionEvent>(OnPointerPosition));
    }

    private void RaycastDraggables() {
      var hits = new RaycastHit[maxHits];
      Physics.RaycastNonAlloc(mainCamera.ScreenPointToRay(pointerScreenPosition), hits, float.MaxValue, draggableLayer);

      foreach (var hit in hits.Where(hit => hit.collider).OrderBy(hit => hit.distance)) {
        if (hit.collider.gameObject.GetComponent(typeof(DragComponent)) as DragComponent is { } component) rayHits.TryAdd(component, hit);
      }

      EventBus<RaycastBeforeDragBeginEvent>.Raise(
        new RaycastBeforeDragBeginEvent { hitObjects = rayHits.Keys.Select(component => component.instanceId).ToArray() }
      );
    }

    private void InvokeDragStart() {
      var (component, hit) = rayHits.FirstOrDefault(pair => pair.Key.IsDragAllowed());

      if (component != null) {
        currentDrag = component;

        EventBus<DragBeginEvent>.Raise(
          new DragBeginEvent {
            instanceId = component.instanceId, pointerScreenPosition = pointerScreenPosition, hitPoint = hit.point, hitNormal = hit.normal
          }
        );

        FlushRayHits();
      }
    }

    private void InvokeDrag() {
      if (currentDrag != null) EventBus<DragEvent>.Raise(new DragEvent { instanceId = currentDrag.instanceId });
    }

    private void InvokeDragEnd() {
      if (currentDrag != null) EventBus<DragEndEvent>.Raise(new DragEndEvent { instanceId = currentDrag.instanceId });

      currentDrag = null;
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