using System.Collections.Generic;
using System.Linq;
using Common.DragSystem.Component;
using Common.EventBus;
using UnityEngine;

namespace Common.DragSystem.Service {
  public sealed class DragService {
    private const int MAX_HIT_BUFFER = 8;

    private readonly LayerMask draggableLayer;
    private readonly Camera raycastCamera;
    private readonly int maxHits;
    private readonly Dictionary<DragComponent, RaycastHit> rayHits = new Dictionary<DragComponent, RaycastHit>();

    private DragComponent currentDrag;
    private Vector3 pointerScreenPosition;
    private Vector3 pointerHitPositionOnScreen;

    public DragService(LayerMask draggableLayer, int maxHits, Camera raycastCamera) {
      this.draggableLayer = draggableLayer;
      this.maxHits = maxHits;
      this.raycastCamera = raycastCamera;

      EventBus<PointerDownEvent>.Register(new EventBinding<PointerDownEvent>(RaycastDraggables));
      EventBus<PointerUpEvent>.Register(new EventBinding<PointerUpEvent>(InvokeDragEnd));
      EventBus<PointerPositionEvent>.Register(new EventBinding<PointerPositionEvent>(OnPointerPosition));
    }

    private void RaycastDraggables() {
      var hits = new RaycastHit[MAX_HIT_BUFFER];
      Physics.RaycastNonAlloc(raycastCamera.ScreenPointToRay(pointerScreenPosition), hits, float.MaxValue, draggableLayer);

      foreach (var hit in hits.Where(hit => hit.collider).OrderBy(hit => hit.distance).Take(maxHits)) {
        if (ServiceLocator.ServiceLocator.For(hit.collider).TryGet(out DragComponent component)) {
          rayHits.TryAdd(component, hit);
        }
      }

      if (rayHits.Count > 0) pointerHitPositionOnScreen = pointerScreenPosition;

      EventBus<RaycastBeforeDragBeginEvent>.Raise(
        new RaycastBeforeDragBeginEvent {
          hitObjects = rayHits.Keys.Select(component => component.GetTargetInstanceId()).ToArray(), pointerPosition = pointerHitPositionOnScreen
        }
      );
    }

    private void InvokeDragStart() {
      var (component, hit) = rayHits.FirstOrDefault(pair => pair.Key.IsDragAllowed());

      if (component != null) {
        currentDrag = component;

        EventBus<DragBeginEvent>.Raise(
          new DragBeginEvent {
            instanceId = component.GetTargetInstanceId(), pointerPosition = pointerHitPositionOnScreen, hitPoint = hit.point, hitNormal = hit.normal
          }
        );

        FlushRayHits();
      }
    }

    private void InvokeDrag() {
      if (currentDrag != null) EventBus<DragEvent>.Raise(new DragEvent { instanceId = currentDrag.GetTargetInstanceId() });
    }

    private void InvokeDragEnd() {
      if (currentDrag != null) EventBus<DragEndEvent>.Raise(new DragEndEvent { instanceId = currentDrag.GetTargetInstanceId() });

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