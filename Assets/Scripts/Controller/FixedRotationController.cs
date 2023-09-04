using MyBox;
using UnityEngine;

/// <summary>
/// This class provides the function to calculate the nearest multiple of rotation to <see cref="rotationRoundTo"/>.
/// Its children subclasses should use the <see cref="GetNearestRotation"/> method to get the nearest rotation to <see cref="rotationRoundTo"/> and provide functionality to fixing objects rotation.
/// </summary>
public abstract class FixedRotationController : MonoBehaviour {
  private const float MAX_EULER_ROTATION_DEGREES = 360;

  #region serializable fields

  [SerializeField]
  [Tooltip("Euler angles, which defines the nearest multiple of rotation to.")]
  [RangeVector(new float[] {}, new[] { MAX_EULER_ROTATION_DEGREES, MAX_EULER_ROTATION_DEGREES, MAX_EULER_ROTATION_DEGREES })]
  private Vector3 rotationRoundTo = Vector3.zero;

  [SerializeField]
  [Range(0, float.MaxValue)]
  protected float speed;

  #endregion

  #region methods

  /// <summary>
  /// This method calculates the nearest multiple of rotation based on the values of <see cref="rotationRoundTo"/>.
  /// </summary> 
  /// <returns>The nearest rotation as a Quaternion.</returns>
  protected Quaternion GetNearestRotation() {
    var eulerAngles = GetCurrentRotation().eulerAngles;

    return Quaternion.Euler(
      rotationRoundTo.x >= 0.1 ? Math.RoundToNearestMultiple(eulerAngles.x, rotationRoundTo.x) : eulerAngles.x,
      rotationRoundTo.y >= 0.1 ? Math.RoundToNearestMultiple(eulerAngles.y, rotationRoundTo.y) : eulerAngles.y,
      rotationRoundTo.z >= 0.1 ? Math.RoundToNearestMultiple(eulerAngles.z, rotationRoundTo.z) : eulerAngles.z
    );
  }

  protected abstract Quaternion GetCurrentRotation();

  #endregion
}