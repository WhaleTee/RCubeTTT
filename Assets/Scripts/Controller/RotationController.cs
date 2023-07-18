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
  }

  protected void Update() {
    Rotate();
    StopDragging();
    RotateToDegrees();
  }

  #endregion

  #region methods

  private void RotateToDegrees() {
    if (!dragging) {
      var startRotation = rotation;
      var targetRotation = GetClosestRotation(rotateToDegrees);

      transform.rotation = Quaternion.Slerp(startRotation, targetRotation, rotationToDegreesElapsedTime / rotateToDegreesDuration);
      rotationToDegreesElapsedTime += Time.deltaTime;
    }
  }

  private Quaternion GetClosestRotation(Vector3Int degrees) {
    var closestRotation = Quaternion.Euler(0, 0, 0);
    var closestAngle = Quaternion.Angle(rotation, closestRotation);

    for (var x = -180; x <= 180; x += degrees.x) {
      for (var y = -180; y <= 180; y += degrees.y) {
        for (var z = -180; z <= 180; z += degrees.z) {
          var targetRotation = Quaternion.Euler(x, y, z);
          var angle = Quaternion.Angle(rotation, targetRotation);

          if (angle < closestAngle) {
            closestRotation = targetRotation;
            closestAngle = angle;
          }
        }
      }
    }

    return closestRotation;
  }

  protected void ReadDragContext(InputAction.CallbackContext context) => dragDeltaInput = context.ReadValue<Vector2>();
  private void StopDragging() => dragDeltaInput = Vector2.zero;

  #endregion

  #region abstract methods

  protected abstract void Rotate();

  #endregion
}