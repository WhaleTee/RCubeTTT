using System.Linq;
using UnityEngine;

public class FixedCubeFaceRotationController : FixedRotationController {
  #region fields

  private int cubePieceLayer;
  
  private ObjectScanner objectScanner;
  
  private bool isDragging;
  private Quaternion targetRotation;
  private float rotationElapsedTime;
  private CubePiece[] cubePieces;

  #endregion

  #region properties

  private Vector3 localRotationEuler => transform.localRotation.eulerAngles;

  #endregion

  #region unity methods

  private void Awake() {
    targetRotation = CurrentRotation();
    EventManager.AddStartDragRCubeFaceListener(StartDragRCubeFaceHandler);
    EventManager.AddEndDragRCubeFaceListener(EndDragRCubeFaceHandler);
    
    cubePieceLayer = LayerMask.GetMask("CubePiece");
    
    objectScanner = GetComponent<ObjectScanner>();
  }

  private void Update() {
    if (!isDragging) {
      Rotate();
      RotateCubePieces();
      rotationElapsedTime += Time.deltaTime;
    }
  }

  #endregion

  #region methods

  private void Rotate() {
    transform.localRotation = Quaternion.Slerp(CurrentRotation(), targetRotation, rotationElapsedTime / rotateDuration);
  }
  
  private void RotateCubePieces() {
    // foreach (var cubePiece in cubePieces) {
    //   
    //   cubePiece.transform.localRotation = Quaternion.Slerp(CurrentRotation(), targetRotation, rotationElapsedTime / rotateDuration);
    // }
  }

  private void StartDragRCubeFaceHandler() {
    rotationElapsedTime = 0;
    isDragging = true;
    targetRotation = CurrentRotation();
    cubePieces = objectScanner.ScanForLayer(9, cubePieceLayer).Select(go => go.GetComponent<CubePiece>()).ToArray();
  }

  private void EndDragRCubeFaceHandler() {
    isDragging = false;
    targetRotation = GetNearestRotation();
  }

  protected override Quaternion CurrentRotation() => transform.localRotation;

  #endregion
}