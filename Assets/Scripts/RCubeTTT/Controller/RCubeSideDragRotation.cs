using Common.DragSystem.Rotation;
using Common.EventSystem.Bus;
using Common.Extensions;
using RCubeTTT.Model;
using UnityEngine;

namespace RCubeTTT.Controller {
  public class RCubeSideDragRotation : DragRotation {
    private PlayerPlayData activePlayer;
    private Vector2 rotationDirection;
    private Vector2 pointerPosition;
    private Quaternion initRotation;

    protected override void Awake() {
      base.Awake();

      EventBus<PlayerTurnEvent>.Register(
        new EventBinding<PlayerTurnEvent>(
          ctx => {
            if (ctx.player.isMyTurn) activePlayer = ctx.player;
          }
        )
      );

      EventBus<PointerPositionEvent>.Register(new EventBinding<PointerPositionEvent>(ctx => pointerPosition = ctx.screenPosition));

      EventBus<DragBeginEvent>.Register(
        new EventBinding<DragBeginEvent>(
          ctx => {
            if (ctx.instanceId == targetInstanceId) initRotation = targetTransform.localRotation.normalized;
          }
        )
      );

      EventBus<ObjectCropRotationEndEvent>.Register(
        new EventBinding<ObjectCropRotationEndEvent>(
          ctx => {
            if (ctx.instanceId != targetInstanceId) return;

            rotationDirection = Vector2.zero;
            var angle = Quaternion.Angle(initRotation, targetTransform.localRotation.normalized);

            if (angle is > 0 and < 180) {
              initRotation = targetTransform.localRotation.normalized;
              activePlayer.canDragCubeSide = false;
              EventBus<PlayerTurnEvent>.Raise(new PlayerTurnEvent { player = activePlayer });
            }
          }
        )
      );

      EventBus<AxisDragAllowedEvent>.Register(
        new EventBinding<AxisDragAllowedEvent>(
          ctx => {
            if (ctx.instanceId == targetInstanceId) rotationDirection = ctx.directionDragAllowed;
          }
        )
      );
    }

    protected override void Rotate() {
      if (!activePlayer.canDragCubeSide) return;
      var rotationAngle = Vector2.Dot(rotationDirection, pointerDelta) * speed * Time.deltaTime;
      targetTransform.Rotate(targetTransform.up, rotationAngle, Space.World);
    }
  }
}