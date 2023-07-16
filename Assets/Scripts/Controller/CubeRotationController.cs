using UnityEngine;
using UnityEngine.InputSystem;

public class CubeRotationController : MonoBehaviour {
  #region serializable fields

  [SerializeField]
  [Range(.5f, 5)]
  private float rotateLerpDuration;

  #endregion

  #region fields

  private bool dragging;
  private float timeElapsed;

  #endregion

  #region properties

  private Quaternion rotation => transform.rotation;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddMouseUpInputListener(ReadMouseRightUpContext);
    EventManager.AddMouseDownInputListener(ReadMouseRightDownContext);
  }

  private void Update() {
    RotateTo90Degrees();
  }

  #endregion

  #region methods

  private void ReadMouseRightDownContext(InputAction.CallbackContext context) {
    dragging = true;
    Debug.Log("mouse right down");
    Debug.Log(dragging);
  }

  private void ReadMouseRightUpContext(InputAction.CallbackContext context) {
    timeElapsed = 0;
    dragging = false;
    Debug.Log("mouse right up");
    Debug.Log(dragging);
  }

  private void RotateTo90Degrees() {
    if (!dragging) {
      var startRotation = rotation;
      var targetRotation = GetClosestTo90Rotation();

      transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / rotateLerpDuration);
      timeElapsed += Time.deltaTime;
    }
  }

  private Quaternion GetClosestTo90Rotation() {
    var closestRotation = Quaternion.Euler(0, 0, 0);
    var closestAngle = Quaternion.Angle(rotation, closestRotation);
    for (var x = -180; x <= 180; x += 90)
    {
      for (var y = -180; y <= 180; y += 90)
      {
        for (var z = -180; z <= 180; z += 90)
        {
          var targetRotation = Quaternion.Euler(x, y, z);
          var angle = Quaternion.Angle(rotation, targetRotation);
          if (angle < closestAngle)
          {
            closestRotation = targetRotation;
            closestAngle = angle;
          }
        }
      }
    }
    return closestRotation;
  }

  #endregion
}