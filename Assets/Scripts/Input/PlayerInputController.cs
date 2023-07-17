using UnityEngine;

public class PlayerInputController : MonoBehaviour {
  #region seiralizable fields

  [SerializeField]
  private bool enableMouseInput;

  [SerializeField]
  private bool enableScreenInput;

  #endregion

  #region unity fields

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