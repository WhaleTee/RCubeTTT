using UnityEngine;

public static class QuaternionExtension {
  public static Quaternion SmoothDamp(this Quaternion rot, Quaternion target, ref Quaternion deriv, float time) {
    if (Time.deltaTime < Mathf.Epsilon) return rot;
    // account for double-cover
    var dot = Quaternion.Dot(rot, target);
    var multi = dot > 0f ? 1f : -1f;
    target.x *= multi;
    target.y *= multi;
    target.z *= multi;
    target.w *= multi;
    // smooth damp (nlerp approx)
    var result = new Vector4(
      Mathf.SmoothDamp(rot.x, target.x, ref deriv.x, time),
      Mathf.SmoothDamp(rot.y, target.y, ref deriv.y, time),
      Mathf.SmoothDamp(rot.z, target.z, ref deriv.z, time),
      Mathf.SmoothDamp(rot.w, target.w, ref deriv.w, time)
    ).normalized;
		
    // ensure deriv is tangent
    var derivError = Vector4.Project(new Vector4(deriv.x, deriv.y, deriv.z, deriv.w), result);
    deriv.x -= derivError.x;
    deriv.y -= derivError.y;
    deriv.z -= derivError.z;
    deriv.w -= derivError.w;		
		
    return new Quaternion(result.x, result.y, result.z, result.w);
  }
}