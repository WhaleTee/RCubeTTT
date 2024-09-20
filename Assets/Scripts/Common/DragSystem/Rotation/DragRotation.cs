using Common.EventSystem.Bus;
using UnityEngine;

namespace Common.DragSystem.Rotation {
  public abstract class DragRotation : MonoBehaviour, TargetInstanceIdProvider {
    [Header("Drag Rotation Settings")]
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Transform relative;

    [SerializeField]
    protected uint speed;

    public int targetInstanceId => targetTransform.gameObject.GetInstanceID();
    protected Camera mainCamera { get; private set; }
    protected Vector2 pointerDelta { get; private set; }
    protected Transform targetTransform => target ? target : transform;
    protected Transform relativeTransform => relative ? relative : mainCamera.transform;

    protected virtual void Awake() {
      mainCamera = Camera.main;

      EventBus<PointerPositionDeltaEvent>.Register(new EventBinding<PointerPositionDeltaEvent>(ctx => pointerDelta = ctx.delta));
      EventBus<DragEvent>.Register(new EventBinding<DragEvent>(ctx => { if (ctx.instanceId == targetInstanceId) Rotate(); }));
    }

    protected abstract void Rotate();
  }
}