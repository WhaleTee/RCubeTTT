using MyBox;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Provides the function to rotate objects by dragging.
/// </summary>
public abstract class DragRotationController : MonoBehaviour {
  #region fields

  private const float MAX_ROTATION_SPEED = 100;

  protected Pointer currentPointer;
  protected Vector2 inputDelta;

  #endregion

  #region serializable fields

  [SerializeField]
  protected GameObject rotationRelativeObject;

  [SerializeField]
  [Range(0, MAX_ROTATION_SPEED)]
  protected float rotationSpeed;

  [SerializeField]
  [RangeVector(new float[] { }, new float[] { 1, 1, 1 })]
  protected Vector3Int accessRotation = Vector3Int.zero;

  #endregion

  #region methods

  protected void ReadInputContext(InputAction.CallbackContext context) => inputDelta = context.ReadValue<Vector2>();
  protected void ResetInputDelta() => inputDelta = Vector2.zero;

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