/// <summary>
/// Controls the behavior of the Rubik's Cube.
/// </summary>
public class RCubeDragRotationController : DragRotationController {
  #region fields

  private bool isDragging;

  #endregion

  #region unity methods

  private void Awake() {
    EventManager.AddRCubeDragStartListener(OnRCubeDragStart);
    EventManager.AddRCubeDragEndListener(OnRCubeDragEnd);
  }

  private void Update() {
    if (isDragging) {
      Rotate();
    }

    ResetInputDelta();
  }

  #endregion

  #region methods

  /// <summary>
  /// Handles the right mouse button down event.
  /// Starts reading input context to initiate dragging. 
  /// </summary>
  private void OnRCubeDragStart() {
    PlayerInputManager.mouse.Drag.performed += ReadInputContext;
    isDragging = true;
  }

  /// <summary>
  /// Handles the right mouse button up event.
  /// If the Rubik's Cube is currently being dragged, stops reading input context to stop dragging. 
  /// </summary>
  private void OnRCubeDragEnd() {
    PlayerInputManager.mouse.Drag.performed -= ReadInputContext;
    isDragging = false;
  }

  #endregion
}