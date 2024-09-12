using System.Collections;
using Common.EventSystem.Bus;
using Common.Extensions;
using UnityEngine;

namespace Common.SnapRotation {
  public class CropRotation : MonoBehaviour {
    [SerializeField]
    [Tooltip("Euler angles, which defines the nearest multiple of rotation to.")]
    private Vector3 rotationRoundTo = Vector3.zero;

    [SerializeField]
    [Tooltip("Duration of rotation.")]
    [Range(0, 10)]
    private float duration;

    private Quaternion targetRotation;
    private float rotationElapsedTime;

    private Quaternion localRotation => transform.localRotation;

    private void Awake() {
      EventBus<DragBeginEvent>.Register(new EventBinding<DragBeginEvent>(StopIdleRotation));
      EventBus<DragEndEvent>.Register(new EventBinding<DragEndEvent>(StartIdleRotation));
    }

    private void StopIdleRotation(DragBeginEvent ctx) {
      if (ctx.instanceId == gameObject.GetInstanceID()) {
        targetRotation = localRotation;
        StopCoroutine(RotateRCubeFace());

        EventBus<ObjectIdleRotationEndEvent>.Raise(
          new ObjectIdleRotationEndEvent { instanceId = gameObject.GetInstanceID(), currentRotation = localRotation, targetRotation = targetRotation }
        );
      }
    }

    private void StartIdleRotation(DragEndEvent ctx) {
      targetRotation = GetNearestRotation();
      StartCoroutine(RotateRCubeFace());

      EventBus<ObjectIdleRotationBeginEvent>.Raise(
        new ObjectIdleRotationBeginEvent { instanceId = gameObject.GetInstanceID(), currentRotation = localRotation, targetRotation = targetRotation }
      );
    }

    private IEnumerator RotateRCubeFace() {
      while (Quaternion.Angle(localRotation, targetRotation) > 0.5) {
        Rotate();
        rotationElapsedTime += Time.deltaTime;
        yield return null;
      }

      transform.localRotation = targetRotation;
      rotationElapsedTime = 0;
    }

    private void Rotate() {
      transform.localRotation = Quaternion.Slerp(localRotation, targetRotation, rotationElapsedTime / duration);
    }

    private Quaternion GetNearestRotation() {
      var eulerAngles = localRotation.eulerAngles;

      return Quaternion.Euler(
        rotationRoundTo.x >= 0.1 ? eulerAngles.x.RoundToNearestMultiple(rotationRoundTo.x) : eulerAngles.x,
        rotationRoundTo.y >= 0.1 ? eulerAngles.y.RoundToNearestMultiple(rotationRoundTo.y) : eulerAngles.y,
        rotationRoundTo.z >= 0.1 ? eulerAngles.z.RoundToNearestMultiple(rotationRoundTo.z) : eulerAngles.z
      );
    }
  }
}