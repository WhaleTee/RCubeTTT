using UnityEngine;

namespace Common.Extensions {
  public static class Vector2Extensions {
    /// <summary>
    /// Returns a new Vector2 with each component being the absolute value of the corresponding component of the input vector.
    /// </summary>
    /// <param name="vector">The input Vector2 whose components' absolute values are to be computed.</param>
    /// <returns>A new Vector2 with the absolute values of the input vector's components.</returns>
    public static Vector2 Abs(this Vector2 vector) {
      return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
    }
  }
}