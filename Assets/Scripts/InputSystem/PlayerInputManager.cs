public static class PlayerInputManager {
  #region fields

  private static readonly PlayerInput playerInput = new PlayerInput();

  public static PlayerInput.MouseActions mouse = playerInput.Mouse;

  #endregion

  #region methods

  public static void EnableMouseInput() {
    mouse.Enable();
  }

  public static void DisableMouseInput() {
    mouse.Disable();
  }

  #endregion
}