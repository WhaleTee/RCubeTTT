using System.Collections;
using RCubeTTT.Commons;
using RCubeTTT.EventSystem;
using RCubeTTT.EventSystem.Event.RCubeEvent;
using RCubeTTT.EventSystem.EventContext;
using RCubeTTT.EventSystem.EventInvoker;
using RCubeTTT.EventSystem.EventInvoker.Impl;
using RCubeTTT.Model;
using UnityEngine;
using UnityEngine.Serialization;

namespace RCubeTTT.Controller
{
  /// <summary>
  /// Controls the fixed rotation behavior of the Rubik's Cube face.
  /// </summary>
  public class RCubeFaceFixedRotationController : FixedRotationController {
    #region fields

    private readonly IRotationEventInvoker rotationEventInvoker = new RotationEventInvoker();
    private readonly IRotationEventInvoker rotationEndEventInvoker = new RotationEventInvoker();

    private RCubeFaceDragRotationController faceDragRotationController;
    private Transform currentTransform => transform;

    #endregion

    #region unity methods

    private void Awake() {
      RCubeFaceEventManager.AddRotationPerformedInvoker(rotationEventInvoker);
      RCubeFaceEventManager.AddRotationEndInvoker(rotationEndEventInvoker);

      RCubeFaceEventManager.AddDragStartListener(OnDragStart);
      RCubeFaceEventManager.AddDragEndListener(OnDragEnd);
      
      faceDragRotationController = GetComponent<RCubeFaceDragRotationController>();

      rotationSpace = Space.Self;
      targetRotation = currentRotation;
    }

    #endregion

    #region methods

    private IEnumerator RotateRCubeFace() {
      while (Quaternion.Angle(currentRotation, targetRotation) > 0.5) {
        Rotate();
        rotationElapsedTime += Time.deltaTime;
        rotationEventInvoker.Invoke(new RotationEventContext(GetInstanceID(), currentTransform.rotation, currentTransform.localRotation));
        yield return null;
      }

      transform.localRotation = targetRotation;
      rotationElapsedTime = 0;
      rotationEndEventInvoker.Invoke(new RotationEventContext(GetInstanceID(), currentTransform.rotation, currentTransform.localRotation));
    }

    private void OnDragStart(InputHitObjectEventContext context) {
      if (context.instanceId == faceDragRotationController.GetInstanceID()) {
        targetRotation = currentRotation;
        StopCoroutine(RotateRCubeFace());
      }
    }

    private void OnDragEnd(ObjectInstanceContext context) {
      if (context.instanceId == faceDragRotationController.GetInstanceID()) {
        targetRotation = GetNearestRotation();
        StartCoroutine(RotateRCubeFace());
      }
    }

    #endregion
  }
}