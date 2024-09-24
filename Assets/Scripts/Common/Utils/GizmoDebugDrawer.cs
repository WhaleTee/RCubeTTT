using UnityEngine;

namespace Common.Utils
{
  /// <summary>
  /// Provides extension methods for debugging purposes.
  /// </summary>
  public static class GizmoDebugDrawer {
    /// <summary>
    /// Draws a box cast on hit using the specified parameters.
    /// </summary>
    /// <param name="origin">The origin of the box cast.</param>
    /// <param name="halfExtents">The half extents of the box.</param>
    /// <param name="orientation">The orientation of the box.</param>
    /// <param name="direction">The direction of the box cast.</param>
    /// <param name="hitInfoDistance">The distance of the hit info.</param>
    /// <param name="color">The color of the box cast.</param>
    public static void DrawBoxCastOnHit(
      Vector3 origin, Vector3 halfExtents, Quaternion orientation, Vector3 direction,
      float hitInfoDistance, Color color
    ) {
      origin = CastCenterOnCollision(origin, direction, hitInfoDistance);
      DrawBox(origin, halfExtents, orientation, color);
    }

    /// <summary>
    /// Draws a box cast box using the specified parameters.
    /// </summary>
    /// <param name="origin">The origin of the box cast.</param>
    /// <param name="halfExtents">The half extents of the box.</param>
    /// <param name="orientation">The orientation of the box.</param>
    /// <param name="direction">The direction of the box cast.</param>
    /// <param name="distance">The distance of the box cast.</param>
    /// <param name="color">The color of the box cast.</param>
    public static void DrawBoxCastBox(
      Vector3 origin, Vector3 halfExtents, Quaternion orientation, Vector3 direction,
      float distance, Color color
    ) {
      direction.Normalize();
      var bottomBox = new Box(origin, halfExtents, orientation);
      var topBox = new Box(origin + direction * distance, halfExtents, orientation);
      Debug.DrawLine(bottomBox.backBottomLeft, topBox.backBottomLeft, color);
      Debug.DrawLine(bottomBox.backBottomRight, topBox.backBottomRight, color);
      Debug.DrawLine(bottomBox.backTopLeft, topBox.backTopLeft, color);
      Debug.DrawLine(bottomBox.backTopRight, topBox.backTopRight, color);
      Debug.DrawLine(bottomBox.frontTopLeft, topBox.frontTopLeft, color);
      Debug.DrawLine(bottomBox.frontTopRight, topBox.frontTopRight, color);
      Debug.DrawLine(bottomBox.frontBottomLeft, topBox.frontBottomLeft, color);
      Debug.DrawLine(bottomBox.frontBottomRight, topBox.frontBottomRight, color);
      DrawBox(bottomBox, color);
      DrawBox(topBox, color);
    }

    /// <summary>
    /// Draws a box using the specified parameters.
    /// </summary>
    /// <param name="origin">The origin of the box.</param>
    /// <param name="halfExtents">The half extents of the box.</param>
    /// <param name="orientation">The orientation of the box.</param>
    /// <param name="color">The color of the box.</param>
    public static void DrawBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Color color) =>
    DrawBox(new Box(origin, halfExtents, orientation), color);

    /// <summary>
    /// Draws a box using the specified box object and color.
    /// </summary>
    /// <param name="box">The box object to be drawn.</param>
    /// <param name="color">The color of the box.</param>
    public static void DrawBox(Box box, Color color) {
      Debug.DrawLine(box.frontTopLeft, box.frontTopRight, color);
      Debug.DrawLine(box.frontTopRight, box.frontBottomRight, color);
      Debug.DrawLine(box.frontBottomRight, box.frontBottomLeft, color);
      Debug.DrawLine(box.frontBottomLeft, box.frontTopLeft, color);
      Debug.DrawLine(box.backTopLeft, box.backTopRight, color);
      Debug.DrawLine(box.backTopRight, box.backBottomRight, color);
      Debug.DrawLine(box.backBottomRight, box.backBottomLeft, color);
      Debug.DrawLine(box.backBottomLeft, box.backTopLeft, color);
      Debug.DrawLine(box.frontTopLeft, box.backTopLeft, color);
      Debug.DrawLine(box.frontTopRight, box.backTopRight, color);
      Debug.DrawLine(box.frontBottomRight, box.backBottomRight, color);
      Debug.DrawLine(box.frontBottomLeft, box.backBottomLeft, color);
    }

    /// <summary>
    /// Represents a box in 3D space.
    /// </summary>
    public struct Box {
      public Vector3 localFrontTopLeft { get; private set; }
      public Vector3 localFrontTopRight { get; private set; }
      public Vector3 localFrontBottomLeft { get; private set; }
      public Vector3 localFrontBottomRight { get; private set; }
      public Vector3 localBackTopLeft => -localFrontBottomRight;
      public Vector3 localBackTopRight => -localFrontBottomLeft;
      public Vector3 localBackBottomLeft => -localFrontTopRight;
      public Vector3 localBackBottomRight => -localFrontTopLeft;
      public Vector3 frontTopLeft => localFrontTopLeft + origin;
      public Vector3 frontTopRight => localFrontTopRight + origin;
      public Vector3 frontBottomLeft => localFrontBottomLeft + origin;
      public Vector3 frontBottomRight => localFrontBottomRight + origin;
      public Vector3 backTopLeft => localBackTopLeft + origin;
      public Vector3 backTopRight => localBackTopRight + origin;
      public Vector3 backBottomLeft => localBackBottomLeft + origin;
      public Vector3 backBottomRight => localBackBottomRight + origin;
      public Vector3 origin { get; private set; }

      /// <summary>
      /// Constructs a new Box object with the specified origin, half extents, and orientation.
      /// </summary>
      /// <param name="origin">The origin of the box.</param>
      /// <param name="halfExtents">The half extents of the box.</param>
      /// <param name="orientation">The orientation of the box.</param>
      public Box(Vector3 origin, Vector3 halfExtents, Quaternion orientation) : this(origin, halfExtents) {
        Rotate(orientation);
      }

      /// <summary>
      /// Constructs a new Box object with the specified origin and half extents.
      /// </summary>
      /// <param name="origin">The origin of the box.</param>
      /// <param name="halfExtents">The half extents of the box.</param>
      public Box(Vector3 origin, Vector3 halfExtents) {
        localFrontTopLeft = new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
        localFrontTopRight = new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
        localFrontBottomLeft = new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);
        localFrontBottomRight = new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);
        this.origin = origin;
      }

      /// <summary>
      /// Rotates the box using the specified orientation.
      /// </summary>
      /// <param name="orientation">The orientation to rotate the box.</param>
      public void Rotate(Quaternion orientation) {
        localFrontTopLeft = RotatePointAroundPivot(localFrontTopLeft, Vector3.zero, orientation);
        localFrontTopRight = RotatePointAroundPivot(localFrontTopRight, Vector3.zero, orientation);
        localFrontBottomLeft = RotatePointAroundPivot(localFrontBottomLeft, Vector3.zero, orientation);
        localFrontBottomRight = RotatePointAroundPivot(localFrontBottomRight, Vector3.zero, orientation);
      }
    }

    /// <summary>
    /// Casts the center of the box on collision using the specified parameters.
    /// </summary>
    /// <param name="origin">The origin of the box cast.</param>
    /// <param name="direction">The direction of the box cast.</param>
    /// <param name="hitInfoDistance">The distance of the hit info.</param>
    /// <returns>The cast center after collision.</returns>
    private static Vector3 CastCenterOnCollision(Vector3 origin, Vector3 direction, float hitInfoDistance) {
      return origin + direction.normalized * hitInfoDistance;
    }

    /// <summary>
    /// Rotates a point around a pivot using the specified rotation.
    /// </summary>
    /// <param name="point">The point to be rotated.</param>
    /// <param name="pivot">The pivot point to rotate around.</param>
    /// <param name="rotation">The rotation to apply to the point.</param>
    /// <returns>The rotated point.</returns>
    private static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation) {
      var direction = point - pivot;
      return pivot + rotation * direction;
    }
  
    public static void DrawRayCast(Vector3 origin, Vector3 direction, Color color) {
      Debug.DrawRay(origin, direction, color);
    }
  }
}