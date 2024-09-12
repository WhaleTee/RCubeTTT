using UnityEngine;

namespace Common.Extensions {
  public static class Vector3Extensions {
    /// <summary>
    /// Returns a new Vector3 with each component being the absolute value of the corresponding component of the input vector.
    /// </summary>
    /// <param name="vector">The input Vector3 whose components' absolute values are to be computed.</param>
    /// <returns>A new Vector3 with the absolute values of the input vector's components.</returns>
    public static Vector3 Abs(this Vector3 vector) {
      return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }
  }
}