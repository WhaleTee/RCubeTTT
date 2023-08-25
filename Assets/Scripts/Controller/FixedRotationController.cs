using UnityEngine;
using MyBox;

/// <summary>
/// This class provides the function to calculate the nearest multiple of rotation to <see cref="rotationRoundTo"/>.
/// Its children subclasses should use the <see cref="GetNearestRotation"/> method to get the nearest rotation to <see cref="rotationRoundTo"/> and provide functionality to fixing objects rotation.
/// </summary>
public abstract class FixedRotationController : MonoBehaviour {
  #region fields

  private const float MAX_EULER_ROTATION_DEGREES = 360;

  #endregion

  #region serializable fields

  [SerializeField]
  [RangeVector(new float[] {}, new float[] { 1, 1, 1 })]
  private Vector3Int fixedRotationBy = Vector3Int.zero;

  [SerializeField]
  [Tooltip("Euler angles, which defines the nearest multiple of rotation to.")]
  [RangeVector(new float[] {}, new[] { MAX_EULER_ROTATION_DEGREES, MAX_EULER_ROTATION_DEGREES, MAX_EULER_ROTATION_DEGREES })]
  private Vector3 rotationRoundTo = Vector3.zero;

  [SerializeField]
  [Range(1, 3)]
  protected float rotateDuration = 1;

  #endregion

  #region methods

  /// <summary>
  /// This method calculates the nearest multiple of rotation based on the values of <see cref="rotationRoundTo"/>.
  /// </summary> 
  /// <returns>The nearest rotation as a Quaternion.</returns>
  protected Quaternion GetNearestRotation() {
    var eulerAngles = CurrentRotation().eulerAngles;

    return Quaternion.Euler(
      fixedRotationBy.x > 0 && rotationRoundTo.x >= 1 ? Math.RoundToNearestMultiple(eulerAngles.x, rotationRoundTo.x) : eulerAngles.x,
      fixedRotationBy.y > 0 && rotationRoundTo.y >= 1 ? Math.RoundToNearestMultiple(eulerAngles.y, rotationRoundTo.y) : eulerAngles.y,
      fixedRotationBy.z > 0 && rotationRoundTo.z >= 1 ? Math.RoundToNearestMultiple(eulerAngles.z, rotationRoundTo.z) : eulerAngles.z
    );
  }

  protected abstract Quaternion CurrentRotation();

  #endregion
}