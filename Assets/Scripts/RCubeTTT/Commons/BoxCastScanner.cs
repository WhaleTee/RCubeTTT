using Support;
using UnityEngine;

namespace RCubeTTT.Commons
{
  /// <summary>
  /// Scans for game objects using a box cast in Unity's 3D space.
  /// </summary>
  public class BoxCastScanner : CastScanner {
    #region serializable fields

    [SerializeField]
    protected Vector3 size;

    [SerializeField]
    [Tooltip("Whether to draw the box cast gizmo or not.")]
    private bool debug;

    #endregion

    #region properties

    /// <summary>
    /// The position of the box cast.
    /// </summary>
    private Vector3 position => transform.position + center;

    #endregion

    #region unity methods

    #if UNITY_EDITOR

    /// <summary>
    /// Draws the box cast gizmo.
    /// </summary>
    private void OnDrawGizmosSelected() {
      if (debug) {
        DebugExtensions.DrawBoxCastBox(
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

    #endregion

    #region methods

    protected override void CastNonAlloc(in RaycastHit[] hits, LayerMask layer) {
      Physics.BoxCastNonAlloc(
        position,
        size,
        direction,
        hits,
        transform.rotation,
        MAX_DISTANCE,
        layer
      );
    }

    #endregion
  }
}