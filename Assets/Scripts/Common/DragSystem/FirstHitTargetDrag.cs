using Common.EventSystem.Bus;
using UnityEngine;

namespace Common.DragSystem {
  /// <summary>
  /// Allow drag only if game object was first hit by raycast.
  /// </summary>
  public class FirstHitTargetDrag : MonoBehaviour, DragComponent {
    [SerializeField]
    private GameObject target;
    
    private bool dragAllowed;
    public int targetInstanceId => target.GetInstanceID();

    private void Awake() => EventBus<RaycastBeforeDragBeginEvent>.Register(new EventBinding<RaycastBeforeDragBeginEvent>(OnRaycastBeforeDrag));

    public bool IsDragAllowed() => dragAllowed;

    private void OnRaycastBeforeDrag(RaycastBeforeDragBeginEvent ctx) => dragAllowed = ctx.hitObjects?.Length > 0 && ctx.hitObjects[0] == targetInstanceId;
  }
}