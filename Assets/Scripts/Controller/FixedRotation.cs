using MyBox;
using UnityEngine;

/// <summary>
/// This class provides the function to calculate the nearest multiple of rotation to <see cref="rotationRoundTo"/>.
/// Its children subclasses should use the <see cref="GetNearestRotation"/> method to get the nearest rotation to <see cref="rotationRoundTo"/> and provide functionality to fixing objects rotation.
/// </summary>
public class FixedRotation : MonoBehaviour {
  #region fields

  private const float MAX_EULER_ROTATION_DEGREES = 360;

  #endregion

  #region serializable fields

  [SerializeField]
  [Tooltip("Enables fixing the cube rotation to specific angles after rotating it.")]
  private bool enableFixedRotation;

  [SerializeField]
  [ConditionalField(nameof(enableFixedRotation), false, true)]
  [RangeVector(new float[] { 1, 1, 1 })]
  private Vector2Int accessRotation = Vector2Int.zero;

  [SerializeField]
  [Tooltip("Euler angles, which defines the nearest multiple of rotation to.")]
  [ConditionalField(nameof(enableFixedRotation), false, true)]
  [RangeVector(new[] { MAX_EULER_ROTATION_DEGREES, MAX_EULER_ROTATION_DEGREES, MAX_EULER_ROTATION_DEGREES })]
  private Vector3 rotationRoundTo = Vector3.zero;

  [SerializeField]
  [Range(1, 3)]
  protected float rotationDuration = 1;

  #endregion

  #region properties

  private Quaternion globalRotation => transform.rotation;

  #endregion

  #region methods

  /// <summary>
  /// This method calculates the nearest multiple of rotation based on the values of <see cref="rotationRoundTo"/>.
  /// </summary> 
  /// <returns>The nearest rotation as a Quaternion.</returns>
  protected Quaternion GetNearestRotation() {
    var eulerAngles = globalRotation.eulerAngles;

    return Quaternion.Euler(
      accessRotation.x > 0 && rotationRoundTo.x >= 1 ? Math.RoundToNearestMultiple(eulerAngles.x, rotationRoundTo.x) : eulerAngles.x,
      accessRotation.y > 0 && rotationRoundTo.y >= 1 ? Math.RoundToNearestMultiple(eulerAngles.y, rotationRoundTo.y) : eulerAngles.y,
      accessRotation is { x: > 0, y: > 0 } && rotationRoundTo.z >= 1 ? Math.RoundToNearestMultiple(eulerAngles.z, rotationRoundTo.z) : eulerAngles.z
    );
  }

  #endregion
}