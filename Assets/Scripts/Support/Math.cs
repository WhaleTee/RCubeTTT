using UnityEngine;

/// <summary>
/// Provides mathematical functions and operations.
/// </summary>
public static class Math {
  /// <summary>
  /// Rounds <paramref name="value"/> to the nearest multiple of <paramref name="roundTo"/>
  /// </summary>
  /// <param name="value">The value to be rounded.</param>
  /// <param name="roundTo">The value that specifies the multiple to round to.</param>
  /// <returns>The value rounded to the nearest multiple of of the specified <paramref name="roundTo"/></returns>
  public static float RoundToNearestMultiple(float value, float roundTo) {
    return Mathf.Round(value / roundTo) * roundTo;
  }
}