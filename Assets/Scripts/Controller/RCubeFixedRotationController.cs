using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the fixed rotation behavior of the Rubik's Cube.
/// </summary>
public class RCubeFixedRotationController : FixedRotationController {
  #region unity methods

  private void Awake() {
    EventManager.AddRCubeDragStartListener(OnCubeDragStart);
    EventManager.AddRCubeDragEndListener(OnRCubeDragEnd);

    rotationContext = RotationContext.Local;
    targetRotation = currentRotation;
  }

  #endregion

  #region methods

  /// <summary>
  /// Rotates the Rubik's Cube to the nearest rotation.
  /// </summary>
  /// <returns>An IEnumerator used for coroutine execution.</returns>
  private IEnumerator RotateRCube() {
    while (Quaternion.Angle(currentRotation, targetRotation) > 0) {
      Rotate();
      yield return null;
    }
  }

  /// <summary>
  /// Called when the Rubik's Cube is dragged.
  /// </summary>
  private void OnCubeDragStart() {
    targetRotation = currentRotation;
    StopCoroutine(RotateRCube());
  }

  /// <summary>
  /// Called when the Rubik's Cube is released.
  /// </summary>
  private void OnRCubeDragEnd() {
    targetRotation = GetNearestRotation();
    StartCoroutine(RotateRCube());
  }

  #endregion
}