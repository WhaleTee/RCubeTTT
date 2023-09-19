using System.Linq;
using UnityEngine;

public class RCubeFaceSignsScannerController : CastScanner {
  #region serializable fields

  [SerializeField]
  private int raysCount;

  [SerializeField]
  private int rowsCount;

  [SerializeField]
  private Vector2 step;

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

  private MarkType[] scannedSigns;
  private Quaternion initialLocalRotation;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddRCubeFacePiecesFacesRaycastHitInvoker(rCubeFacePiecesFacesRaycastHitEventInvoker);

    EventManager.AddRCubeFaceRotationStartListener(OnRCubeFaceRotationStart);
    EventManager.AddRCubeFaceRotationEndListener(OnRCubeFaceRotationEnd);
    EventManager.AddRCubePieceFaceMarkSetListener(OnRCubePieceFaceSignSet);

    pieceFaceLayerMask = LayerMask.GetMask("CubePieceFace");

    scannedSigns = new MarkType[raysCount * rowsCount];

    ScanForFaces();
  }

  private void OnDrawGizmosSelected() {
    if (debug) {
      for (var i = 0; i < rowsCount; i++) {
        for (var j = 0; j < raysCount; j++) {
          var transformPosition = transform.localPosition;
          var position = new Vector3(transformPosition.x + center.x + j * step.x, transformPosition.y + center.y + i * step.y, transformPosition.z);

          Debug.DrawRay(position, direction, Color.red);
        }
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
    scannedSigns = ScanForLayer(raysCount * rowsCount, pieceFaceLayerMask)
                   .Select(
                     go => {
                       var signController = go.GetComponentInChildren<RCubePieceFaceMarkController>();
                       return signController != null ? signController.markType : MarkType.None;
                     }
                   )
                   .ToArray();

    rCubeFacePiecesFacesRaycastHitEventInvoker.Invoke(new RCubeFacePiecesFacesRaycastHitEventContext(scannedSigns, facePositionType));
  }

  protected override void CastNonAlloc(RaycastHit[] hits, LayerMask layer) {
    for (var i = 0; i < rowsCount; i++) {
      for (var j = 0; j < raysCount; j++) {
        var transformPosition = transform.localPosition;
        var position = new Vector3(transformPosition.x + center.x + j * step.x, transformPosition.y + center.y + i * step.y, transformPosition.z);

        Physics.Raycast(
          position,
          direction,
          out var hit,
          float.PositiveInfinity,
          pieceFaceLayerMask
        );

        hits[i * rowsCount + j] = hit;
      }
    }
  }
}