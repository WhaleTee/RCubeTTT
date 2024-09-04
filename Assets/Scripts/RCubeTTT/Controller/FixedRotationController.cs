using RCubeTTT.Model;
using Support;
using UnityEngine;

namespace RCubeTTT.Controller
{
  /// <summary>
  /// Provides functionality to calculate the nearest multiple of rotation to <see cref="rotationRoundTo"/>.
  /// Its children subclasses should use the <see cref="GetNearestRotation"/> method to get the nearest rotation to <see cref="rotationRoundTo"/> and provide functionality to fixing objects rotation.
  /// </summary>
  public abstract class FixedRotationController : MonoBehaviour {
    #region fields
    private const float MAX_ROTATION_SPEED = 10;

    [SerializeField]
    [Tooltip("Euler angles, which defines the nearest multiple of rotation to.")]
    private Vector3 rotationRoundTo = Vector3.zero;

    [SerializeField]
    [Tooltip("Duration of rotation.")]
    [Range(0, MAX_ROTATION_SPEED)]
    protected float duration;
    
    protected Quaternion targetRotation;
    protected Space rotationSpace;
    protected float rotationElapsedTime;
    
    #endregion

    #region properties

    protected Quaternion currentRotation => rotationSpace == Space.Self ? transform.localRotation : transform.rotation;

    #endregion

    #region methods

    /// <summary>
    /// Calculates the nearest multiple of rotation based on the values of <see cref="rotationRoundTo"/>.
    /// </summary> 
    /// <returns>The nearest rotation as a Quaternion.</returns>
    protected Quaternion GetNearestRotation() {
      var eulerAngles = currentRotation.eulerAngles;

      return Quaternion.Euler(
        rotationRoundTo.x >= 0.1 ? RoundToNearestMultiple(eulerAngles.x, rotationRoundTo.x) : eulerAngles.x,
        rotationRoundTo.y >= 0.1 ? RoundToNearestMultiple(eulerAngles.y, rotationRoundTo.y) : eulerAngles.y,
        rotationRoundTo.z >= 0.1 ? RoundToNearestMultiple(eulerAngles.z, rotationRoundTo.z) : eulerAngles.z
      );
    }

    /// <summary>
    /// Rotates the object to the <see cref="targetRotation"/> by an angular step of <see cref="duration"/> * <see cref="Time.deltaTime"/> (but note that the rotation will not overshoot).
    /// </summary>
    protected void Rotate() {
      transform.localRotation = Quaternion.Slerp(currentRotation, targetRotation, rotationElapsedTime / duration);
    }
    
    public static float RoundToNearestMultiple(float value, float roundTo) {
          return Mathf.Round(value / roundTo) * roundTo;
    }

    #endregion
  }
}