using System.Collections;
using Common.EventSystem.Bus;
using UnityEngine;

namespace Common {
  public class IdleRotationToComponent : MonoBehaviour {
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
      EventBus<ObjectDragBeginEvent>.Register(new EventBinding<ObjectDragBeginEvent>(StopIdleRotation));
      EventBus<ObjectDragEndEvent>.Register(new EventBinding<ObjectDragEndEvent>(StartIdleRotation));
    }

    private void StopIdleRotation(ObjectDragBeginEvent ctx) {
      if (ctx.instanceId == gameObject.GetInstanceID()) {
        targetRotation = localRotation;
        StopCoroutine(RotateRCubeFace());

        EventBus<ObjectIdleRotationEndEvent>.Raise(
          new ObjectIdleRotationEndEvent { instanceId = gameObject.GetInstanceID(), currentRotation = localRotation, targetRotation = targetRotation }
        );
      }
    }

    private void StartIdleRotation(ObjectDragEndEvent ctx) {
      if (ctx.instanceId == gameObject.GetInstanceID()) {
        targetRotation = GetNearestRotation();
        StartCoroutine(RotateRCubeFace());

        EventBus<ObjectIdleRotationBeginEvent>.Raise(
          new ObjectIdleRotationBeginEvent {
            instanceId = gameObject.GetInstanceID(), currentRotation = localRotation, targetRotation = targetRotation
          }
        );
      }
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