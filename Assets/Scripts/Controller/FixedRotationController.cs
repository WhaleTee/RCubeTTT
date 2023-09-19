using MyBox;
using UnityEngine;

/// <summary>
/// Provides functionality to calculate the nearest multiple of rotation to <see cref="rotationRoundTo"/>.
/// Its children subclasses should use the <see cref="GetNearestRotation"/> method to get the nearest rotation to <see cref="rotationRoundTo"/> and provide functionality to fixing objects rotation.
/// </summary>
public abstract class FixedRotationController : MonoBehaviour {
  #region fields

  private const float MAX_EULER_ROTATION_DEGREES = 360;
  private const float MAX_ROTATION_SPEED = 1000;

  #endregion

  #region serializable fields

  [SerializeField]
  [Tooltip("Euler angles, which defines the nearest multiple of rotation to.")]
  [RangeVector(new float[] { }, new[] { MAX_EULER_ROTATION_DEGREES, MAX_EULER_ROTATION_DEGREES, MAX_EULER_ROTATION_DEGREES })]
  private Vector3 rotationRoundTo = Vector3.zero;

  [SerializeField]
  [Tooltip("Rotation speed in degrees. Usually, it multiplies by Time.deltaTime.")]
  [Range(0, MAX_ROTATION_SPEED)]
  protected float rotationSpeed;

  #endregion

  #region properties

  /// <summary>
  /// Defines the target rotation.
  /// </summary>
  protected Quaternion targetRotation { get; set; }

  /// <summary>
  /// Defines the rotation context. It should be set in the Awake method of the child class.
  /// </summary>
  protected RotationContext rotationContext { get; set; }

  /// <summary>
  /// Defines the current rotation. It depends on the <see cref="rotationContext"/> value.
  /// </summary>
  protected Quaternion currentRotation => rotationContext == RotationContext.Local ? transform.localRotation : transform.rotation;

  #endregion

  #region methods

  /// <summary>
  /// Calculates the nearest multiple of rotation based on the values of <see cref="rotationRoundTo"/>.
  /// </summary> 
  /// <returns>The nearest rotation as a Quaternion.</returns>
  protected Quaternion GetNearestRotation() {
    var eulerAngles = currentRotation.eulerAngles;

    return Quaternion.Euler(
      rotationRoundTo.x >= 0.1 ? Math.RoundToNearestMultiple(eulerAngles.x, rotationRoundTo.x) : eulerAngles.x,
      rotationRoundTo.y >= 0.1 ? Math.RoundToNearestMultiple(eulerAngles.y, rotationRoundTo.y) : eulerAngles.y,
      rotationRoundTo.z >= 0.1 ? Math.RoundToNearestMultiple(eulerAngles.z, rotationRoundTo.z) : eulerAngles.z
    );
  }

  /// <summary>
  /// Rotates the object to the <see cref="targetRotation"/> by an angular step of <see cref="rotationSpeed"/> * <see cref="Time.deltaTime"/> (but note that the rotation will not overshoot).
  /// </summary>
  protected void Rotate() {
    transform.localRotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
  }

  #endregion
}