using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class DragRotationController : MonoBehaviour, Identifiable {
  #region serializable fields

  [SerializeField]
  protected GameObject rotationRelativeObject;

  [SerializeField]
  [Range(10, 100)]
  protected float rotationSpeed;

  [SerializeField]
  [RangeVector(new float[] { }, new float[] { 1, 1, 1 })]
  protected Vector3Int accessRotation = Vector3Int.zero;

  #endregion

  #region fields

  private readonly string id = Guid.NewGuid().ToString();
  protected Pointer currentPointer;
  protected Vector2 dragDeltaInput;

  #endregion

  #region methods

  protected void ReadDragContext(InputAction.CallbackContext context) => dragDeltaInput = context.ReadValue<Vector2>();
  protected void StopDragging() => dragDeltaInput = Vector2.zero;

  protected virtual void Rotate() {
    if (accessRotation.y > 0) {
      var deltaRotation = Vector3.Dot(dragDeltaInput, rotationRelativeObject ? rotationRelativeObject.transform.right : Vector3.right)
                          * rotationSpeed
                          * Time.deltaTime;

      transform.Rotate(rotationRelativeObject ? rotationRelativeObject.transform.up : Vector3.up, -deltaRotation, Space.World);
    }

    if (accessRotation.x > 0) {
      var deltaRotation = Vector3.Dot(dragDeltaInput, rotationRelativeObject ? rotationRelativeObject.transform.up : Vector3.up)
                          * rotationSpeed
                          * Time.deltaTime;

      transform.Rotate(rotationRelativeObject ? rotationRelativeObject.transform.right : Vector3.right, deltaRotation, Space.World);
    }
  }

  public string GetId() => id;

  #endregion
}