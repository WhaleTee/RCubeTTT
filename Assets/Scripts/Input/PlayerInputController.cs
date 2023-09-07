using UnityEngine;

/// <summary>
/// Represents a class that controls the input behavior of the player.
/// </summary>
public class PlayerInputController : MonoBehaviour {
  #region seiralizable fields

  [SerializeField]
  private bool enableMouseInput;

  [SerializeField]
  private bool enableScreenInput;

  #endregion

  #region unity methods

  private void OnEnable() {
    if (enableMouseInput) {
      PlayerInputManager.EnableMouseInput();
    }

    if (enableScreenInput) { }
  }

  private void OnDisable() {
    if (enableMouseInput) {
      PlayerInputManager.DisableMouseInput();
    }

    if (enableScreenInput) { }
  }

  #endregion
}