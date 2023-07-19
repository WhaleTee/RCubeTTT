using UnityEngine;
using UnityEngine.InputSystem;

public abstract class RotationController : MonoBehaviour {
  #region serializable fields

  [SerializeField]
  protected Vector3 rotationPoint;

  [SerializeField]
  [Range(10, 100)]
  protected float rotationSpeed;

  [SerializeField]
  [RangeVector(new float[] { 90, 90, 90 }, new float[] { 180, 180, 180 })]
  protected Vector3Int rotateToDegrees;

  [SerializeField]
  [Range(1, 3)]
  protected float rotateToDegreesDuration;

  [SerializeField]
  protected Camera targetCamera;

  #endregion

  #region fields

  protected Pointer currentPointer;
  protected bool dragging;
  protected Vector2 dragDeltaInput;
  protected float rotationToDegreesElapsedTime;

  #endregion

  #region properties

  private Quaternion rotation => transform.rotation;

  #endregion

  #region unity methods

  protected virtual void Awake() {
    currentPointer = Pointer.current;
    PlayerInputManager.mouse.RightClick.canceled += MouseUpHandler;
    PlayerInputManager.mouse.LeftClick.canceled += MouseUpHandler;
  }

  protected void Update() {
    Rotate();
    StopDragging();
    RotateToDegrees();
  }

  #endregion

  #region methods

  private void MouseUpHandler(InputAction.CallbackContext context) {
    if (dragging) {
      rotationToDegreesElapsedTime = 0;
      dragging = false;
    }
  }

  private void RotateToDegrees() {
    if (!dragging) {
      var targetRotation = GetClosestRotation(rotation, rotateToDegrees);

      transform.rotation = Quaternion.Slerp(rotation, targetRotation, rotationToDegreesElapsedTime / rotateToDegreesDuration);
      rotationToDegreesElapsedTime += Time.deltaTime;
    }
  }

  private Quaternion GetClosestRotation(Quaternion currentRotation, Vector3Int degrees) {
    if (rotateToDegrees != Vector3Int.zero) {
      var iterateValues = new Vector3Int(-180, -180, -180);
      var closestRotation = Quaternion.Euler(0, 0, 0);
      var closestAngle = Quaternion.Angle(currentRotation, closestRotation);

      while (degrees.x > 0 && iterateValues.x <= 180) {
        closestAngle = ClosestAngle(currentRotation, iterateValues, closestAngle, ref closestRotation);
        iterateValues.x += degrees.x;
      }

      while (degrees.y > 0 && iterateValues.y <= 180) {
        closestAngle = ClosestAngle(currentRotation, iterateValues, closestAngle, ref closestRotation);
        iterateValues.y += degrees.y;
      }

      while (degrees.z > 0 && iterateValues.z <= 180) {
        closestAngle = ClosestAngle(currentRotation, iterateValues, closestAngle, ref closestRotation);
        iterateValues.z += degrees.z;
      }

      return closestRotation;
    }

    return Quaternion.identity;
  }

  private static float ClosestAngle(Quaternion currentRotation, Vector3Int iterateValues, float closestAngle, ref Quaternion closestRotation) {
    var targetRotation = Quaternion.Euler(iterateValues.x, iterateValues.y, iterateValues.z);
    var angle = Quaternion.Angle(currentRotation, targetRotation);

    if (angle < closestAngle) {
      closestRotation = targetRotation;
      closestAngle = angle;
    }

    return closestAngle;
  }

  protected void ReadDragContext(InputAction.CallbackContext context) => dragDeltaInput = context.ReadValue<Vector2>();
  private void StopDragging() => dragDeltaInput = Vector2.zero;

  #endregion

  #region abstract methods

  protected abstract void Rotate();

  #endregion
}