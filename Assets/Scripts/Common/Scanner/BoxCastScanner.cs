using Common.Utils;
using UnityEngine;

namespace Common.Scanner
{
  /// <summary>
  /// Scans for game objects using a box cast in Unity's 3D space.
  /// </summary>
  public class BoxCastScanner : CastScanner {
    [SerializeField] protected Vector3 size;

    [SerializeField]
    [Tooltip("Whether to draw the box cast gizmo or not.")]
    private bool debug;

    /// <summary>
    /// The position of the box cast.
    /// </summary>
    private Vector3 position => transform.position + center;

    #if UNITY_EDITOR

    /// <summary>
    /// Draws the box cast gizmo.
    /// </summary>
    private void OnDrawGizmosSelected() {
      if (debug) {
        GizmoDebugDrawer.DrawBoxCastBox(
          position,
          size,
          transform.rotation,
          direction,
          MAX_DISTANCE,
          Color.red
        );
      }
    }

    #endif

    protected override void CastNonAlloc(in RaycastHit[] hits, LayerMask layer) {
      Physics.BoxCastNonAlloc(position, size, direction, hits, transform.rotation, MAX_DISTANCE, layer);
    }
  }
}