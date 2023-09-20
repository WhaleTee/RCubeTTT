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
  /// <returns>An <see cref="IEnumerator"/> used for coroutine execution.</returns>
  private IEnumerator RotateRCube() {
    while (Quaternion.Angle(currentRotation, targetRotation) > 0.5) {
      Rotate();
      rotationElapsedTime += Time.deltaTime;
      yield return null;
    }
    transform.localRotation = targetRotation;
    rotationElapsedTime = 0;
  }

  /// <summary>
  /// Handles the start of dragging for the Rubik's Cube.
  /// It sets the target rotation to the current rotation and stops the rotation coroutine.
  /// </summary>
  private void OnCubeDragStart() {
    targetRotation = currentRotation;
    StopCoroutine(RotateRCube());
  }

  /// <summary>
  /// Handles the end of dragging for the Rubik's Cube.
  /// It sets the target rotation to the nearest rotation and starts the rotation coroutine to rotate the entire Rubik's Cube. 
  /// </summary>
  private void OnRCubeDragEnd() {
    targetRotation = GetNearestRotation();
    StartCoroutine(RotateRCube());
  }

  #endregion
}