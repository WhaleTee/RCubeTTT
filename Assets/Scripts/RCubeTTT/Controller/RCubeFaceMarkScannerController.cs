using System.Linq;
using RCubeTTT.Commons;
using RCubeTTT.EventSystem;
using RCubeTTT.EventSystem.EventContext;
using RCubeTTT.EventSystem.EventInvoker;
using RCubeTTT.EventSystem.EventInvoker.Impl;
using RCubeTTT.Model;
using UnityEngine;

namespace RCubeTTT.Controller {
  public class RCubeFaceSignsScannerController : CastScanner {
    #region serializable fields

    [SerializeField]
    private GameObject[] rays;

    [SerializeField]
    private RCubeFacePositionType facePositionType;

    [SerializeField]
    [Tooltip("Whether to draw the ray casts gizmo or not.")]
    private bool debug;

    #endregion

    #region fields

    private readonly RCubeFacePiecesFacesRayCastHitEventInvoker rCubeFacePiecesFacesRayCastHitEventInvoker =
    new RCubeFacePiecesFacesRayCastHitEventInvokerImpl();

    private LayerMask pieceFaceLayerMask;

    private MarkType[] scannedMarks;
    private Quaternion initialLocalRotation;

    #endregion

    #region unity methods

    private void Awake() {
      RCubeFaceEventManager.AddRotationStartListener(OnRCubeFaceRotationStart);
      RCubeFaceEventManager.AddRotationEndListener(OnRCubeFaceRotationEnd);
      EventManager.AddRCubePieceFaceMarkSetListener(OnRCubePieceFaceMarkSet);

      EventManager.AddRCubeFacePiecesFacesRaycastHitInvoker(rCubeFacePiecesFacesRayCastHitEventInvoker);

      pieceFaceLayerMask = LayerMask.GetMask("CubePieceFace");

      scannedMarks = new MarkType[rays.Length];
    }

    private void Start() {
      ScanForFaces();
    }

    private void OnDrawGizmosSelected() {
      if (debug) {
        foreach (var ray in rays) {
          var rayTransform = ray.transform;
          Debug.DrawRay(rayTransform.position, rayTransform.forward, Color.red);
        }
      }
    }

    #endregion

    private void OnRCubeFaceRotationStart(RotationEventContext context) {
      initialLocalRotation = context.localRotation;
    }

    private void OnRCubeFaceRotationEnd(RotationEventContext context) {
      if (Quaternion.Angle(initialLocalRotation, context.localRotation) > 0) {
        ScanForFaces();
      }
    }

    private void OnRCubePieceFaceMarkSet(RCubePieceFaceMarkSetEventContext context) {
      ScanForFaces();
    }

    private void ScanForFaces() {
      scannedMarks = ScanForLayer(rays.Length, pieceFaceLayerMask)
                     .Select(
                       go => {
                         var markController = go.GetComponentInChildren<RCubePieceFaceMarkController>();
                         return markController ? markController.markType : MarkType.None;
                       }
                     )
                     .ToArray();

      rCubeFacePiecesFacesRayCastHitEventInvoker.Invoke(new RCubeFacePiecesFacesRaycastHitEventContext(scannedMarks, facePositionType));
    }

    protected override void CastNonAlloc(in RaycastHit[] hits, LayerMask layer) {
      for (var i = 0; i < rays.Length; i++) {
        var rayTransform = rays[i].transform;

        Physics.Raycast(
          rayTransform.position,
          rayTransform.forward,
          out var hit,
          float.PositiveInfinity,
          pieceFaceLayerMask
        );

        hits[i] = hit;
      }
    }
  }
}