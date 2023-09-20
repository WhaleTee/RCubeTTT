using System;
using System.Linq;
using UnityEngine;

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

  private readonly RCubeFacePiecesFacesRaycastHitEventInvoker rCubeFacePiecesFacesRaycastHitEventInvoker =
  new RCubeFacePiecesFacesRaycastHitEventInvokerImpl();

  private LayerMask pieceFaceLayerMask;

  private MarkType[] scannedMarks;
  private Quaternion initialLocalRotation;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddRCubeFaceRotationStartListener(OnRCubeFaceRotationStart);
    EventManager.AddRCubeFaceRotationEndListener(OnRCubeFaceRotationEnd);
    EventManager.AddRCubePieceFaceMarkSetListener(OnRCubePieceFaceSignSet);
    
    EventManager.AddRCubeFacePiecesFacesRaycastHitInvoker(rCubeFacePiecesFacesRaycastHitEventInvoker);

    pieceFaceLayerMask = LayerMask.GetMask("CubePieceFace");

    scannedMarks = new MarkType[rays.Length];
  }

  private void Start() {
    ScanForFaces();
  }

  private void OnDrawGizmosSelected() {
    if (debug) {
      for (var i = 0; i < rays.Length; i++) {
        var rayTransform = rays[i].transform;
        Debug.DrawRay(rayTransform.position, rayTransform.forward, Color.red);
      }
    }
  }

  #endregion

  private void OnRCubeFaceRotationStart(RCubeFaceRotationStartEventContext context) {
    initialLocalRotation = context.localRotation;
  }

  private void OnRCubeFaceRotationEnd(RCubeFaceRotationEndEventContext context) {
    if (Quaternion.Angle(initialLocalRotation, context.localRotation) > 0) {
      ScanForFaces();
    }
  }

  private void OnRCubePieceFaceSignSet(RCubePieceFaceMarkSetEventContext context) {
    ScanForFaces();
  }

  private void ScanForFaces() {
    scannedMarks = ScanForLayer(rays.Length, pieceFaceLayerMask)
                   .Select(
                     go => {
                       var markController = go.GetComponentInChildren<RCubePieceFaceMarkController>();
                       return markController != null ? markController.markType : MarkType.None;
                     }
                   )
                   .ToArray();

    rCubeFacePiecesFacesRaycastHitEventInvoker.Invoke(new RCubeFacePiecesFacesRaycastHitEventContext(scannedMarks, facePositionType));
  }

  protected override void CastNonAlloc(RaycastHit[] hits, LayerMask layer) {
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