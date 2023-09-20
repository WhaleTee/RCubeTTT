using System.Collections.Generic;
using UnityEngine;

public class RCubeFaceVisualizerController : MonoBehaviour {
  #region serializable fields

  [Header("Visuals")]
  [SerializeField]
  private GameObject emptyFace;

  [SerializeField]
  private GameObject signX;

  [SerializeField]
  private GameObject signO;

  [SerializeField]
  private int visualsCount;

  [SerializeField]
  private int rowsCount;

  [Space]
  [Header("Position")]
  [SerializeField]
  protected Vector2 offset;

  [SerializeField]
  private Vector2 step;

  [Space]
  [Header("Face information")]
  [SerializeField]
  private RCubeFacePositionType facePositionType;

  #endregion

  #region fields

  private GameObject[] facePiecesFacesVisuals;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddRCubeFacePiecesFacesRaycastHitListener(OnRCubeFacePiecesFacesRaycastHit);

    facePiecesFacesVisuals = new GameObject[visualsCount * rowsCount];
  }

  #endregion

  #region methods

  private void OnRCubeFacePiecesFacesRaycastHit(RCubeFacePiecesFacesRaycastHitEventContext context) {
    if (facePositionType == context.facePositionType) {
      UpdateFaceVisualization(context.scannedSigns);
    }
  }

  private void UpdateFaceVisualization(IReadOnlyList<MarkType> scannedSigns) {
    if (scannedSigns != null) {
      for (var i = 0; i < scannedSigns.Count; i++) {
        switch (scannedSigns[i]) {
          case MarkType.X:
            Destroy(facePiecesFacesVisuals[i]);
            facePiecesFacesVisuals[i] = Instantiate(signX, transform);
            break;
          case MarkType.O:
            Destroy(facePiecesFacesVisuals[i]);
            facePiecesFacesVisuals[i] = Instantiate(signO, transform);
            break;
          case MarkType.None:
            Destroy(facePiecesFacesVisuals[i]);
            facePiecesFacesVisuals[i] = Instantiate(emptyFace, transform);
            break;
        }
      }
    } else {
      for (var i = 0; i < facePiecesFacesVisuals.Length; i++) {
        facePiecesFacesVisuals[i] = Instantiate(emptyFace, transform);
      }
    }

    for (var i = 0; i < rowsCount; i++) {
      for (var j = 0; j < visualsCount; j++) {
        var faceLocalScale = facePiecesFacesVisuals[i * rowsCount + j].transform.localScale;

        facePiecesFacesVisuals[i * rowsCount + j].transform.localPosition = new Vector2(
          offset.x * faceLocalScale.x + j * step.x * faceLocalScale.x,
          offset.y * faceLocalScale.y + i * step.y * faceLocalScale.y
        );
      }
    }
  }

  #endregion
}