using Common.DragSystem.Component.Rotation;
using Common.EventBus;
using RCubeTTT.Scriptable;
using UnityEngine;

namespace RCubeTTT.Component {
  public class RCubeSideDragRotation : DragRotation {
    private readonly Transform cubeSideTransform;
    private readonly float speed;
    private PlayerGameData activePlayer;
    private Vector2 rotationDirection;
    private Quaternion initRotation;

    public RCubeSideDragRotation(Transform cubeSideTransform, int targetInstanceId, float speed) : base(targetInstanceId) {
      this.cubeSideTransform = cubeSideTransform;
      this.speed = speed;
      
      EventBus<PlayerTurnEvent>.Register(
        new EventBinding<PlayerTurnEvent>(
          ctx => {
            if (ctx.player.isMyTurn) activePlayer = ctx.player;
          }
        )
      );

      EventBus<DragBeginEvent>.Register(
        new EventBinding<DragBeginEvent>(
          ctx => {
            if (ctx.instanceId == targetInstanceId) initRotation = cubeSideTransform.localRotation.normalized;
          }
        )
      );

      EventBus<ObjectCropRotationEndEvent>.Register(
        new EventBinding<ObjectCropRotationEndEvent>(
          ctx => {
            if (ctx.instanceId != targetInstanceId) return;

            rotationDirection = Vector2.zero;
            var angle = Quaternion.Angle(initRotation, cubeSideTransform.localRotation.normalized);

            if (angle is > 0 and < 180) {
              initRotation = cubeSideTransform.localRotation.normalized;
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
      cubeSideTransform.Rotate(cubeSideTransform.up, rotationAngle, Space.World);
    }
  }
}