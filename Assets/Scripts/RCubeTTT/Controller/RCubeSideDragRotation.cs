using Common.DragSystem;
using Common.DragSystem.Rotation;
using Common.EventSystem.Bus;
using UnityEngine;

namespace RCubeTTT.Controller {
  public class RCubeSideDragRotation : DragRotation, DragComponent {
    private Vector3 sideHitPoint;
    private Vector3 sideHitNormal;
    private Vector2 pointerHitPosition;

    protected override void Awake() {
      base.Awake();

      EventBus<DragBeginEvent>.Register(
        new EventBinding<DragBeginEvent>(
          ctx => {
            if (ctx.instanceId == instanceId) {
              sideHitPoint = ctx.hitPoint;
              sideHitNormal = ctx.hitNormal;
              pointerHitPosition = ctx.pointerScreenPosition;
            }
          }
        )
      );
    }

    protected override void Rotate() {
      targetTransform.Rotate(relativeTransform.up, GetAngle() * speed * Time.deltaTime, Space.World);
    }

    private float GetAngle() {
      float hitPointY = Vector2Int.RoundToInt(sideHitPoint).y;
      float sidePositionY = Vector3Int.RoundToInt(targetTransform.position).y;
      
      return Vector3Int.RoundToInt(relativeTransform.up) switch {
               var v when v == Vector3Int.up
                          || (v == Vector3Int.forward && hitPointY > sidePositionY) => Vector3.Dot(pointerDelta, Vector3.left),
               var v when v == Vector3Int.down
                          || (v == Vector3Int.back && hitPointY > sidePositionY) => Vector3.Dot(pointerDelta, Vector3.right),
               var v when v == Vector3Int.left
                          || (v == Vector3Int.forward && hitPointY <= sidePositionY) => Vector3.Dot(pointerDelta, Vector3.down),
               var v when v == Vector3Int.right
                          || (v == Vector3Int.back && hitPointY <= sidePositionY) => Vector3.Dot(pointerDelta, Vector3.up),
               var _ => 0
             };
    }

    /// <summary>
    /// Calculates the direction on the screen from a given screen point to a world point shifted by a world direction.
    /// </summary>
    /// <param name="screenPoint">The initial point on the screen.</param>
    /// <param name="worldPoint">The point in the world space.</param>
    /// <param name="worldDirection">The direction in the world space to shift the world point.</param>
    /// <returns>A normalized vector representing the direction on the screen.</returns>
    private Vector2 ScreenDirection(Vector2 screenPoint, Vector3 worldPoint, Vector3 worldDirection) {
      Vector2 shifted = mainCamera.WorldToScreenPoint(worldPoint + worldDirection);

      return (shifted - screenPoint).normalized;
    }

    public bool IsDragAllowed() {
      var cameraRhs = mainCamera.transform.forward.normalized;
      var rotationAxis = relativeTransform.right.normalized;
      var altRotationAxis = relativeTransform.forward.normalized;
      var axisSign = Mathf.Sign(cameraRhs.x * cameraRhs.y * cameraRhs.z);
      var signFlip = axisSign * Mathf.Sign(Vector3.Dot(rotationAxis, cameraRhs)) * Mathf.Sign(Vector3.Dot(altRotationAxis, cameraRhs));
      var rotationDirection = signFlip * ScreenDirection(pointerHitPosition, sideHitPoint, altRotationAxis);
      var altRotationDirection = -signFlip * ScreenDirection(pointerHitPosition, sideHitPoint, rotationAxis);

      var angle = Vector2.Dot(pointerDelta, rotationDirection);
      var altAngle = Vector2.Dot(pointerDelta, altRotationDirection);
      Debug.Log($"angle: {angle}, altAngle: {altAngle}");
      return angle > 5;
    }
  }
}