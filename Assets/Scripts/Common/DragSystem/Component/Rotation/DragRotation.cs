using Common.EventBus;
using UnityEngine;

namespace Common.DragSystem.Component.Rotation {
  public abstract class DragRotation : TargetInstanceIdProvider {
    protected readonly int targetInstanceId;
    protected Vector2 pointerDelta { get; private set; }

    protected DragRotation(int targetInstanceId) {
      this.targetInstanceId = targetInstanceId;

      EventBus<PointerPositionDeltaEvent>.Register(new EventBinding<PointerPositionDeltaEvent>(ctx => pointerDelta = ctx.delta));
      EventBus<DragEvent>.Register(new EventBinding<DragEvent>(ctx => { if (ctx.instanceId == GetTargetInstanceId()) Rotate(); }));
    }

    public int GetTargetInstanceId() => targetInstanceId;

    protected abstract void Rotate();
  }
}