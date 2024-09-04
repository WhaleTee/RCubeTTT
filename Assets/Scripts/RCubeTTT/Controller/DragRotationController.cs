using UnityEngine;
using UnityEngine.InputSystem;

namespace RCubeTTT.Controller {
  /// <summary>
  /// Provides the function to rotate objects by dragging.
  /// </summary>
  public abstract class DragRotationController : MonoBehaviour {
    #region fields

    private const float MAX_ROTATION_SPEED = 100;

    [SerializeField]
    protected GameObject rotationRelativeObject;

    [SerializeField]
    [Range(0, MAX_ROTATION_SPEED)]
    protected float rotationSpeed;

    [SerializeField]
    protected Vector3Int accessRotation = Vector3Int.zero;

    protected Vector2 inputDelta;

    #endregion

    #region methods

    /// <summary>
    /// Reads the input context and updates the input delta vector based on the input value.
    /// </summary>
    /// <param name="context">The input action callback context.</param>
    protected void ReadInputContext(InputAction.CallbackContext context) => inputDelta += context.ReadValue<Vector2>();

    /// <summary>
    /// Resets the input delta vector to zero.
    /// </summary>
    protected void ResetInputDelta() => inputDelta = Vector2.zero;

    /// <summary>
    /// Rotates the object based on the input delta and rotation settings.
    /// </summary>
    protected virtual void Rotate() {
      if (accessRotation.y > 0) {
        var deltaRotation = Vector3.Dot(inputDelta, rotationRelativeObject ? rotationRelativeObject.transform.right : Vector3.right)
                            * rotationSpeed
                            * Time.deltaTime;

        transform.Rotate(rotationRelativeObject ? rotationRelativeObject.transform.up : Vector3.up, -deltaRotation, Space.World);
      }

      if (accessRotation.x > 0) {
        var deltaRotation = Vector3.Dot(inputDelta, rotationRelativeObject ? rotationRelativeObject.transform.up : Vector3.up)
                            * rotationSpeed
                            * Time.deltaTime;

        transform.Rotate(rotationRelativeObject ? rotationRelativeObject.transform.right : Vector3.right, deltaRotation, Space.World);
      }
    }

    #endregion
  }
}