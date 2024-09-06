using UnityEngine;

namespace Common {
  public static class Extensions {
    #region float

    public static float RoundToNearestMultiple(this float value, float roundTo) => Mathf.Round(value / roundTo) * roundTo;

    #endregion
  }
}