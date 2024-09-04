using System.Collections.Generic;
using System.Linq;
using Common.EventSystem.Context;
using Common.EventSystem.Invoker;
using Common.InputSystem;
using UnityEngine;

namespace Common.DragSystem {
  public class DragService : MonoBehaviour {
    private const int MAX_HITS = 8;

    [SerializeField]
    private LayerMask draggableLayer;

    private readonly IEventInvoker<RaycastHitContext> dragStartInvoker = new RaycastHitContextInvoker();
    private readonly IEventInvoker<GameObjectContext> dragInvoker = new GameObjectContextInvoker();
    private readonly IEventInvoker<GameObjectContext> dragEndInvoker = new GameObjectContextInvoker();

    private Camera mainCamera;
    private Vector3 pointerScreenPosition;
    private readonly Dictionary<DragComponent, RaycastHit> rayHits = new Dictionary<DragComponent, RaycastHit>();
    private DragComponent currentDragComponent;

    private void Awake() {
      mainCamera = Camera.main;

      DragEventManager.AddDragStartInvoker(dragStartInvoker);
      DragEventManager.AddDragInvoker(dragInvoker);
      DragEventManager.AddDragEndInvoker(dragEndInvoker);

      PlayerInputEventManager.AddPointerPositionListener(OnPointerPosition);
      PlayerInputEventManager.AddPointerClickListener(RaycastDraggables);
      PlayerInputEventManager.AddPointerClickUpListener(InvokeDragEnd);
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
          dragStartInvoker.Invoke(new RaycastHitContext(hit));
          break;
        }
      }

      if (currentDragComponent) FlushRayHits();
    }

    private void InvokeDrag() {
      if (currentDragComponent) dragInvoker.Invoke(new GameObjectContext(currentDragComponent.gameObject));
    }

    private void InvokeDragEnd() {
      if (currentDragComponent) dragEndInvoker.Invoke(new GameObjectContext(currentDragComponent.gameObject));
      currentDragComponent = null;
      FlushRayHits();
    }

    private void FlushRayHits() => rayHits.Clear();

    private void OnPointerPosition(PositionContext ctx) {
      pointerScreenPosition = ctx.screenPosition;
      InvokeDragStart();
      InvokeDrag();
    }
  }
}