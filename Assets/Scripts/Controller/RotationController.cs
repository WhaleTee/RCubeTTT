using MyBox;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class RotationController : MonoBehaviour {
  #region serializable fields

  [SerializeField]
  private bool allowDragRotation;

  [SerializeField]
  [ConditionalField(nameof(allowDragRotation), false, true)]
  private GameObject rotationRelativeObject;

  [SerializeField]
  [ConditionalField(nameof(allowDragRotation), false, true)]
  [Range(10, 100)]
  protected float rotationSpeed;

  [SerializeField]
  [ConditionalField(nameof(allowDragRotation), false, true)]
  [RangeVector(new float[] { }, new float[] { 1, 1, 1 })]
  protected Vector2Int accessRotation;

  #endregion

  #region fields

  protected Pointer currentPointer;
  protected Vector2 dragDeltaInput;

  #endregion

  #region unity methods

  protected virtual void Awake() {
    currentPointer = Pointer.current;
  }

  protected virtual void Update() {
    if (allowDragRotation) {
      Rotate();
    }

    StopDragging();
  }

  #endregion

  #region methods

  protected void ReadDragContext(InputAction.CallbackContext context) => dragDeltaInput = context.ReadValue<Vector2>();
  private void StopDragging() => dragDeltaInput = Vector2.zero;

  protected void Rotate() {
    if (accessRotation.y > 0) {
      var deltaRotation = Vector3.Dot(dragDeltaInput, rotationRelativeObject ? rotationRelativeObject.transform.right : Vector3.right) * rotationSpeed * Time.deltaTime;

      transform.Rotate(rotationRelativeObject ? rotationRelativeObject.transform.up : Vector3.up, -deltaRotation, Space.World);
    }

    if (accessRotation.x > 0) {
      var deltaRotation = Vector3.Dot(dragDeltaInput, rotationRelativeObject ? rotationRelativeObject.transform.up : Vector3.up) * rotationSpeed * Time.deltaTime;

      transform.Rotate(rotationRelativeObject ? rotationRelativeObject.transform.right : Vector3.right, deltaRotation, Space.World);
    }
  }

  #endregion
}