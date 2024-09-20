using System;
using System.Collections;
using Common.Extensions;
using UnityEngine;

namespace Common.CropRotation {
  public class CropRotationService {
    public delegate Coroutine StartCoroutine(IEnumerator routine);
    public delegate void StopCoroutine(IEnumerator routine);
    
    private Vector3 rotationRoundTo;
    private float duration;
    private Quaternion targetRotation;
    private float rotationElapsedTime;
    private Quaternion localRotation;

    public void StopCropRotation(Action<IEnumerator> stopCoroutine) {
      targetRotation = localRotation;
      stopCoroutine.Invoke(RotateRCubeFace());
    }

    public void StartCropRotation(Transform target, Vector3 rotationRoundTo, float duration, StartCoroutine startCoroutine) {
      this.rotationRoundTo = rotationRoundTo;
      this.duration = duration;
      localRotation = target.localRotation;
      targetRotation = GetNearestRotation(localRotation.eulerAngles);
      startCoroutine(RotateRCubeFace());
    }

    private IEnumerator RotateRCubeFace() {
      while (Quaternion.Angle(localRotation, targetRotation) > 0.5) {
        Rotate();
        rotationElapsedTime += Time.deltaTime;
        yield return null;
      }

      localRotation = targetRotation;
      rotationElapsedTime = 0;
    }

    private void Rotate() {
      localRotation = Quaternion.Slerp(localRotation, targetRotation, rotationElapsedTime / duration);
    }

    private Quaternion GetNearestRotation(Vector3 localRotation) {
      return Quaternion.Euler(
        rotationRoundTo.x >= 0.1 ? localRotation.x.RoundToNearestMultiple(rotationRoundTo.x) : localRotation.x,
        rotationRoundTo.y >= 0.1 ? localRotation.y.RoundToNearestMultiple(rotationRoundTo.y) : localRotation.y,
        rotationRoundTo.z >= 0.1 ? localRotation.z.RoundToNearestMultiple(rotationRoundTo.z) : localRotation.z
      );
    }
  }
}